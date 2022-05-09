namespace WebApp.Dal.Models;

public class AccountInfo : BaseEntity<int>
{
    public string Login { get; set; }
    public string PasswordHash { get; set; }
}