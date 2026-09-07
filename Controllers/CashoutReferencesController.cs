using Microsoft.AspNetCore.Mvc;
using WalletCash.DTOs;
using WalletCash.Services;

namespace WalletCash.Controllers;

[ApiController]
[Route("v2/cashout/references")]
public class CashoutReferencesController : ControllerBase
{
    private readonly ICashoutReferencesService _cashoutReferencesService;

    public CashoutReferencesController(ICashoutReferencesService cashoutReferencesService)
    {
        _cashoutReferencesService = cashoutReferencesService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CashoutReferenceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    public async Task<IActionResult> CreateReferenceAsync(
        [FromBody] CashoutReferenceRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _cashoutReferencesService.CreateReferenceAsync(request, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{reference}")]
    [ProducesResponseType(typeof(CashoutReferenceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    public async Task<IActionResult> GetReferenceAsync(
        string reference,
        CancellationToken cancellationToken)
    {
        var result = await _cashoutReferencesService.GetReferenceAsync(reference, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{reference}")]
    [ProducesResponseType(typeof(CashoutReferenceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    public async Task<IActionResult> DeleteReferenceAsync(
        string reference,
        CancellationToken cancellationToken)
    {
        var result = await _cashoutReferencesService.DeleteReferenceAsync(reference, cancellationToken);
        return Ok(result);
    }
}
