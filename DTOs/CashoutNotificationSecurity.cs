using System.Text.Json.Serialization;

namespace WalletCash.DTOs;

public class CashoutNotificationSecurity
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    [JsonPropertyName("version")]
    public int Version { get; set; }

    [JsonPropertyName("signature")]
    public string Signature { get; set; } = string.Empty;
}
