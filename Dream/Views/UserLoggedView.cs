using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Views
{
    public class UserLoggedView
    {
        public ConsoleKey Key { get; set; }
        public string Username { get; set; }
        public UserLoggedView(string username)
        {
            this.Username = username;
            GetValues();
        }
        private void GetValues()
        {
            Console.WriteLine("Dream grame store");
            Console.WriteLine($"User profile of {this.Username}");
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
        public void Print(string result)
        {
            Console.WriteLine(result);
        }
    }
}
