
namespace Dream.Views
{
                /* --- Summary --- */
    /* --- This interface is responsible for --- */
        /* --- liking and disliking games --- */
            /* --- from users profiles --- */
    public class LikeView
    {
        public string Game { get; set; }
        public int GameNumber { get; set; }
        public IEnumerable<string> AllGames { get; set; }
        public LikeView(IEnumerable<string> allGames)
        {
            AllGames = allGames;
            GetValues();
        }
        private void GetValues()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Select a game to like/dislike");
            Console.WriteLine();
            Console.WriteLine(string.Join('\n', AllGames));
            Console.Write("\nSelect a game by typing its number: ");
            GameNumber = int.Parse(Console.ReadLine());
        }
        public void LikedGame(string gameName)
        {
            Console.WriteLine($"\nYou have successfully liked {gameName}");
        }
        public void RemovedGame(string gameName)
        {
            Console.WriteLine($"\nYou have successfully disliked {gameName}");
        }
        public void InvalidGame()
        {
            Console.WriteLine("\nYou have used an invalid numer!");
        }
    }
}
