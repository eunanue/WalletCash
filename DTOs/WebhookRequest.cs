using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WalletCash.DTOs;

public class WebhookRequest
{
    [Required]
    [Url]
    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}
