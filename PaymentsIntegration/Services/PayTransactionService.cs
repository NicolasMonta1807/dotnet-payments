using System.Globalization;
using PaymentsIntegration.Data;
using PaymentsIntegration.Model;
using PaymentsIntegration.Model.DTOs;

namespace PaymentsIntegration.Services;

public class PayTransactionService(PaymentsDbContext context, ILogger<PayTransactionService> logger)
{
    private const string TransactionSuccessMessage = "Transaction successful";
    private const string InvalidCardMessage = "Invalid card";
    private static readonly Random Random = new();

    private static bool ValidateFunds()
    {
        return Random.Next(0, 2) == 1;
    }

    private bool IsValidCardNumber(string cardNumber)
    {
        if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length != 16)
        {
            logger.LogError("Invalid card number");
            return false;
        }

        return true;
    }

    private bool IsValidExpirationDate(string expirationDate)
    {
        if (!DateTime.TryParseExact(expirationDate, "MM/yy", null, DateTimeStyles.None, out var expDate)
            || expDate < DateTime.UtcNow)
        {
            logger.LogError("Invalid expiration date");
            return false;
        }

        return true;
    }

    private bool ValidateCard(PaymentRequest request)
    {
        return IsValidCardNumber(request.CardNumber) &&
               IsValidExpirationDate(request.ExpirationDate) &&
               ValidateFunds();
    }

    public PaymentResponse ProcessPayment(PaymentRequest request)
    {
        if (!ValidateCard(request))
            return new PaymentResponse
            {
                Success = false,
                Message = InvalidCardMessage,
                TransactionId = string.Empty
            };

        var transaction = new PayTransaction
        {
            Id = Guid.NewGuid().ToString(),
            Amount = request.Amount,
            TransactionDate = DateTime.Now,
            IsSuccessful = true,
            Message = TransactionSuccessMessage,
            LastDigits = request.CardNumber[^4..]
        };

        context.Payments.Add(transaction);
        context.SaveChanges();

        return new PaymentResponse
        {
            Success = true,
            Message = string.Empty,
            TransactionId = transaction.Id
        };
    }
}