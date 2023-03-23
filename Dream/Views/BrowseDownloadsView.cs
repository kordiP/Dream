using Dream.Data.Models;

namespace Dream.Views
{
                /* --- Summary --- */
    /* --- This interface is responsible for --- */
    /* --- browsing downloaded games by users --- */

    public class BrowseDownloadsView
    {
        public IEnumerable<Download> Downloads { get; set; }
        public BrowseDownloadsView(IEnumerable<Download> ?downloads)
        {
            this.Downloads = downloads;
        }
        public void NoDownloads()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("\nNo games");
        }
        public void ShowDownloads()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"\nGames downloaded by {Downloads.First().User.Username}\n");
            foreach (var download in Downloads)
            {
                Console.WriteLine($"{download.Game.Name} - {download.Date}");
            }
            Console.WriteLine("\nPress any key to go back...");
            Console.ReadKey(true);
        }
    }
}
