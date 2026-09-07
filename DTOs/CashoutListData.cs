using System.Text.Json.Serialization;

namespace WalletCash.DTOs;

public class CashoutListData
{
    [JsonPropertyName("total_count")]
    public int TotalCount { get; set; }

    [JsonPropertyName("pages")]
    public int Pages { get; set; }

    [JsonPropertyName("current_page")]
    public int CurrentPage { get; set; }

    [JsonPropertyName("has_next_page")]
    public bool HasNextPage { get; set; }

    [JsonPropertyName("list")]
    public List<CashoutListItem> List { get; set; } = [];
}
