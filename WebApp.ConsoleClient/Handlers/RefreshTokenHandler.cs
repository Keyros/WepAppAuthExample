using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace WebApp.ConsoleClient.Handlers;

public class RefreshTokenHandler : DelegatingHandler
{
    private readonly ITokenStore _tokenStore;

    public RefreshTokenHandler(HttpMessageHandler innerHandler, ITokenStore tokenStore) : base(innerHandler)
    {
        _tokenStore = tokenStore;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        if (NeedUpdateTokens(response))
        {
            using (response)
            {
                Console.WriteLine("Refresh token");
                var refreshTokenResponse = await RefreshTokens(request, cancellationToken);
                _tokenStore.UpdateRefreshToken(refreshTokenResponse.RefreshToken);
                _tokenStore.UpdateToken(refreshTokenResponse.Token);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", refreshTokenResponse.Token);
                return await base.SendAsync(request, cancellationToken);
            }
        }

        return response;
    }

    private async Task<AuthenticatedResponse> RefreshTokens(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var baseUri = $"{request.RequestUri!.Scheme}://{request.RequestUri.Authority}";
        var requestUri = new Uri($"{baseUri}/bearer/refresh");

        var refreshTokenRequestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri);
        var refreshTokenRequest = new RefreshTokenRequest
        {
            Token = request.Headers.Authorization!.Parameter!,
            RefreshToken = _tokenStore.GetRefreshToken()
        };

        var body = new StringContent(JsonSerializer.Serialize(refreshTokenRequest), Encoding.UTF8,
            "application/json");
        refreshTokenRequestMessage.Content = body;
        using var responseMessage = await base.SendAsync(refreshTokenRequestMessage, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        var tokenResponse = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<AuthenticatedResponse>(tokenResponse)!;
    }

    private static bool NeedUpdateTokens(HttpResponseMessage response)
        => response.StatusCode == HttpStatusCode.Unauthorized &&
           response.Headers.TryGetValues("Token-Expired", out var values) &&
           bool.TryParse(values.FirstOrDefault(), out var isTokenExpired) &&
           isTokenExpired;
}