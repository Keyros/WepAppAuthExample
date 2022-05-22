namespace WebApp.Dal.Models;

public class Note : BaseEntity<int>
{
    public string Title { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTime CreationDate { get; set; }
}