using EcommerceMvcStore.Data;
using EcommerceMvcStore.Filters;
using EcommerceMvcStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMvcStore.Controllers;

[CustomerOnly]
public class CartController(ApplicationDbContext context, CartService cartService) : Controller
{
    public IActionResult Index() => View(cartService.GetCartItems());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int productId, int quantity = 1)
    {
        var product = await context.Products.FirstOrDefaultAsync(x => x.Id == productId && x.IsActive);
        if (product is null) return NotFound();

        cartService.AddToCart(product, Math.Max(1, quantity));
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(int productId, int quantity)
    {
        cartService.UpdateQuantity(productId, quantity);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Clear()
    {
        cartService.Clear();
        return RedirectToAction(nameof(Index));
    }
}
