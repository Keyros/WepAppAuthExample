namespace WebApp.Dal.Models;

public class UserInfo : BaseEntity<int>
{
    public string Name { get; set; }
    public string Email { get; set; }
    
}