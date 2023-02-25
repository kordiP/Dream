using System;
using System.Collections.Generic;

namespace Dream.Data.Models;

public class Genre
{
    public int GenreId { get; set; }

    public string Name { get; set; } = null!;

    public int? AgeRequirements { get; set; }

    public virtual ICollection<Game> Games { get; } = new List<Game>();
}
