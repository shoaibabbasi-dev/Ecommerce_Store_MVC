using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceMvcStore.Models.Ecommerce;

public class Order
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required, StringLength(120)]
    public string CustomerName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required, StringLength(300)]
    public string ShippingAddress { get; set; } = string.Empty;

    [Required, StringLength(30)]
    public string PhoneNumber { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    [Required, StringLength(30)]
    public string Status { get; set; } = "Pending";

    [Required, StringLength(30)]
    public string PaymentMethod { get; set; } = "CashOnDelivery";

    [Required, StringLength(30)]
    public string PaymentStatus { get; set; } = "Pending";

    [StringLength(50)]
    public string? TransactionId { get; set; }

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
