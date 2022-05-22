namespace WebApp.Dal.Models;

public class RefreshToken : BaseEntity<int>
{
    public int AccountId { get; set; }
    public string? Token { get; set; }
    public DateTime? RefreshTokenLifeTime { get; set; }
}