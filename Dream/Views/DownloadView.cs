namespace Dream.Views
{
    public class DownloadView
    {
        public string Game { get; set; }
        public int GameNumber { get; set; }
        public IEnumerable<string> AllGames { get; set; }
        public DownloadView(IEnumerable<string> allGames)
        {
            AllGames = allGames;
            GetValues();
        }
        private void GetValues()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Select a game to download/remove");
            Console.WriteLine();
            Console.WriteLine(string.Join('\n', AllGames));
            Console.Write("Select a game by typing its number: ");
            GameNumber = int.Parse(Console.ReadLine());
        }
        public void DownloadedGame(string gameName)
        {
            Console.WriteLine($"You have successfully downloaded {gameName}");
        }
        public void RemovedGame(string gameName)
        {
            Console.WriteLine($"You have successfully removed {gameName}");
        }
        public void InvalidGame()
        {
            Console.WriteLine("You have used an invalid numer!");
        }
        public void InvalidAge()
        {
            Console.WriteLine("You are too yound to play this game!");
        }
        public void InvalidBalance()
        {
            Console.WriteLine("You don't have enough money to play this game!");
        }
    }
}
