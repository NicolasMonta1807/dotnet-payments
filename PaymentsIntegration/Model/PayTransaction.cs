using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsIntegration.Model;

[Table("pay_transaction")]
public class PayTransaction
{
    [Column("id")] [MaxLength(64)] public string Id { get; set; } = "";

    [Column("amount")] public decimal Amount { get; set; }

    [Column("transaction_date")] public DateTime TransactionDate { get; set; }

    [Column("is_successful")] public bool IsSuccessful { get; set; }

    [Column("message")] [MaxLength(512)] public string Message { get; set; } = "";

    [Column("last_digits")] [MaxLength(4)] public string LastDigits { get; set; } = "";
}