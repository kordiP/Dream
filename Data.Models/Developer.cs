using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dream.Data.Models;

public class Developer
{
    public Developer()
    {
        this.GameDevelopers = new List<GameDeveloper>();
    }

    [Key]
    public int DeveloperId { get; set; }

    [Required, MaxLength(100)]
    public string Email { get; set; }

    [Required, MaxLength(50)]
    public string FirstName { get; set; }

    [Required, MaxLength(50)] 
    public string LastName { get; set; }

    public virtual ICollection<GameDeveloper> GameDevelopers { get; set; }
}
