﻿namespace Dream.Views.UserViews
{
                /* --- Summary --- */
    /* --- This interface is responsible for --- */
          /* --- the deposits of users --- */

    public class UserDepositView
    {
        public decimal Amount { get; set; }
        public string Username { get; set; }

        public UserDepositView(string username)
        {
            this.Username = username;
            GetValues();
        }
        private void GetValues()
        {
            Console.WriteLine(new string('-', 50));
            Console.Write("\nAmount: ");
            Amount = decimal.Parse(Console.ReadLine());
        }
        public void SuccessfulDeposit(decimal balance)
        {
            Console.WriteLine($"\n{Username} has successfully deposited {Amount:f2}");
            Console.WriteLine($"{Username} now has {balance:f2}");
        }
        public void InvalidDeposit()
        {
            Console.WriteLine("\nCannot deposit negative amount");
        }
    }
}
