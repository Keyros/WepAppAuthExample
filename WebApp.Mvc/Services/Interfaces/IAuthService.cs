namespace WebApp.Mvc.Services.Interfaces;

public interface IAuthService
{
    Task<bool> Login(string login, string password);
    Task Logout();
    bool IsAuthenticated { get; }
    Task<(string token, string name)>  GetToken(string login, string password);
}