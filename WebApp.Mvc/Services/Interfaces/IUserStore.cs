using WebApp.Dal.Models;

namespace WebApp.Mvc.Services.Interfaces;

public interface IUserStore
{
    IEnumerable<AccountInfo> GetAccounts();
}