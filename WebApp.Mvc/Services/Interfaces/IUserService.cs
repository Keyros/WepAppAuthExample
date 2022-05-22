using System.Security.Claims;
using WebApp.Dal.Models;

namespace WebApp.Mvc.Services.Interfaces;

public interface IUserService
{
    Task<Account?> GetAccount(string login);
    Task<IEnumerable<Claim>> GetUserClaims(Account account);
    void AddRefreshToken(int accountId, string refresh, DateTime utcNow);
}