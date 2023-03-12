using Dream.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Dream.Views
{
    public class BrowseLikesView
    {
        public IEnumerable<Like> Likes { get; set; }
        public BrowseLikesView(IEnumerable<Like> ?likes)
        {
            this.Likes = likes;
        }
        public void NoLikes()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("No games");
        }
        public void ShowLikes()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"Games liked by {Likes.First().User.Username}");
            foreach (var like in Likes)
            {
                Console.WriteLine($"{like.Game.Name} - {like.Date}");
            }
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey(true);
        }
    }
}
