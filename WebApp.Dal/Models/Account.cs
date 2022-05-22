namespace WebApp.Dal.Models;

public class Account : BaseEntity<int>
{
    public Account()
    {
        Roles = new HashSet<Role>();
        RefreshTokens = new HashSet<RefreshToken>();
    }

    public string Login { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public DateTime RegistrationDate { get; set; }
    public UserInfo? UserInfo { get; set; } = null!;
    public ICollection<RefreshToken> RefreshTokens { get; set; }
    public ICollection<Role> Roles { get; set; }
}