namespace Dream.Views.DeveloperViews
{
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
            Console.Write("Email: ");
            Email = Console.ReadLine();
            Console.Write("First name: ");
            FirstName = Console.ReadLine();
            Console.Write("Last name: ");
            LastName = Console.ReadLine();
        }
        public void InvalidEmail()
        {
            Console.WriteLine("This email is already in use. Please try another one!");
            GetValues();
        }
    }
}
