using System.Text.Json;
using EcommerceMvcStore.Models.Ecommerce;

namespace EcommerceMvcStore.Services;

public class CartService(IHttpContextAccessor httpContextAccessor)
{
    private const string CartSessionKey = "ShoppingCart";

    private ISession Session => httpContextAccessor.HttpContext!.Session;

    public List<CartItem> GetCartItems()
    {
        var json = Session.GetString(CartSessionKey);
        return string.IsNullOrWhiteSpace(json)
            ? new List<CartItem>()
            : JsonSerializer.Deserialize<List<CartItem>>(json) ?? new List<CartItem>();
    }

    public void SaveCartItems(List<CartItem> items)
    {
        Session.SetString(CartSessionKey, JsonSerializer.Serialize(items));
    }

    public void AddToCart(Product product, int quantity)
    {
        var items = GetCartItems();
        var existing = items.FirstOrDefault(x => x.ProductId == product.Id);

        if (existing is null)
        {
            items.Add(new CartItem
            {
                ProductId = product.Id,
                Name = product.Name,
                UnitPrice = product.Price,
                Quantity = quantity,
                ImageUrl = product.ImageUrl
            });
        }
        else
        {
            existing.Quantity += quantity;
        }

        SaveCartItems(items);
    }

    public void UpdateQuantity(int productId, int quantity)
    {
        var items = GetCartItems();
        var item = items.FirstOrDefault(x => x.ProductId == productId);
        if (item is null) return;

        if (quantity <= 0)
        {
            items.Remove(item);
        }
        else
        {
            item.Quantity = quantity;
        }

        SaveCartItems(items);
    }

    public void Clear() => Session.Remove(CartSessionKey);
}
