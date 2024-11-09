using Microsoft.EntityFrameworkCore;
using PaymentsIntegration.Model;

namespace PaymentsIntegration.Data;

public class PaymentsDbContext(DbContextOptions<PaymentsDbContext> context) : DbContext
{
    private readonly DbContextOptions<PaymentsDbContext> _context = context;

    public DbSet<PayTransaction> Payments { get; set; }
}