namespace WebApp.ConsoleClient;

public interface ITokenStore
{
    string GetRefreshToken();
    void UpdateRefreshToken(string? refreshToken);
    string GetToken();
    void UpdateToken(string? token);
}