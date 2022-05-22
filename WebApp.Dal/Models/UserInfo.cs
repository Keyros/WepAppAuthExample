namespace WebApp.Dal.Models;

public class UserInfo : BaseEntity<int>
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public ICollection<Note>? Notes { get; set; }
}