namespace Dream.Data.Models;

public class Like
{
    public Like()
    {
        this.Date = DateTime.UtcNow;
    }
    public int UserId { get; set; }

    public int GameId { get; set; }

    public DateTime Date { get; set; }

    public virtual Game Game { get; set; }

    public virtual User User { get; set; }
}
