namespace Dream.Views
{
                    /* --- Summary --- */
        /* --- This interface is responsible for --- */
    /* --- browsing games without any registration --- */

    public class BrowseGamesView
    {
        public BrowseGamesView()
        { }
        public void AllGamesList(IEnumerable<string> games)
        {
            foreach (var game in games) 
            {
                Console.WriteLine(game + Environment.NewLine);
            }
        }
        public void MostDownloadedGame(string game)
        {
            Console.WriteLine($"Most downloaded game: {game}\n");
        }
        public void MostLikedGame(string game)
        {
            Console.WriteLine($"Most likes game: {game}");
        }
        public void MostPopularGenre(string genre)
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"\nMost popular genre: {genre}");
        }
        public void ExitView()
        {
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey(true);
        }
        public void NoGamesException()
        {
            Console.WriteLine("No games have been added yet.");
        }
    }
}
