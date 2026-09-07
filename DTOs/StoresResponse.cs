using System.Text.Json.Serialization;

namespace WalletCash.DTOs;

public class StoresResponse
{
    [JsonPropertyName("rc")]
    public int Rc { get; set; }

    [JsonPropertyName("msg")]
    public string Msg { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public List<StoreData>? Data { get; set; }
}
