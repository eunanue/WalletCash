using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WalletCash.Data;
using WalletCash.Data.Entities;
using WalletCash.DTOs;

namespace WalletCash.Services;

public class CashoutNotificationProcessor : ICashoutNotificationProcessor
{
    private readonly CashoutDbContext _dbContext;

    public CashoutNotificationProcessor(CashoutDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(CashoutNotificationResultCode Code, string Message)> ProcessAsync(
        CashoutNotificationRequest notification,
        CancellationToken cancellationToken = default)
    {
        var reference = await _dbContext.References
            .FirstOrDefaultAsync(r => r.Reference == notification.Reference, cancellationToken);

        var (resultCode, message) = Evaluate(reference, notification);

        var entity = new CashoutNotificationEntity
        {
            DappNotificationId = notification.Id,
            ReferenceId = reference?.Id,
            ReferenceText = notification.Reference,
            Amount = notification.Amount,
            Fee = notification.Fee,
            Total = notification.Total,
            Currency = notification.Currency,
            Merchant = notification.Merchant,
            Refunded = notification.Refunded,
            NotificationDate = notification.Date,
            CustomData = notification.CustomData,
            SecurityKey = notification.Security.Key,
            SecurityVersion = notification.Security.Version,
            SignatureValid = true,
            ResultCode = (int)resultCode,
            ResultMessage = message,
            ReceivedAtUtc = DateTimeOffset.UtcNow,
        };

        _dbContext.Notifications.Add(entity);

        if (resultCode == CashoutNotificationResultCode.Success && reference is not null)
        {
            entity.ProcessedAtUtc = DateTimeOffset.UtcNow;
            reference.Status = CashoutReferenceStatus.Used;
            reference.UpdatedAtUtc = DateTimeOffset.UtcNow;
        }

        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex) when (IsDuplicateNotification(ex))
        {
            // Dapp reintentó la misma notificación (DappNotificationId ya existe): no reprocesar el estado
            // de la referencia, solo confirmar de nuevo el éxito para que Dapp no siga reintentando.
            return (CashoutNotificationResultCode.Success, "Operación exitosa");
        }

        return (resultCode, message);
    }

    private static (CashoutNotificationResultCode Code, string Message) Evaluate(
        CashoutReferenceEntity? reference,
        CashoutNotificationRequest notification)
    {
        if (reference is null)
        {
            return (CashoutNotificationResultCode.InvalidReference, "Referencia no encontrada.");
        }

        if (reference.Status == CashoutReferenceStatus.Cancelled)
        {
            return (CashoutNotificationResultCode.ReferenceCancelled, "La referencia fue cancelada.");
        }

        if (reference.Status == CashoutReferenceStatus.Used)
        {
            return (CashoutNotificationResultCode.ReferenceAlreadyUsed, "La referencia ya fue utilizada.");
        }

        if (reference.Amount != notification.Amount)
        {
            return (CashoutNotificationResultCode.InvalidReferenceAmount, "El monto no coincide con la referencia.");
        }

        return (CashoutNotificationResultCode.Success, "Operación exitosa");
    }

    private static bool IsDuplicateNotification(DbUpdateException ex) =>
        ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627);
}
