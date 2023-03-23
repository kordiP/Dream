namespace Dream.Views.UserViews
{
                /* --- Summary --- */
    /* --- This interface is responsible for --- */
         /* --- logging existing users --- */

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
            Console.WriteLine("\nLogging into your user account");
            Console.Write("Username: ");
            this.Username = Console.ReadLine();
        }
        public void InvalidUsername()
        {
            Console.WriteLine("\nThis username does not exist. Please try another one!");
            GetValues();
        }
    }
}
