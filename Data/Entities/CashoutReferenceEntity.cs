namespace WalletCash.Data.Entities;

public class CashoutReferenceEntity
{
    public long Id { get; set; }
    public string Reference { get; set; } = string.Empty;
    public CashoutReferenceStatus Status { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? CustomData { get; set; }
    public int? ExpirationMinutes { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset? ExpirationDate { get; set; }
    public DateTimeOffset? CancelledAtUtc { get; set; }
    public DateTimeOffset CreatedAtUtc { get; set; }
    public DateTimeOffset UpdatedAtUtc { get; set; }
    public byte[]? RowVersion { get; set; }

    public List<CashoutNotificationEntity> Notifications { get; set; } = [];
}
