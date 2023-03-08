using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Views
{
    public class DeveloperLoggedView
    {
        public ConsoleKey Key { get; set; }
        public string FullName { get; set; }
        public DeveloperLoggedView(string fullName)
        {
            this.FullName = fullName;
            GetValues();
        }
        private void GetValues()
        {
            Console.WriteLine("Dream grame store");
            Console.WriteLine($"Developer profile of {this.FullName}");
            Console.WriteLine("1. Create new game");
            Console.WriteLine("2. Browse your games");
            Console.WriteLine("3. Edit profile info");
            Console.WriteLine("4. Delete profile");
            Console.WriteLine("5. Log out");
            Console.WriteLine("Esc. Exit");

            Key = Console.ReadKey(true).Key;
        }
        public void Print(string result)
        {
            Console.WriteLine(result);
        }

    }
}
