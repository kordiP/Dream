using System.ComponentModel.DataAnnotations;

namespace Dream.Data.Models;

public class Genre
{
    public Genre()
    {
        this.Games = new List<Game>();
    }

    [Key]
    public int GenreId { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; }

    public int? AgeRequirements { get; set; }

    public virtual ICollection<Game> Games { get; set; }
}
