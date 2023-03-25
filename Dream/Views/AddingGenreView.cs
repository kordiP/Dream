namespace Dream.Views
{
                /* --- Summary --- */
    /* --- This interface is responsible for --- */
            /* --- adding genres--- */

    public class AddingGenreView
    {
        public string Name { get; set; }
        public int ?AgeRequirements { get; set; }
        public AddingGenreView(string name)
        {
            this.Name = name;
            GetValues(); 
        }
        private void GetValues()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("\nCreate new genre: ");
            Console.WriteLine($"\nName: {Name}");
            Console.Write("Age requirements: ");

            this.AgeRequirements = int.Parse(Console.ReadLine());
        }
    }
}
