using WalletCash.DTOs;

namespace WalletCash.Services;

public interface IStoresService
{
    Task<StoresResponse> GetStoresAsync(StoresQuery query, CancellationToken cancellationToken = default);
    Task<StoreResponse> GetStoreAsync(string storeId, CancellationToken cancellationToken = default);
}
