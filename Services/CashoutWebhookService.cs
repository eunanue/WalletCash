using WalletCash.DTOs;

namespace WalletCash.Services;

public class CashoutWebhookService : ICashoutWebhookService
{
    private readonly ICashoutSignatureValidator _signatureValidator;
    private readonly ICashoutNotificationProcessor _notificationProcessor;
    private readonly ILogger<CashoutWebhookService> _logger;

    public CashoutWebhookService(
        ICashoutSignatureValidator signatureValidator,
        ICashoutNotificationProcessor notificationProcessor,
        ILogger<CashoutWebhookService> logger)
    {
        _signatureValidator = signatureValidator;
        _notificationProcessor = notificationProcessor;
        _logger = logger;
    }

    public async Task<WebhookResponse> ProcessNotificationAsync(
        CashoutNotificationRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!_signatureValidator.IsValid(request))
        {
            _logger.LogWarning("Invalid signature for notification {Id}.", request.Id);
            return new WebhookResponse { Rc = (int)CashoutNotificationResultCode.ValidationError, Msg = "Error de validación" };
        }

        var (code, message) = await _notificationProcessor.ProcessAsync(request, cancellationToken);

        return new WebhookResponse { Rc = (int)code, Msg = message };
    }
}
