using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApp.Mvc.Authorization.Bearer;
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

        var claims = await GetUserClaims(login, password);

        if (claims == null)
        {
            return false;
        }

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme,
            ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);

        await _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));
        return true;
    }

    private async Task<IEnumerable<Claim>?> GetUserClaims(string login, string password)
    {
        var user = await _userService.GetAccountInfo(login);
        if (user == null)
        {
            return null;
        }

        if (user.PasswordHash != password)
        {
            return null;
        }

        return await _userService.GetUserClaims(user);
    }


    public Task Logout()
        => _contextAccessor.HttpContext?.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme) ??
           Task.CompletedTask;


    public bool IsAuthenticated => _contextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;

    public async Task<(string token, string name)> GetToken(string login, string password)
    {
        var claims = await GetUserClaims(login, password);

        if (claims == null)
        {
            return default;
        }

        var claimsIdentity =
            new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme, ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);


        var now = DateTime.UtcNow;
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: now,
            claims: claimsIdentity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return (encodedJwt, claimsIdentity.Name ?? string.Empty);
    }
}