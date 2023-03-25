namespace Dream.Views
{
                /* --- Summary --- */
    /* --- This interface is responsible for --- */
        /* --- adding games by developers --- */

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
            Console.WriteLine("\nCreate your new game:");
            Console.Write("\nName: "); 
            this.Name = Console.ReadLine();

            Console.Write("Price: ");
            decimal price = 0;
            while (!decimal.TryParse(Console.ReadLine(), out price))
            {
                InvalidPriceValue();
                Console.Write("Price: ");
            }
            this.Price = price;

            Console.Write("Required memory: ");
            double memory = 0;
            while (!double.TryParse(Console.ReadLine(), out memory))
            {
                InvalidMemoryValue();
                Console.Write("Required memory: ");
            }
            this.RequiredMemory = memory;

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
            Console.WriteLine($"\nYou have successfully created the new game of {name}");
        }
        public void InvalidGameName()
        {
            Console.WriteLine("\nName of game can not be empty");
        }
        public void InvalidGenreName()
        {
            Console.WriteLine("\nName of genre can not be empty");
        }
        public void InvalidPriceValue()
        {
            Console.WriteLine("\nPrice value was invalid");
        }
        public void InvalidMemoryValue()
        {
            Console.WriteLine("\nRequired memory value was invalid");
        }
    }
}
