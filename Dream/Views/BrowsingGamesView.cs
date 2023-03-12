namespace Dream.Views
{
    public class BrowsingGamesView
    {
        public IEnumerable<string> Games { get; set; }
        public BrowsingGamesView(IEnumerable<string> games)
        {
            this.Games = games;
        }
        public void AllGamesList()
        {
            Console.WriteLine();
            foreach (var game in Games) 
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
