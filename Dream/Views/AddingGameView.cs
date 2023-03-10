namespace Dream.Views
{
    public class AddingGameView
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double RequiredMemory { get; set; }
        public string? Description { get; set; }
        public string GenreName { get; set; }
        public List<string> ?DeveloperEmails { get; set; }

        public AddingGameView()
        {
            GetValues();
        }
        private void GetValues()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Create your new game:");
            Console.Write("Name: ");
            this.Name = Console.ReadLine();
            Console.Write("Price: ");
            this.Price = decimal.Parse(Console.ReadLine());
            Console.Write("Required memory: ");
            this.RequiredMemory = double.Parse(Console.ReadLine());
            Console.Write("Genre: ");
            this.GenreName = Console.ReadLine();
            Console.Write("Description: ");
            this.Description = Console.ReadLine();
            char[] separators = new char[] { ' ', ',', ';' };
            Console.Write("CoDevelopers' emails: ");
            this.DeveloperEmails = Console.ReadLine().Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
        public void SuccessfullyCreatedGame(string name)
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"You have successfully created the new game of {name}");
        }
    }
}
