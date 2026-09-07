using Microsoft.AspNetCore.Mvc;
using WalletCash.DTOs;
using WalletCash.Services;

namespace WalletCash.Controllers;

[ApiController]
[Route("v2/stores")]
public class StoresController : ControllerBase
{
    private readonly IStoresService _storesService;

    public StoresController(IStoresService storesService)
    {
        _storesService = storesService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(StoresResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    public async Task<IActionResult> GetStoresAsync(
        [FromQuery] StoresQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _storesService.GetStoresAsync(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{storeId}")]
    [ProducesResponseType(typeof(StoreResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    public async Task<IActionResult> GetStoreAsync(
        string storeId,
        CancellationToken cancellationToken)
    {
        var result = await _storesService.GetStoreAsync(storeId, cancellationToken);
        return Ok(result);
    }
}
