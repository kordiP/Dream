using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Views
{
    public class IndexView
    {
        private ConsoleKey key;
        public IndexView()
        { IndexViewCommands(); }
        private void IndexViewCommands()
        {
            Console.WriteLine("Dream grame store");
            Console.WriteLine("1. Sign up");
            Console.WriteLine("2. Sign in");
            Console.WriteLine("3. Browse games");
            Console.WriteLine("Esc. Exit");

            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.NumPad1 or ConsoleKey.D1:
                    Console.WriteLine(new string('-', 50));
                    Console.WriteLine("Sign up");
                    break;
                case ConsoleKey.NumPad2 or ConsoleKey.D2:
                    Console.WriteLine(new string('-', 50));
                    Console.WriteLine("Sign in");
                    break;
                case ConsoleKey.NumPad3 or ConsoleKey.D3:
                    Console.WriteLine(new string('-', 50));
                    Console.WriteLine("Browse games");
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine(new string('-', 50));
                    IndexViewCommands();
                    break;
            }
        }
    }
}
