using Microsoft.AspNetCore.Mvc;

namespace WalletCash.DTOs;

public class CashoutListQuery
{
    [FromQuery(Name = "page")]
    public int Page { get; set; } = 1;

    [FromQuery(Name = "limit")]
    public int Limit { get; set; } = 100;

    [FromQuery(Name = "reference")]
    public string? Reference { get; set; }

    [FromQuery(Name = "amount")]
    public decimal? Amount { get; set; }

    [FromQuery(Name = "start_date")]
    public DateOnly? StartDate { get; set; }

    [FromQuery(Name = "end_date")]
    public DateOnly? EndDate { get; set; }
}
