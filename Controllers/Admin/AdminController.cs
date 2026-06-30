using EcommerceMvcStore.Data;
using EcommerceMvcStore.Models.Ecommerce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMvcStore.Controllers.Admin;

[Authorize(Roles = "Admin")]
public class AdminController(ApplicationDbContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        ViewBag.ProductCount = await context.Products.CountAsync();
        ViewBag.ActiveProducts = await context.Products.CountAsync(x => x.IsActive);
        ViewBag.InactiveProducts = await context.Products.CountAsync(x => !x.IsActive);
        ViewBag.LowStockCount = await context.Products.CountAsync(x => x.StockQuantity <= 10 && x.IsActive);
        ViewBag.CategoryCount = await context.Categories.CountAsync();
        ViewBag.PendingOrders = await context.Orders.CountAsync(x => x.Status == "Pending");
        ViewBag.RecentProducts = await context.Products
            .Include(x => x.Category)
            .OrderByDescending(x => x.Id)
            .Take(8)
            .ToListAsync();
        return View();
    }

    public async Task<IActionResult> Products()
    {
        var products = await context.Products.Include(x => x.Category).OrderBy(x => x.Name).ToListAsync();
        return View(products);
    }

    public async Task<IActionResult> CreateProduct()
    {
        await LoadCategories();
        return View(new Product());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProduct(Product product)
    {
        if (!ModelState.IsValid)
        {
            await LoadCategories();
            return View(product);
        }

        context.Products.Add(product);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Products));
    }

    public async Task<IActionResult> EditProduct(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product is null) return NotFound();

        await LoadCategories();
        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProduct(Product product)
    {
        if (!ModelState.IsValid)
        {
            await LoadCategories();
            return View(product);
        }

        context.Products.Update(product);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Products));
    }

    public async Task<IActionResult> Orders()
    {
        var orders = await context.Orders.OrderByDescending(x => x.CreatedAtUtc).ToListAsync();
        return View(orders);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateOrderStatus(int id, string status)
    {
        var allowedStatuses = new[] { "Pending", "Processing", "Shipped", "Delivered", "Cancelled" };
        if (!allowedStatuses.Contains(status))
        {
            return BadRequest();
        }

        var order = await context.Orders.FindAsync(id);
        if (order is null) return NotFound();

        order.Status = status;
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Orders));
    }

    private async Task LoadCategories()
    {
        var categories = await context.Categories.OrderBy(c => c.Name).ToListAsync();
        ViewBag.Categories = new SelectList(categories, "Id", "Name");
    }
}
