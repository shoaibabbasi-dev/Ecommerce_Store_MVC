using EcommerceMvcStore.Data;
using EcommerceMvcStore.Filters;
using EcommerceMvcStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMvcStore.Controllers;

[CustomerOnly]
public class StoreController(ApplicationDbContext context) : Controller
{
    public async Task<IActionResult> Index(int? categoryId, string? q)
    {
        var query = context.Products
            .Include(p => p.Category)
            .Where(p => p.IsActive);

        if (categoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == categoryId);
        }

        if (!string.IsNullOrWhiteSpace(q))
        {
            query = query.Where(p => p.Name.Contains(q) || p.Description.Contains(q));
        }

        var vm = new ProductListViewModel
        {
            Products = await query.OrderBy(p => p.Name).ToListAsync(),
            Categories = await context.Categories.OrderBy(c => c.Name).ToListAsync(),
            SelectedCategoryId = categoryId,
            SearchTerm = q ?? string.Empty
        };

        return View(vm);
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await context.Products
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

        if (product is null) return NotFound();
        return View(product);
    }
}
