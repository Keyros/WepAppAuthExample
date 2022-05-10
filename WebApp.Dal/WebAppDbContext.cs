using Microsoft.EntityFrameworkCore;
using WebApp.Dal.Models;

namespace WebApp.Dal;

public class WebAppDbContext : DbContext
{
    public WebAppDbContext(DbContextOptions<WebAppDbContext> options) : base(options)
    {
    }

    public DbSet<Note> Notes { get; set; } = null!;
    public DbSet<UserInfo> Users { get; set; } = null!;
    public DbSet<AccountInfo> Accounts { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
}