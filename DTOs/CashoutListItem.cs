using System.Text.Json.Serialization;

namespace WalletCash.DTOs;

public class CashoutListItem
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

    [JsonPropertyName("refunded")]
    public bool Refunded { get; set; }

    [JsonPropertyName("date")]
    public DateTimeOffset Date { get; set; }

    [JsonPropertyName("auth_number")]
    public string AuthNumber { get; set; } = string.Empty;

    [JsonPropertyName("merchant")]
    public string Merchant { get; set; } = string.Empty;

    [JsonPropertyName("merchant_id")]
    public string MerchantId { get; set; } = string.Empty;

    [JsonPropertyName("store_id")]
    public string StoreId { get; set; } = string.Empty;

    [JsonPropertyName("custom_data")]
    public string? CustomData { get; set; }
}
