using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dream.Data.Models;

public class Game
{
    [Key]
    public int GameId { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; }

    [Precision(12, 2)]
    public decimal Price { get; set; }

    [Precision(12, 2)]
    public double RequiredMemory { get; set; }

    public string? Description { get; set; }

    public int GenreId { get; set; }

    public virtual Genre Genre { get; set; }

    public virtual ICollection<Download> Downloads { get; set; }

    public virtual ICollection<Like> Likes { get; set; }
    public virtual ICollection<GameDeveloper> GameDevelopers { get; set; }
}
