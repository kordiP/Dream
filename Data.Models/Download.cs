using System;
using System.Collections.Generic;

namespace Dream.Data.Models;

public class Download
{
    public int UserId { get; set; }

    public int GameId { get; set; }

    public DateTime Date { get; set; }

    public virtual Game Game { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
