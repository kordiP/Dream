namespace Dream.Views.DeveloperViews
{
    public class DeveloperLoggedView
    {
        public ConsoleKey Key { get; set; }
        public string FullName { get; set; }
        public DeveloperLoggedView(string fullName)
        {
            FullName = fullName;
            GetValues();
        }
        private void GetValues()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Dream game store");
            Console.WriteLine($"Developer profile of {FullName}");
            Console.WriteLine("1. Create new game");
            Console.WriteLine("2. Browse your games");
            Console.WriteLine("3. Edit profile info");
            Console.WriteLine("4. Delete profile");
            Console.WriteLine("5. Log out");
            Console.WriteLine("Esc. Exit");

            Key = Console.ReadKey(true).Key;
        }
    }
}
