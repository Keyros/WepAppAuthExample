using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebApp.Mvc.Services.Interfaces;

namespace WebApp.Mvc.Services;

public class AuthService : IAuthService
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IUserService _userService;

    public AuthService(IHttpContextAccessor contextAccessor, IUserService userService)
    {
        _contextAccessor = contextAccessor;
        _userService = userService;
    }

    public async Task<bool> Login(string login, string password)
    {
        if (_contextAccessor.HttpContext == null)
        {
            return false;
        }

        if (login != "admin" || password != "admin")
        {
            return false;
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, login)
        };
        // создаем объект ClaimsIdentity
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        // установка аутентификационных куки
        await _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));
        return true;
    }

    public Task Logout()
        => _contextAccessor.HttpContext?.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme) ??
           Task.CompletedTask;


    public bool IsAuthenticated => _contextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
}