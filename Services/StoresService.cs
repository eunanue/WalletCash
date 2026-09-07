using System.Net.Http.Json;
using System.Web;
using WalletCash.DTOs;

namespace WalletCash.Services;

public class StoresService : IStoresService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<StoresService> _logger;

    public StoresService(IHttpClientFactory httpClientFactory, ILogger<StoresService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<StoresResponse> GetStoresAsync(
        StoresQuery query,
        CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient("CashoutApi");

        var qs = HttpUtility.ParseQueryString(string.Empty);
        qs["latitude"] = query.Latitude.ToString();
        qs["longitude"] = query.Longitude.ToString();
        qs["radius"] = query.Radius.ToString();

        var response = await client.GetAsync($"v2/stores?{qs}", cancellationToken);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<StoresResponse>(cancellationToken);

        if (result is null)
        {
            _logger.LogError("Stores API returned a null or unparseable response.");
            throw new InvalidOperationException("The stores API returned an unexpected empty response.");
        }

        return result;
    }

    public async Task<StoreResponse> GetStoreAsync(
        string storeId,
        CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient("CashoutApi");

        var response = await client.GetAsync($"v2/stores/{storeId}", cancellationToken);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<StoreResponse>(cancellationToken);

        if (result is null)
        {
            _logger.LogError("Stores API returned a null or unparseable response for store {StoreId}.", storeId);
            throw new InvalidOperationException("The stores API returned an unexpected empty response.");
        }

        return result;
    }
}
