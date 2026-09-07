namespace WalletCash.DTOs;

public enum CashoutNotificationResultCode
{
    Success = 0,
    InvalidReference = -10,
    ReferenceAlreadyUsed = -11,
    ReferenceCancelled = -12,
    InvalidReferenceAmount = -13,
    ValidationError = -20,
    MaxAmountPerTransactionLimit = -21,
    TransactionCountLimit = -22,
    AccountAmountLimit = -23,
    ServiceUnavailable = -30,
    TransactionAlreadyProcessed = -50,
    Other = -99,
}
