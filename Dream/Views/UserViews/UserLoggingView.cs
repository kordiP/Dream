namespace Dream.Views.UserViews
{
    public class UserLoggingView
    {
        public string Username { get; set; }
        public UserLoggingView()
        {
            GetValues();
        }
        private void GetValues()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Logging into your user account");
            Console.Write("Username: ");
            this.Username = Console.ReadLine();
        }
        public void InvalidUsername()
        {
            Console.WriteLine("This username does not exist. Please try another one!");
            GetValues();
        }
    }
}
