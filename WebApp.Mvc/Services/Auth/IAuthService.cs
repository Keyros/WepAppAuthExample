namespace WebApp.Mvc.Services.Auth;

public interface IAuthService
{
    Task<bool> Login(string login, string password);
    Task Logout();
    bool IsAuthenticated { get; }
    Task<AuthenticatedResponse?> GetToken(string login, string password);
}