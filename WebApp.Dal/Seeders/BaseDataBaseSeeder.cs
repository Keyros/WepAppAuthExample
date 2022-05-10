using Microsoft.EntityFrameworkCore;

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
    }
}