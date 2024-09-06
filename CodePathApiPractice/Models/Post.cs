namespace CodePathWebAPI.Models;

 public abstract class BaseDatetimeEntity
 {
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
 }
public partial class Post: BaseDatetimeEntity
{
    public int ID { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Active { get; set; } = false;
    public ApplicationUser? User { get; set; }
}