using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebApp.Mvc.Services.Interfaces;

namespace WebApp.Mvc.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public AuthService(IHttpContextAccessor contextAccessor, IUserService userService, ITokenService tokenService)
    {
        _contextAccessor = contextAccessor;
        _userService = userService;
        _tokenService = tokenService;
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

    public async Task<AuthenticatedResponse?> GetToken(string login, string password)
    {
        var claims = await GetUserClaims(login, password);

        if (claims == null)
        {
            return default;
        }

        var claimsIdentity =
            new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme, ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

        var encodedJwt = _tokenService.GenerateAccessToken(claimsIdentity.Claims);
        var refreshToken = _tokenService.GenerateRefreshToken();
        var accountInfo = await _userService.GetAccountInfo(login);
        accountInfo!.RefreshToken = refreshToken;
        accountInfo.RefreshTokenLifeTime = DateTime.UtcNow;

        return new AuthenticatedResponse
        {
            RefreshToken = refreshToken,
            Token = encodedJwt
        };
    }

    public async Task<AuthenticatedResponse?> RefreshTokens(RefreshTokenRequest refreshTokenRequest)
    {
        var claimsPrincipal = _tokenService.GetPrincipalFromExpiredToken(refreshTokenRequest.Token!);
        if (claimsPrincipal == null)
        {
            return null;
        }

        var name = claimsPrincipal.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);

        var accountInfo = await _userService.GetAccountInfo(name);
        if (accountInfo == null)
        {
            return null;
        }

        if (!string.Equals(accountInfo.RefreshToken, refreshTokenRequest.RefreshToken,
                StringComparison.InvariantCultureIgnoreCase))
        {
            accountInfo.RefreshToken = null;
            accountInfo.RefreshTokenLifeTime = null;
            return null;
        }

        if (DateTime.UtcNow - accountInfo.RefreshTokenLifeTime >= TimeSpan.FromMinutes(1))
        {
            accountInfo.RefreshToken = null;
            accountInfo.RefreshTokenLifeTime = null;
            return null;
        }

        var token = _tokenService.GenerateAccessToken(claimsPrincipal.Claims);
        var refresh = _tokenService.GenerateRefreshToken();
        
        accountInfo.RefreshToken = refresh;
        accountInfo.RefreshTokenLifeTime = DateTime.UtcNow;

        return new AuthenticatedResponse
        {
            RefreshToken = refresh,
            Token = token
        };
    }
}