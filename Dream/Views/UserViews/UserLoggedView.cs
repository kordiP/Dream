using Dream.Data.Models;

namespace Dream.Views.UserViews
{
    public class UserLoggedView
    {
        public ConsoleKey Key { get; set; }
        public string Username { get; set; }
        public decimal Balance { get; set; }
        public int Likes { get; set; }
        public int Downloads { get; set; }
        public UserLoggedView(string Username, decimal Balance, int Downloads, int Likes)
        {
            this.Username = Username;
            this.Balance = Balance;
            this.Likes = Likes;
            this.Downloads = Downloads;
            GetValues();
        }
        private void GetValues()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("\nDream game store");
            Console.WriteLine($"\nUser profile of {Username}");
            Console.WriteLine($"*Balance: {Balance:f2} -- Likes: {Likes} -- Downloads: {Downloads}*");
            Console.WriteLine("\n1. Browse games");
            Console.WriteLine("2. Dislike/Like game");
            Console.WriteLine("3. Remove/Download game");
            Console.WriteLine("4. Browse liked games");
            Console.WriteLine("5. Browse downloaded games");
            Console.WriteLine("6. Edit profile info");
            Console.WriteLine("7. Delete profile");
            Console.WriteLine("8. Deposit money");
            Console.WriteLine("9. Log out");
            Console.WriteLine("Esc. Exit");

            Key = Console.ReadKey(true).Key;
        }
        public void DeletedUser(string username)
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"Successfully removed {username}");
        }
    }
}
