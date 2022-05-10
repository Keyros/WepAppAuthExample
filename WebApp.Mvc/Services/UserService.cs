using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using WebApp.Dal;
using WebApp.Dal.Models;
using WebApp.Mvc.Services.Interfaces;

namespace WebApp.Mvc.Services;

public class UserService : IUserService
{
    private readonly WebAppDbContext _context;

    public UserService(WebAppDbContext context)
    {
        _context = context;
    }

    public Task<AccountInfo?> GetAccountInfo(string login)
        => _context.Accounts.FirstOrDefaultAsync(x => x.Login == login);

    public Task<IEnumerable<Claim>> GetUserClaims(AccountInfo accountInfo)
    {
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, accountInfo.Login),
        };

        if (accountInfo.Login == "admin")
        {
            claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, "Admin"));
            claims.Add(new Claim("EvaluatedUsers", "true"));
        }

        return Task.FromResult<IEnumerable<Claim>>(claims);
    }
}