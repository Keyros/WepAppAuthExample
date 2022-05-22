namespace WebApp.Dal.Models;

public class Role : BaseEntity<int>
{
    public string Name { get; set; } = null!;
    public ICollection<Account>? AccountInfos { get; set; }
}