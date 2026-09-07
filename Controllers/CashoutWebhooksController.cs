using Microsoft.AspNetCore.Mvc;
using WalletCash.DTOs;
using WalletCash.Services;

namespace WalletCash.Controllers;

[ApiController]
[Route("v2/cashout/webhooks")]
public class CashoutWebhooksController : ControllerBase
{
    private readonly ICashoutWebhooksService _webhooksService;

    public CashoutWebhooksController(ICashoutWebhooksService webhooksService)
    {
        _webhooksService = webhooksService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CashoutWebhookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    public async Task<IActionResult> CreateWebhookAsync(
        [FromBody] WebhookRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _webhooksService.CreateWebhookAsync(request, cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CashoutWebhookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    public async Task<IActionResult> UpdateWebhookAsync(
        string id,
        [FromBody] WebhookRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _webhooksService.UpdateWebhookAsync(id, request, cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(CashoutWebhookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    public async Task<IActionResult> GetWebhookAsync(CancellationToken cancellationToken)
    {
        var result = await _webhooksService.GetWebhookAsync(cancellationToken);
        return Ok(result);
    }
}
