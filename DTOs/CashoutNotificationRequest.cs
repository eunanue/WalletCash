using System.Text.Json.Serialization;

namespace WalletCash.DTOs;

public class CashoutNotificationRequest
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("reference")]
    public string Reference { get; set; } = string.Empty;

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("fee")]
    public decimal Fee { get; set; }

    [JsonPropertyName("total")]
    public decimal Total { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;

    [JsonPropertyName("merchant")]
    public string Merchant { get; set; } = string.Empty;

    [JsonPropertyName("date")]
    public DateTimeOffset Date { get; set; }

    [JsonPropertyName("refunded")]
    public bool Refunded { get; set; }

    [JsonPropertyName("custom_data")]
    public string? CustomData { get; set; }

    [JsonPropertyName("security")]
    public CashoutNotificationSecurity Security { get; set; } = new();
}
