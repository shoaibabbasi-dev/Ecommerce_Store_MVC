using EcommerceMvcStore.Models.Ecommerce;

namespace EcommerceMvcStore.ViewModels;

public class ProductListViewModel
{
    public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();
    public IEnumerable<Category> Categories { get; set; } = Enumerable.Empty<Category>();
    public int? SelectedCategoryId { get; set; }
    public string SearchTerm { get; set; } = string.Empty;
}
