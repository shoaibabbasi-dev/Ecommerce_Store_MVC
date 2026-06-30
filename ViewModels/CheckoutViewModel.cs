using System.ComponentModel.DataAnnotations;
using EcommerceMvcStore.Models.Ecommerce;
using EcommerceMvcStore.Services;

namespace EcommerceMvcStore.ViewModels;

public class CheckoutViewModel
{
    [Required, StringLength(120)]
    [Display(Name = "Full Name")]
    public string CustomerName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required, StringLength(300)]
    [Display(Name = "Shipping Address")]
    public string ShippingAddress { get; set; } = string.Empty;

    [Required, StringLength(30)]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Payment Method")]
    public string PaymentMethod { get; set; } = PaymentService.CashOnDelivery;

    [Display(Name = "Card Number (test)")]
    public string? CardNumber { get; set; }

    public List<CartItem> CartItems { get; set; } = new();
    public decimal TotalAmount { get; set; }
}
