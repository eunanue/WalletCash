using WalletCash.DTOs;

namespace WalletCash.Services;

public interface ICashoutSignatureValidator
{
    bool IsValid(CashoutNotificationRequest notification);
}
