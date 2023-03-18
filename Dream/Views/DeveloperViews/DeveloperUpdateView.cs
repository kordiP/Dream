namespace Dream.Views.DeveloperViews
{
    public class DeveloperUpdateView
    {
        public string OldEmail { get; set; }
        public string OldFirstName { get; set; }
        public string OldLastName { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DeveloperUpdateView(string oldEmail, string oldFirstName, string oldLastName)
        {
            this.OldEmail = oldEmail;
            this.OldFirstName = oldFirstName;
            this.OldLastName = oldLastName;
            GetValues();
        }
        private void GetValues()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("\nUpdate your profile info");
            Console.Write($"\n(Old email {this.OldEmail}), New email : ");
            Email = Console.ReadLine().Trim();
            Console.Write($"(Old first name {this.OldFirstName}), New first name : ");
            FirstName = Console.ReadLine().Trim();
            Console.Write($"(Old last name {this.OldLastName}), New last name : ");
            LastName = Console.ReadLine().Trim();
        }
        public void InvalidEmail()
        {
            Console.WriteLine("\nThis email is already in use. Please try another one!");
        }
        public void InvalidName()
        {
            Console.WriteLine("\nThis name is invalid. Please try another one!");
        }

        public void SuccessfulUpdate()
        {
            Console.WriteLine("\nYou have successfully updated your profile");
        }

    }
}
