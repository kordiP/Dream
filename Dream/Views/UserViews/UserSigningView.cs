namespace Dream.Views.UserViews
{
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
            Console.Write("Username: ");
            Username = Console.ReadLine();
            Console.Write("Email: ");
            Email = Console.ReadLine();
            Console.Write("First name: ");
            FirstName = Console.ReadLine();
            Console.Write("Last name: ");
            LastName = Console.ReadLine();
            Console.Write("Age: ");
            Age = int.Parse(Console.ReadLine());
        }
        public void InvalidUsername()
        {
            Console.WriteLine("This username is already in use. Please try another one!");
            GetValues();
        }
        public void InvalidEmail()
        {
            Console.WriteLine("This email is already in use. Please try another one!");
            GetValues();
        }
    }
}
