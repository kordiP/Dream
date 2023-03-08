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
            Console.WriteLine("Logging into your user account");
            Console.Write("Username: ");
            this.Username = Console.ReadLine();
        }
    }
}
