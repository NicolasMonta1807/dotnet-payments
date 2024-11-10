using Microsoft.EntityFrameworkCore;
using PaymentsIntegration.Model;

namespace PaymentsIntegration.Data;

public class PaymentsDbContext : DbContext
{
    public PaymentsDbContext(DbContextOptions<PaymentsDbContext> options) : base(options)
    {
    }

    public DbSet<PayTransaction> Payments { get; set; }
}