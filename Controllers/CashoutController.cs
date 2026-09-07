using Microsoft.AspNetCore.Mvc;
using WalletCash.DTOs;
using WalletCash.Services;

namespace WalletCash.Controllers;

[ApiController]
[Route("v2/cashout")]
public class CashoutController : ControllerBase
{
    private readonly ICashoutService _cashoutService;

    public CashoutController(ICashoutService cashoutService)
    {
        _cashoutService = cashoutService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(CashoutListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    public async Task<IActionResult> GetCashoutsAsync(
        [FromQuery] CashoutListQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _cashoutService.GetCashoutsAsync(query, cancellationToken);
        return Ok(result);
    }
}
