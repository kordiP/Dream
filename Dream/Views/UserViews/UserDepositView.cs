namespace Dream.Views.UserViews
{
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
            Console.Write("Amount: ");
            Amount = decimal.Parse(Console.ReadLine());
        }
        public void SuccessfulDeposit(decimal balance)
        {
            Console.WriteLine($"{Username} has successfully deposited {Amount:f2}");
            Console.WriteLine($"{Username} now has {balance:f2}");
        }
        public void InvalidDeposit()
        {
            Console.WriteLine("Cannot deposit negative amount");
        }
    }
}
