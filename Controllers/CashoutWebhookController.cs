using Microsoft.AspNetCore.Mvc;
using WalletCash.DTOs;
using WalletCash.Services;

namespace WalletCash.Controllers;

[ApiController]
[Route("v2/cashout/notifications")]
public class CashoutWebhookController : ControllerBase
{
    private readonly ICashoutWebhookService _webhookService;

    public CashoutWebhookController(ICashoutWebhookService webhookService)
    {
        _webhookService = webhookService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(WebhookResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> ReceiveNotificationAsync(
        [FromBody] CashoutNotificationRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _webhookService.ProcessNotificationAsync(request, cancellationToken);
        return Ok(result);
    }
}
