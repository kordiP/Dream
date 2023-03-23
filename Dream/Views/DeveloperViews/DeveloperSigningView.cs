namespace Dream.Views.DeveloperViews
{
                /* --- Summary --- */
    /* --- This interface is responsible for --- */
       /* --- registrating new developers --- */

    public class DeveloperSigningView
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DeveloperSigningView()
        {
            GetValues();
        }
        private void GetValues()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("\nCreate new developer account");
            Console.Write("\nEmail: ");
            Email = Console.ReadLine().Trim();
            Console.Write("First name: ");
            FirstName = Console.ReadLine().Trim();
            Console.Write("Last name: ");
            LastName = Console.ReadLine().Trim();
        }
        public void InvalidEmail()
        {
            Console.WriteLine("\nThis email is invalid or already in use. Please try another one!");
        }
        public void InvalidName()
        {
            Console.WriteLine("\nThis name is invalid. Please try another one!");
        }

    }
}
