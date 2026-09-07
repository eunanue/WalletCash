using System.Text.Json.Serialization;

namespace WalletCash.DTOs;

public class WebhookData
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;
}
