﻿namespace Dream.Views.DeveloperViews
{
                /* --- Summary --- */
    /* --- This interface is responsible for --- */
      /* --- logging existing developers --- */

    public class DeveloperLoggingView
    {
        public string Email { get; set; }
        public DeveloperLoggingView()
        {
            GetValues();
        }
        private void GetValues()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("\nLogging into your developer account");
            Console.Write("Email: ");
            this.Email = Console.ReadLine();
        }
        public void InvalidEmail()
        {
            Console.WriteLine("\nThis email does not exist. Please try another one!");
            GetValues();
        }
    }
}
