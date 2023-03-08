﻿namespace Dream.Views
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
            Console.WriteLine("Dream game store");
            Console.WriteLine("1. Sign up as user");
            Console.WriteLine("2. Sign up as developer");
            Console.WriteLine("3. Sign in as user");
            Console.WriteLine("4. Sign in as developer");
            Console.WriteLine("5. Browse games");
            Console.WriteLine("Esc. Exit");

            Key = Console.ReadKey(true).Key;
        }
        public void Print(string result)
        {
            Console.WriteLine(result);
        }
    }
}
