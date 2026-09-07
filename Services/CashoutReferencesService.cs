using System.Net.Http.Json;
using WalletCash.Data.Entities;
using WalletCash.DTOs;

namespace WalletCash.Services;

public class CashoutReferencesService : ICashoutReferencesService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ICashoutReferenceRepository _referenceRepository;
    private readonly ILogger<CashoutReferencesService> _logger;

    public CashoutReferencesService(
        IHttpClientFactory httpClientFactory,
        ICashoutReferenceRepository referenceRepository,
        ILogger<CashoutReferencesService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _referenceRepository = referenceRepository;
        _logger = logger;
    }

    public async Task<CashoutReferenceResponse> CreateReferenceAsync(
        CashoutReferenceRequest request,
        CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient("CashoutApi");

        var response = await client.PostAsJsonAsync("v2/cashout/references/", request, cancellationToken);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<CashoutReferenceResponse>(cancellationToken);

        if (result is null)
        {
            _logger.LogError("Cashout API returned a null or unparseable response.");
            throw new InvalidOperationException("The cashout API returned an unexpected empty response.");
        }

        if (result.Data is not null)
        {
            await _referenceRepository.AddAsync(new CashoutReferenceEntity
            {
                Reference = result.Data.Reference,
                Status = CashoutReferenceStatus.Active,
                Amount = result.Data.Amount,
                Currency = result.Data.Currency,
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                CustomData = request.CustomData,
                ExpirationMinutes = request.ExpirationMinutes,
                CreationDate = result.Data.CreationDate,
                ExpirationDate = result.Data.ExpirationDate,
                CreatedAtUtc = DateTimeOffset.UtcNow,
                UpdatedAtUtc = DateTimeOffset.UtcNow,
            }, cancellationToken);
        }

        return result;
    }

    public async Task<CashoutReferenceResponse> GetReferenceAsync(
        string reference,
        CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient("CashoutApi");

        var response = await client.GetAsync($"v2/cashout/references/{reference}", cancellationToken);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<CashoutReferenceResponse>(cancellationToken);

        if (result is null)
        {
            _logger.LogError("Cashout API returned a null or unparseable response for reference {Reference}.", reference);
            throw new InvalidOperationException("The cashout API returned an unexpected empty response.");
        }

        return result;
    }

    public async Task<CashoutReferenceResponse> DeleteReferenceAsync(
        string reference,
        CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient("CashoutApi");

        var response = await client.DeleteAsync($"v2/cashout/references/{reference}", cancellationToken);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<CashoutReferenceResponse>(cancellationToken);

        if (result is null)
        {
            _logger.LogError("Cashout API returned a null or unparseable response when deleting reference {Reference}.", reference);
            throw new InvalidOperationException("The cashout API returned an unexpected empty response.");
        }

        await _referenceRepository.MarkCancelledAsync(reference, cancellationToken);

        return result;
    }
}
