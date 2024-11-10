using System.Globalization;
using PaymentsIntegration.Data;
using PaymentsIntegration.Model;
using PaymentsIntegration.Model.DTOs;

namespace PaymentsIntegration.Services;

public class PayTransactionService
{
    private const string TransactionSuccessMessage = "Transaction successful";
    private static readonly Random Random = new();
    private readonly PaymentsDbContext _context;
    private readonly ILogger<PayTransactionService> _logger;

    public PayTransactionService(PaymentsDbContext context, ILogger<PayTransactionService> logger)
    {
        _context = context;
        _logger = logger;
    }

    private static bool ValidateFunds()
    {
        return Random.Next(0, 100) < 95;
    }

    private static bool ValidateLuhn(string cardNumber)
    {
        var sum = 0;
        var isSecond = false;
        for (var i = cardNumber.Length - 1; i >= 0; i--)
        {
            var digit = cardNumber[i] - '0';

            if (isSecond)
            {
                digit *= 2;
                if (digit > 9)
                    digit -= 9;
            }

            sum += digit;
            isSecond = !isSecond;
        }

        return sum % 10 == 0;
    }

    private bool IsValidCardNumber(string cardNumber)
    {
        if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length != 16)
        {
            _logger.LogError("Invalid card number");
            return false;
        }

        // return ValidateLuhn(cardNumber);
        return true;
    }

    private bool IsValidExpirationDate(string expirationDate)
    {
        if (!DateTime.TryParseExact(expirationDate, "MM/yy", null, DateTimeStyles.None, out var expDate)
            || expDate < DateTime.UtcNow)
        {
            _logger.LogError("Invalid expiration date");
            return false;
        }

        return true;
    }

    private (bool isValid, string errorMessage) ValidateCard(PaymentRequest request)
    {
        if (!IsValidCardNumber(request.CardNumber))
            return (false, "Invalid card number");

        if (!IsValidExpirationDate(request.ExpirationDate))
            return (false, "Invalid expiration date");

        if (!ValidateFunds())
            return (false, "Insufficient funds");

        return (true, string.Empty);
    }

    public PaymentResponse ProcessPayment(PaymentRequest request)
    {
        if (request.Amount <= 0)
        {
            var error = request.Amount < 0 ? "Amount cannot be negative" : "Amount must be greater than zero";
            _logger.LogError(error);
            return new PaymentResponse
            {
                Success = false,
                Message = error,
                TransactionId = string.Empty
            };
        }

        var (isValid, errorMessage) = ValidateCard(request);

        if (!isValid)
            return new PaymentResponse
            {
                Success = false,
                Message = errorMessage,
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

        _context.Payments.Add(transaction);
        _context.SaveChanges();

        return new PaymentResponse
        {
            Success = true,
            Message = string.Empty,
            TransactionId = transaction.Id
        };
    }
}