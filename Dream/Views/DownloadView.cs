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
            Console.Write("\nSelect a game by typing its number: ");
            GameNumber = int.Parse(Console.ReadLine());
        }
        public void DownloadedGame(string gameName)
        {
            Console.WriteLine($"\nYou have successfully downloaded {gameName}");
        }
        public void RemovedGame(string gameName)
        {
            Console.WriteLine($"\nYou have successfully removed {gameName}");
        }
        public void InvalidGame()
        {
            Console.WriteLine("\nYou have used an invalid numer");
        }
        public void InvalidAge()
        {
            Console.WriteLine("\nYou are too young to play this game");
        }
        public void InvalidBalance()
        {
            Console.WriteLine("\nYou don't have enough money to play this game");
        }
    }
}
