using Microsoft.EntityFrameworkCore;
using WebApp.Dal.Models;

namespace WebApp.Dal.Seeders;

public class BaseDataBaseSeeder : IDataBaseSeeder
{
    private readonly WebAppDbContext _context;

    public BaseDataBaseSeeder(WebAppDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        _context.Database.Migrate();
        if (!_context.Accounts.Any())
        {
            _context.Accounts.Add(new AccountInfo
            {
                Login = "admin",
                PasswordHash = "admin",
                RegistrationDate = DateTime.UtcNow
            });
            _context.SaveChanges();
        }
    }
}