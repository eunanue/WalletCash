using WalletCash.Data.Entities;

namespace WalletCash.Services;

public interface ICashoutReferenceRepository
{
    Task AddAsync(CashoutReferenceEntity entity, CancellationToken cancellationToken = default);

    Task<CashoutReferenceEntity?> FindByReferenceAsync(string reference, CancellationToken cancellationToken = default);

    Task<bool> MarkCancelledAsync(string reference, CancellationToken cancellationToken = default);
}
