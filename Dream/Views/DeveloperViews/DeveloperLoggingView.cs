namespace Dream.Views.DeveloperViews
{
    public class DeveloperLoggingView
    {
        public string Email { get; set; }
        public DeveloperLoggingView()
        {
            GetValues();
        }
        private void GetValues()
        {
            Console.WriteLine("Logging into your developer account");
            Console.Write("Email: ");
            this.Email = Console.ReadLine();
        }
    }
}
