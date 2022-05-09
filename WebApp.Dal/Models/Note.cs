namespace WebApp.Dal.Models;

public class Note : BaseEntity<int>
{
    public string Title { get; set; }
    public string Text { get; set; }
}