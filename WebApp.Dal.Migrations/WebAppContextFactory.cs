using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WebApp.Dal.Migrations;

// ReSharper disable once UnusedType.Global
public class WebAppContextFactory : IDesignTimeDbContextFactory<WebAppDbContext>
{
    public WebAppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<WebAppDbContext>();
        optionsBuilder.UseSqlite("Data Source=WebApp.db");

        return new WebAppDbContext(optionsBuilder.Options);
    }
}