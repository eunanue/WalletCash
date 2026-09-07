using System.Text.Json.Serialization;

namespace WalletCash.DTOs;

public class StoreResponse
{
    [JsonPropertyName("rc")]
    public int Rc { get; set; }

    [JsonPropertyName("msg")]
    public string Msg { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public StoreData? Data { get; set; }
}
