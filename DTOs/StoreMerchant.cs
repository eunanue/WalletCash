using System.Text.Json.Serialization;

namespace WalletCash.DTOs;

public class StoreMerchant
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("image")]
    public string? Image { get; set; }

    [JsonPropertyName("category")]
    public StoreMerchantCategory Category { get; set; } = new();

    [JsonPropertyName("opening_time")]
    public string OpeningTime { get; set; } = string.Empty;

    [JsonPropertyName("closing_time")]
    public string ClosingTime { get; set; } = string.Empty;

    [JsonPropertyName("max_deposit_amount")]
    public decimal MaxDepositAmount { get; set; }

    [JsonPropertyName("max_withdrawal_amount")]
    public decimal MaxWithdrawalAmount { get; set; }

    [JsonPropertyName("cash_in_front_commission")]
    public decimal CashInFrontCommission { get; set; }

    [JsonPropertyName("cash_out_front_commission")]
    public decimal CashOutFrontCommission { get; set; }
}
