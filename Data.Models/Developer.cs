using System;
using System.Collections.Generic;

namespace Dream.Data.Models;

public class Developer
{
    public int DeveloperId { get; set; }

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual ICollection<Game> Games { get; } = new List<Game>();
}
