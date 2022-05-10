using Microsoft.EntityFrameworkCore;
using WebApp.Dal.Models;

namespace WebApp.Dal;

public class WebAppDbContext : DbContext
{
    public WebAppDbContext(DbContextOptions<WebAppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Note>? Notes { get; set; }
    public DbSet<UserInfo>? Users { get; set; }
    public DbSet<AccountInfo>? Accounts { get; set; }
    public DbSet<Role>? Roles { get; set; }
}