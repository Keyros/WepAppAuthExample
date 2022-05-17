using WebApp.Dal;
using WebApp.Dal.Models;
using WebApp.Mvc.Services.Interfaces;

namespace WebApp.Mvc.Services;

public class DataBaseUserStore : IUserStore
{
    public DataBaseUserStore(WebAppDbContext context)
    {
        _context = context;
    }

    private readonly WebAppDbContext _context;

    public IEnumerable<AccountInfo> GetAccounts()
    {
        return _context.Accounts.ToList();
    }
}