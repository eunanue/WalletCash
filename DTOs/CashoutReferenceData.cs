using System.Text.Json.Serialization;

namespace WalletCash.DTOs;

public class CashoutReferenceData
{
    [JsonPropertyName("reference")]
    public string Reference { get; set; } = string.Empty;

    [JsonPropertyName("creation_date")]
    public DateTimeOffset CreationDate { get; set; }

    [JsonPropertyName("expiration_date")]
    public DateTimeOffset ExpirationDate { get; set; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;
}
