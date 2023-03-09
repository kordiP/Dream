using Dream.Data.Models;
using Dream.Views.DeveloperViews;

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
                    break;
                case ConsoleKey.NumPad2 or ConsoleKey.D2:
                    break;
                case ConsoleKey.NumPad3 or ConsoleKey.D3:
                    break;
                case ConsoleKey.NumPad4 or ConsoleKey.D4:
                    break;
                case ConsoleKey.NumPad5 or ConsoleKey.D5:
                    IndexController indexController = new IndexController();
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                default:
                    view = new DeveloperLoggedView(developerController.GetDeveloperFullname(currentDeveloper.DeveloperId));
                    CommandInterpreter();
                    break;
            }
        }
    }
}
