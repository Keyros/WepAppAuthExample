using WebApp.Dal.Models;
using WebApp.Mvc.Services.Interfaces;

namespace WebApp.Mvc.Services;

public class MemoryUserStore : IUserStore
{
    private readonly List<AccountInfo> _accountInfos = new();

    public MemoryUserStore()
    {
        _accountInfos.Add(new AccountInfo
        {
            Login = "admin",
            PasswordHash = "admin",
            Id = 1,
            RegistrationDate = DateTime.UtcNow
        });
    }


    public IEnumerable<AccountInfo> GetAccounts() => _accountInfos;
}