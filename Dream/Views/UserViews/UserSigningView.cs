namespace Dream.Views.UserViews
{
                /* --- Summary --- */
    /* --- This interface is responsible for --- */
        /* --- registrating new users --- */

    public class UserSigningView
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public UserSigningView()
        {
            GetValues();
        }
        private void GetValues()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("\nCreate new profile");
            Console.Write("\nUsername: ");
            Username = Console.ReadLine().Trim();
            Console.Write("Email: ");
            Email = Console.ReadLine().Trim();
            Console.Write("First name: ");
            FirstName = Console.ReadLine().Trim();
            Console.Write("Last name: ");
            LastName = Console.ReadLine().Trim();
            Console.Write("Age: ");
            Age = int.Parse(Console.ReadLine());
        }
        public void InvalidUsername()
        {
            Console.WriteLine("\nThis username is invalid or already in use. Credentials were not saved. Please try another one!");
        }
        public void InvalidEmail()
        {
            Console.WriteLine("\nThis email is invalid or already in use. Credentials were not saved. Please try another one!");
        }
        public void InvalidName() 
        {
            Console.WriteLine("\nThis name is invalid. Please try another one!");
        }
    }
}
