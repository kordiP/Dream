using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Dream.Data.Models;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int Age { get; set; }

    public decimal? Balance { get; set; }

    public virtual ICollection<Download> Downloads { get; } = new List<Download>();

    public virtual ICollection<Like> Likes { get; } = new List<Like>();
}
