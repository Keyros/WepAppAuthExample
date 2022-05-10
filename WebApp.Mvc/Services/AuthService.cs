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

        var user = await _userService.GetAccountInfo(login);
        if (user == null)
        {
            return false;
        }

        if (user.PasswordHash != password)
        {
            return false;
        }

        var claims = await _userService.GetUserClaims(user);


        var claimsIdentity = new ClaimsIdentity(claims, "Cookies", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

        await _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));
        return true;
    }

    public Task Logout()
        => _contextAccessor.HttpContext?.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme) ??
           Task.CompletedTask;


    public bool IsAuthenticated => _contextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
}