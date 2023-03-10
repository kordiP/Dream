namespace Dream.Views.UserViews
{
    public class UserUpdateView
    {
        public string OldUsername { get; set; }
        public string OldEmail { get; set; }
        public string OldFirstName { get; set; }
        public string OldLastName { get; set; }
        public int OldAge { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public UserUpdateView(string oldUsername, string oldEmail, string oldFirstName, string oldLastName, int oldAge)
        {
            this.OldUsername = oldUsername;
            this.OldEmail = oldEmail;
            this.OldFirstName = oldFirstName;
            this.OldLastName = oldLastName;
            this.OldAge = oldAge;
            GetValues();
        }
        private void GetValues()
        {
            Console.WriteLine(new string('-', 50));
            Console.Write($"(Old username {this.OldUsername}), new username : ");
            Username = Console.ReadLine().Trim();
            Console.Write($"(Old email {this.OldEmail}), new email : ");
            Email = Console.ReadLine().Trim();
            Console.Write($"(Old first name {this.OldFirstName}), new first name : ");
            FirstName = Console.ReadLine().Trim();
            Console.Write($"(Old last name {this.OldLastName}), new last name : ");
            LastName = Console.ReadLine().Trim();
            Console.Write($"(Old age {this.OldAge}), new age : ");
            Age = int.Parse(Console.ReadLine());
        }
        public void InvalidUsername()
        {
            Console.WriteLine("This username is already in use. Please try another one!");
        }
        public void InvalidEmail()
        {
            Console.WriteLine("This email is already in use. Please try another one!");
        }
        public void SuccessfulUpdate()
        {
            Console.WriteLine("You have successfully update your profile");
        }
    }
}
