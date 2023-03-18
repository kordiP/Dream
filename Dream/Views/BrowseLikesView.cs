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
            Console.WriteLine("\nNo games");
        }
        public void ShowLikes()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"\nGames liked by {Likes.First().User.Username}\n");
            foreach (var like in Likes)
            {
                Console.WriteLine($"{like.Game.Name} - {like.Date}");
            }
            Console.WriteLine("\nPress any key to go back...");
            Console.ReadKey(true);
        }
    }
}
