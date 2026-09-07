using Microsoft.EntityFrameworkCore;
using WalletCash.Data;
using WalletCash.Data.Entities;

namespace WalletCash.Services;

public class CashoutReferenceRepository : ICashoutReferenceRepository
{
    private readonly CashoutDbContext _dbContext;

    public CashoutReferenceRepository(CashoutDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(CashoutReferenceEntity entity, CancellationToken cancellationToken = default)
    {
        _dbContext.References.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<CashoutReferenceEntity?> FindByReferenceAsync(string reference, CancellationToken cancellationToken = default) =>
        _dbContext.References.FirstOrDefaultAsync(r => r.Reference == reference, cancellationToken);

    public async Task<bool> MarkCancelledAsync(string reference, CancellationToken cancellationToken = default)
    {
        var entity = await FindByReferenceAsync(reference, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        entity.Status = CashoutReferenceStatus.Cancelled;
        entity.CancelledAtUtc = DateTimeOffset.UtcNow;
        entity.UpdatedAtUtc = DateTimeOffset.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
