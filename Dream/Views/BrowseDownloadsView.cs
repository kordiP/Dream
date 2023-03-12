using Dream.Data.Models;

namespace Dream.Views
{
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
            Console.WriteLine("No games");
        }
        public void ShowDownloads()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"Games downloaded by {Downloads.First().User.Username}");
            foreach (var download in Downloads)
            {
                Console.WriteLine($"{download.Game.Name} - {download.Date}");
            }
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey(true);
        }
    }
}
