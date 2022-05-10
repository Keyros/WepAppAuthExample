namespace WebApp.Dal.Models;

public class AccountInfo : BaseEntity<int>
{
    public string Login { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    
    public DateTime RegistrationDate { get; set; }
}