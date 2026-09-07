using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletCash.Data.Entities;

namespace WalletCash.Data.Configurations;

public class CashoutNotificationEntityConfiguration : IEntityTypeConfiguration<CashoutNotificationEntity>
{
    public void Configure(EntityTypeBuilder<CashoutNotificationEntity> builder)
    {
        builder.ToTable("Notifications");
        builder.HasKey(n => n.Id);

        builder.Property(n => n.DappNotificationId).HasMaxLength(64).IsRequired();
        builder.HasIndex(n => n.DappNotificationId).IsUnique();

        builder.Property(n => n.ReferenceText).HasMaxLength(64).IsRequired();
        builder.Property(n => n.Amount).HasColumnType("decimal(18,2)");
        builder.Property(n => n.Fee).HasColumnType("decimal(18,2)");
        builder.Property(n => n.Total).HasColumnType("decimal(18,2)");
        builder.Property(n => n.Currency).HasMaxLength(3).IsRequired();
        builder.Property(n => n.Merchant).HasMaxLength(200).IsRequired();
        builder.Property(n => n.SecurityKey).HasMaxLength(100);
        builder.Property(n => n.ResultMessage).HasMaxLength(500);

        builder.Property(n => n.ReceivedAtUtc).HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasOne(n => n.Reference)
            .WithMany(r => r.Notifications)
            .HasForeignKey(n => n.ReferenceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(n => n.ReferenceId);
    }
}
