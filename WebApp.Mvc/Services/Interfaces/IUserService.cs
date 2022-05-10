using System.Security.Claims;
using WebApp.Dal.Models;

namespace WebApp.Mvc.Services.Interfaces;

public interface IUserService
{
    Task<AccountInfo?> GetAccountInfo(string login);
    Task<IEnumerable<Claim>> GetUserClaims(AccountInfo accountInfo);
}