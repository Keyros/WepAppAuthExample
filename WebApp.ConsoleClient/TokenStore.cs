namespace WebApp.ConsoleClient;

public class TokenStore : ITokenStore
{
    private string? _refreshToken;
    private string? _token;

    public string GetRefreshToken() => _refreshToken ?? string.Empty;

    public void UpdateRefreshToken(string? refreshToken)
    {
        _refreshToken = refreshToken;
    }

    public string GetToken() => _token ?? string.Empty;

    public void UpdateToken(string? token)
    {
        _token = token;
    }
}