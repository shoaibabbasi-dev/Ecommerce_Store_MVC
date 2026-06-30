using EcommerceMvcStore.Models.Ecommerce;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EcommerceMvcStore.Data;

public static class DbSeeder
{
    private static readonly string[] ElectronicsNames =
    [
        "Wireless Headphones", "Bluetooth Speaker", "Smart Watch", "Gaming Mouse", "Mechanical Keyboard",
        "4K Monitor", "USB-C Hub", "Portable SSD", "Action Camera", "Drone Camera"
    ];

    private static readonly string[] FashionNames =
    [
        "Casual Jacket", "Denim Jeans", "Cotton Shirt", "Running Shoes", "Sports Hoodie",
        "Leather Belt", "Classic Sneakers", "Summer Cap", "Backpack", "Wool Sweater"
    ];

    private static readonly string[] HomeNames =
    [
        "Coffee Maker", "Air Fryer", "Blender", "Vacuum Cleaner", "Electric Kettle",
        "Dining Set", "Desk Lamp", "Memory Foam Pillow", "Wall Shelf", "Cookware Set"
    ];

    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        await context.Database.MigrateAsync();

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        const string adminEmail = "admin@store.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser is null)
        {
            adminUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(adminUser, "Admin@12345");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }

        var categories = await EnsureCategoriesAsync(context);
        await EnsureProductsAsync(context, categories);
    }

    private static async Task<List<Category>> EnsureCategoriesAsync(ApplicationDbContext context)
    {
        if (!await context.Categories.AnyAsync())
        {
            context.Categories.AddRange(
                new Category { Name = "Electronics", Description = "Smart devices and gadgets" },
                new Category { Name = "Fashion", Description = "Clothes and accessories" },
                new Category { Name = "Home", Description = "Home and kitchen essentials" }
            );
            await context.SaveChangesAsync();
        }

        return await context.Categories.OrderBy(c => c.Id).ToListAsync();
    }

    private static async Task EnsureProductsAsync(ApplicationDbContext context, List<Category> categories)
    {
        const int targetProductCount = 120;
        var existingCount = await context.Products.CountAsync();
        if (existingCount >= targetProductCount) return;

        var productsToAdd = new List<Product>();
        var random = new Random(42);

        var categoryProductNames = new Dictionary<string, string[]>
        {
            ["Electronics"] = ElectronicsNames,
            ["Fashion"] = FashionNames,
            ["Home"] = HomeNames
        };

        var index = existingCount;
        while (existingCount + productsToAdd.Count < targetProductCount)
        {
            foreach (var category in categories)
            {
                if (existingCount + productsToAdd.Count >= targetProductCount) break;
                var names = categoryProductNames.GetValueOrDefault(category.Name, HomeNames);
                var baseName = names[index % names.Length];
                var variant = $"{baseName} #{index + 1}";
                var price = random.Next(20, 500) + random.Next(0, 99) / 100m;
                var stock = random.Next(5, 200);
                productsToAdd.Add(new Product
                {
                    Name = variant,
                    Description = $"{variant} - premium quality {category.Name.ToLower()} product for everyday use.",
                    Price = decimal.Round(price, 2),
                    StockQuantity = stock,
                    CategoryId = category.Id,
                    IsActive = true,
                    ImageUrl = $"https://picsum.photos/seed/{Uri.EscapeDataString(variant)}/600/400"
                });
                index++;
            }
        }

        context.Products.AddRange(productsToAdd);
        await context.SaveChangesAsync();
    }
}
