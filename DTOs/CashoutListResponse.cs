using System.Text.Json.Serialization;

namespace WalletCash.DTOs;

public class CashoutListResponse
{
    [JsonPropertyName("rc")]
    public int Rc { get; set; }

    [JsonPropertyName("msg")]
    public string Msg { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public CashoutListData? Data { get; set; }
}
