namespace Dream.Views.UserViews
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
            decimal amount = 0;
            while (!decimal.TryParse(Console.ReadLine(), out amount))
            {
                InvalidDeposit();
                Console.Write("Amount: ");
            }
            this.Amount = amount;
        }
        public void SuccessfulDeposit(decimal balance)
        {
            Console.WriteLine($"\n{Username} has successfully deposited {Amount:f2}");
            Console.WriteLine($"{Username} now has {balance:f2}");
        }
        public void InvalidDeposit()
        {
            Console.WriteLine("\nInvalid deposit amount");
        }
    }
}
