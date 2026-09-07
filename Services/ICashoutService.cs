using WalletCash.DTOs;

namespace WalletCash.Services;

public interface ICashoutService
{
    Task<CashoutListResponse> GetCashoutsAsync(CashoutListQuery query, CancellationToken cancellationToken = default);
}
