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
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
}