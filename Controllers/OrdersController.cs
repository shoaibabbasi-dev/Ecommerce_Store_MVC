using System.Security.Claims;
using EcommerceMvcStore.Data;
using EcommerceMvcStore.Filters;
using EcommerceMvcStore.Models.Ecommerce;
using EcommerceMvcStore.Services;
using EcommerceMvcStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMvcStore.Controllers;

[Authorize]
[CustomerOnly]
public class OrdersController(ApplicationDbContext context, CartService cartService, PaymentService paymentService) : Controller
{
    public IActionResult Checkout()
    {
        var items = cartService.GetCartItems();
        if (items.Count == 0) return RedirectToAction("Index", "Cart");

        var vm = new CheckoutViewModel
        {
            CartItems = items,
            TotalAmount = items.Sum(x => x.LineTotal)
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(CheckoutViewModel vm)
    {
        var items = cartService.GetCartItems();
        if (items.Count == 0)
        {
            ModelState.AddModelError(string.Empty, "Your cart is empty.");
        }

        if (vm.PaymentMethod == PaymentService.MockCard && string.IsNullOrWhiteSpace(vm.CardNumber))
        {
            ModelState.AddModelError(nameof(vm.CardNumber), "Card number is required for card payment.");
        }

        if (!ModelState.IsValid)
        {
            vm.CartItems = items;
            vm.TotalAmount = items.Sum(x => x.LineTotal);
            return View(vm);
        }

        var total = items.Sum(x => x.LineTotal);
        var paymentResult = paymentService.ProcessPayment(vm.PaymentMethod, total, vm.CardNumber);
        if (!paymentResult.Success)
        {
            ModelState.AddModelError(string.Empty, paymentResult.ErrorMessage ?? "Payment failed.");
            vm.CartItems = items;
            vm.TotalAmount = total;
            return View(vm);
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var order = new Order
        {
            UserId = userId,
            CustomerName = vm.CustomerName,
            Email = vm.Email,
            ShippingAddress = vm.ShippingAddress,
            PhoneNumber = vm.PhoneNumber,
            TotalAmount = total,
            PaymentMethod = vm.PaymentMethod,
            PaymentStatus = paymentResult.PaymentStatus,
            TransactionId = paymentResult.TransactionId,
            Status = paymentResult.PaymentStatus == "Paid" ? "Processing" : "Pending",
            Items = items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                ProductName = i.Name,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity,
                LineTotal = i.LineTotal
            }).ToList()
        };

        context.Orders.Add(order);
        await context.SaveChangesAsync();
        cartService.Clear();
        return RedirectToAction(nameof(Confirmation), new { id = order.Id });
    }

    public async Task<IActionResult> Confirmation(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var order = await context.Orders.Include(x => x.Items).FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        return order is null ? NotFound() : View(order);
    }

    public async Task<IActionResult> MyOrders()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var orders = await context.Orders
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToListAsync();
        return View(orders);
    }
}
