using System;
using System.Collections.Generic;

namespace Dream.Data.Models;

public class Game
{
    public int GameId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public double RequiredMemory { get; set; }

    public int GenreId { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Download> Downloads { get; } = new List<Download>();

    public virtual Genre Genre { get; set; } = null!;

    public virtual ICollection<Like> Likes { get; } = new List<Like>();

    public virtual ICollection<Developer> Developers { get; } = new List<Developer>();
}
