using WalletCash.DTOs;

namespace WalletCash.Services;

public interface ICashoutWebhooksService
{
    Task<CashoutWebhookResponse> CreateWebhookAsync(WebhookRequest request, CancellationToken cancellationToken = default);
    Task<CashoutWebhookResponse> UpdateWebhookAsync(string id, WebhookRequest request, CancellationToken cancellationToken = default);
    Task<CashoutWebhookResponse> GetWebhookAsync(CancellationToken cancellationToken = default);
}
