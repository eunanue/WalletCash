using WalletCash.DTOs;

namespace WalletCash.Services;

public interface ICashoutNotificationProcessor
{
    Task<(CashoutNotificationResultCode Code, string Message)> ProcessAsync(
        CashoutNotificationRequest notification,
        CancellationToken cancellationToken = default);
}
