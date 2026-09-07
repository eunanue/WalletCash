using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using WalletCash.DTOs;

namespace WalletCash.Services;

public class CashoutSignatureValidator : ICashoutSignatureValidator
{
    private readonly string _publicKeyPem;

    public CashoutSignatureValidator(IConfiguration configuration)
    {
        var relativePath = configuration["CashoutWebhook:PublicKeyPath"]
            ?? throw new InvalidOperationException("CashoutWebhook:PublicKeyPath is not configured.");

        _publicKeyPem = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, relativePath));
    }

    public bool IsValid(CashoutNotificationRequest notification)
    {
        byte[] signatureBytes;
        try
        {
            signatureBytes = Convert.FromBase64String(notification.Security.Signature);
        }
        catch (FormatException)
        {
            return false;
        }

        var payload = string.Join('|',
            notification.Id,
            notification.Reference,
            notification.Amount.ToString(CultureInfo.InvariantCulture),
            notification.Fee.ToString(CultureInfo.InvariantCulture),
            notification.Currency,
            notification.Refunded,
            notification.Date.ToString("O"));

        using var rsa = RSA.Create();
        rsa.ImportFromPem(_publicKeyPem);

        return rsa.VerifyData(
            Encoding.UTF8.GetBytes(payload),
            signatureBytes,
            HashAlgorithmName.SHA512,
            RSASignaturePadding.Pkcs1);
    }
}
