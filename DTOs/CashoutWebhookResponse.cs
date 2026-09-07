using System.Text.Json.Serialization;

namespace WalletCash.DTOs;

public class CashoutWebhookResponse
{
    [JsonPropertyName("rc")]
    public int Rc { get; set; }

    [JsonPropertyName("msg")]
    public string Msg { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public WebhookData? Data { get; set; }
}
