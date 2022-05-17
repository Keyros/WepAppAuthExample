using System.Security.Claims;
using WebApp.Dal.Models;
using WebApp.Mvc.Services.Interfaces;

namespace WebApp.Mvc.Services;

public class UserService : IUserService
{
    private readonly IUserStore _userStore;

    public UserService(IUserStore userStore)
    {
        _userStore = userStore;
    }


    public Task<AccountInfo?> GetAccountInfo(string login)
    {
        var item = _userStore.GetAccounts().FirstOrDefault(x => x.Login == login);
        return Task.FromResult(item);
    }

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