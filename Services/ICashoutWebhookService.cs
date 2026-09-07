using WalletCash.DTOs;

namespace WalletCash.Services;

public interface ICashoutWebhookService
{
    Task<WebhookResponse> ProcessNotificationAsync(CashoutNotificationRequest request, CancellationToken cancellationToken = default);
}
