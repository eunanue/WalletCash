using System.Net.Http.Headers;
using System.Text;

namespace WalletCash.Services;

public class DappAuthHttpMessageHandler : DelegatingHandler
{
    private readonly string _apiKey;
    private readonly string _userAgent;

    public DappAuthHttpMessageHandler(IConfiguration configuration)
    {
        _apiKey = configuration["CashoutApi:ApiKey"] ?? string.Empty;
        _userAgent = configuration["CashoutApi:UserAgent"] ?? "WalletCashOut/1.0";
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_apiKey}:"));
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        request.Headers.UserAgent.Clear();
        request.Headers.UserAgent.ParseAdd(_userAgent);

        return base.SendAsync(request, cancellationToken);
    }
}
