using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletCash.Data.Entities;

namespace WalletCash.Data.Configurations;

public class CashoutReferenceEntityConfiguration : IEntityTypeConfiguration<CashoutReferenceEntity>
{
    public void Configure(EntityTypeBuilder<CashoutReferenceEntity> builder)
    {
        builder.ToTable("References");
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Reference).HasMaxLength(64).IsRequired();
        builder.HasIndex(r => r.Reference).IsUnique();

        builder.Property(r => r.Currency).HasMaxLength(3).IsRequired();
        builder.Property(r => r.Amount).HasColumnType("decimal(18,2)");
        builder.Property(r => r.Name).HasMaxLength(200);
        builder.Property(r => r.Email).HasMaxLength(256);
        builder.Property(r => r.Phone).HasMaxLength(30);

        builder.Property(r => r.CreatedAtUtc).HasDefaultValueSql("SYSUTCDATETIME()");
        builder.Property(r => r.UpdatedAtUtc).HasDefaultValueSql("SYSUTCDATETIME()");

        builder.Property(r => r.RowVersion).IsRowVersion();

        builder.HasIndex(r => r.Status);
    }
}
