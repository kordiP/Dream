using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Dream.Data.Models;

public class User
{
    public User()
    {
        this.Downloads = new List<Download>();
        this.Likes = new List<Like>();
    }

    [Key]
    public int UserId { get; set; }

    [Required, MaxLength(50)]
    public string Username { get; set; }

    [Required, MaxLength(100)]
    public string Email { get; set; }

    [Required, MaxLength(50)]
    public string FirstName { get; set; }

    [Required, MaxLength(50)]
    public string LastName { get; set; }

    [Required]
    public int Age { get; set; }

    [Precision(12, 2)]
    public decimal? Balance { get; set; }

    public virtual ICollection<Download> Downloads { get; set; }

    public virtual ICollection<Like> Likes { get; set; }
}
