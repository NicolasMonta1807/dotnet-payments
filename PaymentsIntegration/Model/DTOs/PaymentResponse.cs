namespace PaymentsIntegration.Model.DTOs;

public class PaymentResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public string TransactionId { get; set; }
}