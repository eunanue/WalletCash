namespace WalletCash.Data.Entities;

public enum CashoutReferenceStatus : byte
{
    Active = 1,
    Used = 2,
    Cancelled = 3,
    Expired = 4,
}
