using WalletCash.DTOs;

namespace WalletCash.Services;

public interface ICashoutReferencesService
{
    Task<CashoutReferenceResponse> CreateReferenceAsync(CashoutReferenceRequest request, CancellationToken cancellationToken = default);
    Task<CashoutReferenceResponse> GetReferenceAsync(string reference, CancellationToken cancellationToken = default);
    Task<CashoutReferenceResponse> DeleteReferenceAsync(string reference, CancellationToken cancellationToken = default);
}
