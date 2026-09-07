using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WalletCash.DTOs;

public class CashoutReferenceRequest
{
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("expiration_minutes")]
    public int? ExpirationMinutes { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [EmailAddress]
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("custom_data")]
    public string? CustomData { get; set; }
}
