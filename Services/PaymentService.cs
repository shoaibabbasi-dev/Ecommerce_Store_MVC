namespace EcommerceMvcStore.Services;

public class PaymentResult
{
    public bool Success { get; set; }
    public string PaymentStatus { get; set; } = "Pending";
    public string? TransactionId { get; set; }
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Free demo payment processor (no real charges). Supports mock card and cash on delivery.
/// </summary>
public class PaymentService
{
    public const string CashOnDelivery = "CashOnDelivery";
    public const string MockCard = "MockCard";

    public PaymentResult ProcessPayment(string paymentMethod, decimal amount, string? cardNumber = null)
    {
        if (amount <= 0)
        {
            return new PaymentResult { Success = false, ErrorMessage = "Invalid order amount." };
        }

        return paymentMethod switch
        {
            CashOnDelivery => new PaymentResult
            {
                Success = true,
                PaymentStatus = "Pending",
                TransactionId = null
            },
            MockCard => ProcessMockCard(amount, cardNumber),
            _ => new PaymentResult { Success = false, ErrorMessage = "Please select a valid payment method." }
        };
    }

    private static PaymentResult ProcessMockCard(decimal amount, string? cardNumber)
    {
        var normalized = (cardNumber ?? string.Empty).Replace(" ", "").Replace("-", "");
        if (normalized.Length != 16 || !normalized.All(char.IsDigit))
        {
            return new PaymentResult
            {
                Success = false,
                ErrorMessage = "Enter a valid 16-digit test card number (e.g. 4242424242424242)."
            };
        }

        // Demo rule: cards ending in 0000 are treated as declined.
        if (normalized.EndsWith("0000", StringComparison.Ordinal))
        {
            return new PaymentResult
            {
                Success = false,
                PaymentStatus = "Failed",
                ErrorMessage = "Payment declined by demo gateway. Try another test card."
            };
        }

        return new PaymentResult
        {
            Success = true,
            PaymentStatus = "Paid",
            TransactionId = $"TXN-{Guid.NewGuid():N}"[..16].ToUpperInvariant()
        };
    }
}
