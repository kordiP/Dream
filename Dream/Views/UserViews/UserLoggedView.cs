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
        public UserLoggedView(User user)
        {
            Username = user.Username;

            if (user.Balance is null)
            {
                Balance = 0;
            }
            else
            {
                Balance = (decimal)user.Balance;
            }

            Likes = user.Likes.Count;
            Downloads = user.Downloads.Count;
            GetValues();
        }
        private void GetValues()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Dream game store");
            Console.WriteLine($"User profile of {Username}");
            Console.WriteLine($"Balance: {Balance:f2} -- Likes: {Likes} -- Downloads:{Downloads}");
            Console.WriteLine("1. Browse games");
            Console.WriteLine("2. Like game");
            Console.WriteLine("3. Download game");
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
