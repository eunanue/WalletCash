using System.Net.Http.Json;
using WalletCash.DTOs;

namespace WalletCash.Services;

public class CashoutWebhooksService : ICashoutWebhooksService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<CashoutWebhooksService> _logger;

    public CashoutWebhooksService(IHttpClientFactory httpClientFactory, ILogger<CashoutWebhooksService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<CashoutWebhookResponse> CreateWebhookAsync(
        WebhookRequest request,
        CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient("CashoutApi");

        var response = await client.PostAsJsonAsync("v2/cashout/webhooks", request, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await ReadResponseAsync(response, cancellationToken);
    }

    public async Task<CashoutWebhookResponse> UpdateWebhookAsync(
        string id,
        WebhookRequest request,
        CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient("CashoutApi");

        var response = await client.PutAsJsonAsync($"v2/cashout/webhooks/{id}", request, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await ReadResponseAsync(response, cancellationToken);
    }

    public async Task<CashoutWebhookResponse> GetWebhookAsync(CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient("CashoutApi");

        var response = await client.GetAsync("v2/cashout/webhooks", cancellationToken);
        response.EnsureSuccessStatusCode();

        return await ReadResponseAsync(response, cancellationToken);
    }

    private async Task<CashoutWebhookResponse> ReadResponseAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var result = await response.Content.ReadFromJsonAsync<CashoutWebhookResponse>(cancellationToken);

        if (result is null)
        {
            _logger.LogError("Cashout webhooks API returned a null or unparseable response.");
            throw new InvalidOperationException("The cashout webhooks API returned an unexpected empty response.");
        }

        return result;
    }
}
