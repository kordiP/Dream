using Dream.Data.Models;
using Dream.Views;

namespace Dream.Controllers.DeveloperControllers
{
    public class LoggedDeveloperController
    {
        private DeveloperLoggedView view;
        private DeveloperController developerController;
        private Developer currentDeveloper;

        public LoggedDeveloperController(Developer developer)
        {
            currentDeveloper = developer;
            this.developerController = new DeveloperController();
            view = new DeveloperLoggedView(developerController.GetDeveloperFullname(currentDeveloper.DeveloperId));
            CommandInterpreter();
        }
        private void CommandInterpreter()
        {
            switch (view.Key)
            {
                case ConsoleKey.NumPad1 or ConsoleKey.D1:
                    view.Print("1. Create new game");
                    break;
                case ConsoleKey.NumPad2 or ConsoleKey.D2:
                    view.Print("2. Browse your games");
                    break;
                case ConsoleKey.NumPad3 or ConsoleKey.D3:
                    view.Print("3. Edit profile info");
                    break;
                case ConsoleKey.NumPad4 or ConsoleKey.D4:
                    view.Print("4. Delete profile");
                    break;
                case ConsoleKey.NumPad5 or ConsoleKey.D5:
                    view.Print("5. Log out");
                    break;
                case ConsoleKey.NumPad9 or ConsoleKey.D9:
                    view.Print(new string('-', 50));
                    IndexController indexController = new IndexController();
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                default:
                    view.Print(new string('-', 50));
                    view = new DeveloperLoggedView(developerController.GetDeveloperFullname(currentDeveloper.DeveloperId));
                    CommandInterpreter();
                    break;
            }
        }
    }
}
