using System.Text.Json.Serialization;

namespace WalletCash.DTOs;

public class WebhookResponse
{
    [JsonPropertyName("rc")]
    public int Rc { get; set; }

    [JsonPropertyName("msg")]
    public string Msg { get; set; } = string.Empty;
}
