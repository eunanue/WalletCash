namespace WalletCash.Data.Entities;

public class CashoutNotificationEntity
{
    public long Id { get; set; }
    public string DappNotificationId { get; set; } = string.Empty;
    public long? ReferenceId { get; set; }
    public CashoutReferenceEntity? Reference { get; set; }
    public string ReferenceText { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal Fee { get; set; }
    public decimal Total { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string Merchant { get; set; } = string.Empty;
    public bool Refunded { get; set; }
    public DateTimeOffset NotificationDate { get; set; }
    public string? CustomData { get; set; }
    public string? SecurityKey { get; set; }
    public int? SecurityVersion { get; set; }
    public bool SignatureValid { get; set; }
    public int ResultCode { get; set; }
    public string? ResultMessage { get; set; }
    public DateTimeOffset ReceivedAtUtc { get; set; }
    public DateTimeOffset? ProcessedAtUtc { get; set; }
}
