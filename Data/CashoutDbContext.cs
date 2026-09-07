using Microsoft.EntityFrameworkCore;
using WalletCash.Data.Entities;

namespace WalletCash.Data;

public class CashoutDbContext : DbContext
{
    public CashoutDbContext(DbContextOptions<CashoutDbContext> options) : base(options)
    {
    }

    public DbSet<CashoutReferenceEntity> References => Set<CashoutReferenceEntity>();
    public DbSet<CashoutNotificationEntity> Notifications => Set<CashoutNotificationEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("cashout");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CashoutDbContext).Assembly);
    }
}
