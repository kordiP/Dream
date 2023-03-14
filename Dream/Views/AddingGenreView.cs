namespace Dream.Views
{
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
            Console.WriteLine("Create new genre: ");
            Console.WriteLine($"Name: {this.Name}");
            Console.Write("Age requirements: ");
            this.AgeRequirements = int.Parse(Console.ReadLine());
        }
    }
}
