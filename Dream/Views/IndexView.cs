using Dream.Controllers;
using Dream.Data.Models;
using Dream.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Views
{
    public class IndexView
    {
        public ConsoleKey Key { get; set; }
        public IndexView()
        {
            GetValues(); 
        }
        private void GetValues()
        {
            Console.WriteLine("Dream grame store");
            Console.WriteLine("1. Sign up as user");
            Console.WriteLine("2. Sign up as developer");
            Console.WriteLine("3. Sign in");
            Console.WriteLine("4. Browse games");
            Console.WriteLine("Esc. Exit");

            Key = Console.ReadKey(true).Key;
        }
        public void Print(string result)
        {
            Console.WriteLine(result);
        }
    }
}
