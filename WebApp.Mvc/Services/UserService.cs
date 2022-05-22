using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using WebApp.Dal;
using WebApp.Dal.Models;
using WebApp.Mvc.Services.Interfaces;

namespace WebApp.Mvc.Services;

public class UserService : IUserService
{
    private readonly WebAppDbContext _webAppDbContext;

    public UserService(WebAppDbContext webAppDbContext)
    {
        _webAppDbContext = webAppDbContext;
    }


    public async Task<Account?> GetAccount(string login)
    {
        var item = await _webAppDbContext.Accounts.FirstAsync(x => x.Login == login);
        return item;
    }

    public Task<IEnumerable<Claim>> GetUserClaims(Account account)
    {
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, account.Login),
        };

        if (account.Login == "admin")
        {
            claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, "Admin"));
            claims.Add(new Claim("EvaluatedUsers", "true"));
        }

        return Task.FromResult<IEnumerable<Claim>>(claims);
    }

    public void AddRefreshToken(int id, string refresh, DateTime utcNow)
    {
        _webAppDbContext.RefreshTokens.Add(new RefreshToken
        {
            AccountId = id,
            Token = refresh,
            RefreshTokenLifeTime = utcNow,
        });
        _webAppDbContext.SaveChanges();
    }

    public async Task<Account?> GetAccountWithTokens(string login)
    {
        var item = await _webAppDbContext.Accounts
            .Include(x => x.RefreshTokens)
            .FirstAsync(x => x.Login == login);
        return item;
    }
}