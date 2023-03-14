namespace Dream.Views
{
    public class BrowsingGamesView
    {
        public BrowsingGamesView()
        { }
        public void AllGamesList(IEnumerable<string> games)
        {
            Console.WriteLine(new string('-', 50));
            foreach (var game in games) 
            {
                Console.WriteLine(game + Environment.NewLine);
            }
        }
        public void MostDownloadedGame(string game)
        {
            Console.WriteLine($"Most downloaded game: {game}");
        }
        public void MostLikedGame(string game)
        {
            Console.WriteLine($"Most likes game: {game}");
        }
        public void MostPopularGenre(string genre)
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"Most popular genre: {genre}");
        }
        public void ExitView()
        {
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey(true);
        }
    }
}
