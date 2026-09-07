using System.Net.Http.Json;
using System.Web;
using WalletCash.DTOs;

namespace WalletCash.Services;

public class CashoutService : ICashoutService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<CashoutService> _logger;

    public CashoutService(IHttpClientFactory httpClientFactory, ILogger<CashoutService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<CashoutListResponse> GetCashoutsAsync(
        CashoutListQuery query,
        CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient("CashoutApi");

        var qs = HttpUtility.ParseQueryString(string.Empty);
        qs["page"] = query.Page.ToString();
        qs["limit"] = query.Limit.ToString();

        if (!string.IsNullOrEmpty(query.Reference)) qs["reference"] = query.Reference;
        if (query.Amount.HasValue) qs["amount"] = query.Amount.Value.ToString("0.0");
        if (query.StartDate.HasValue) qs["start_date"] = query.StartDate.Value.ToString("yyyy-MM-dd");
        if (query.EndDate.HasValue) qs["end_date"] = query.EndDate.Value.ToString("yyyy-MM-dd");

        var response = await client.GetAsync($"v2/cashout?{qs}", cancellationToken);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<CashoutListResponse>(cancellationToken);

        if (result is null)
        {
            _logger.LogError("Cashout list API returned a null or unparseable response.");
            throw new InvalidOperationException("The cashout list API returned an unexpected empty response.");
        }

        return result;
    }
}
