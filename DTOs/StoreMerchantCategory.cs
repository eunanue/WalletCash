using System.Text.Json.Serialization;

namespace WalletCash.DTOs;

public class StoreMerchantCategory
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}
