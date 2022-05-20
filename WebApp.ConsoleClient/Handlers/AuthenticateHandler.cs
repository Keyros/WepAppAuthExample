using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebApp.ConsoleClient.Handlers;

public class AuthenticateHandler : DelegatingHandler
{
    private readonly string _login;
    private readonly string _password;
    private readonly ITokenStore _tokenStore;

    public AuthenticateHandler(HttpMessageHandler innerHandler, string login, string password, ITokenStore tokenStore)
        : base(innerHandler)
    {
        _login = login;
        _password = password;
        _tokenStore = tokenStore;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenStore.GetToken());
        var response = await base.SendAsync(request, cancellationToken);

        if (NeedLogin(response))
        {
            using (response)
            {
                Console.WriteLine("Login");
                var loginResponse = await Login(request, cancellationToken);
                _tokenStore.UpdateRefreshToken(loginResponse.RefreshToken);
                _tokenStore.UpdateToken(loginResponse.Token);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Token);
                return await base.SendAsync(request, cancellationToken);
            }
        }

        return response;
    }

    private async Task<AuthenticatedResponse> Login(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var baseUri = $"{request.RequestUri!.Scheme}://{request.RequestUri.Authority}";
        var requestUri = new Uri($"{baseUri}/bearer/token?username={_login}&password={_password}");

        var loginRequestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri);
        using var responseMessage = await base.SendAsync(loginRequestMessage, cancellationToken);
        responseMessage.EnsureSuccessStatusCode();
        var loginResponse = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<AuthenticatedResponse>(loginResponse)!;
    }

    private static bool NeedLogin(HttpResponseMessage response)
        => response.StatusCode == HttpStatusCode.Unauthorized &&
           response.Headers.WwwAuthenticate.Count != 0 &&
           response.Headers.WwwAuthenticate.Any(x => x.Scheme == "Bearer" && x.Parameter == null);
}