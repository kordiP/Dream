namespace Dream.Views.DeveloperViews
{
    public class DeveloperLoggedView
    {
        public ConsoleKey Key { get; set; }
        public string FullName { get; set; }
        public int Games { get; set; }
        public int Likes { get; set; } /*Indicates for how many likes their games have*/
        public int Downloads { get; set; } /*Indicates for how many downloads their games have*/
        public DeveloperLoggedView(string fullName, int games, int like, int downloads)
        {
            FullName = fullName;
            Games = games;
            Likes = like;
            Downloads = downloads;
            GetValues();
        }
        private void GetValues()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Dream game store");
            Console.WriteLine($"Developer profile of {FullName}");
            Console.WriteLine($"Games: {Games} -- Likes: {Likes} -- Downloads: {Downloads}");
            Console.WriteLine("1. Create new game");
            Console.WriteLine("2. Browse your games");
            Console.WriteLine("3. Edit profile info");
            Console.WriteLine("4. Delete profile");
            Console.WriteLine("5. Log out");
            Console.WriteLine("Esc. Exit");

            Key = Console.ReadKey(true).Key;
        }
        public void DeletedDeveloper(string name)
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"Successfully removed {name}");
        }
    }
}
