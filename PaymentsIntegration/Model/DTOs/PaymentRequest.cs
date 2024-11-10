namespace PaymentsIntegration.Model.DTOs;

public class PaymentRequest
{
    public string CardNumber { get; set; } = string.Empty;
    public string ExpirationDate { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}