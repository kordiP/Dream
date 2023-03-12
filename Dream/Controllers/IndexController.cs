using Dream.Controllers.DeveloperControllers;
using Dream.Controllers.UserControllers;
using Dream.Data.Models;
using Dream.Views;

namespace Dream.Controllers
{
    public class IndexController
    {
        private IndexView view;
        private UserController userController;
        private DeveloperController developerController;
        private LoggedUserController loggedUserController;
        private LoggedDeveloperController loggedDeveloperController;
        public IndexController()
        {
            view = new IndexView();
            userController = new UserController();
            developerController = new DeveloperController();
            CommandInterpreter();
        }
        private void CommandInterpreter()
        {
            switch (view.Key)
            {
                case ConsoleKey.NumPad1 or ConsoleKey.D1:
                    int userId = userController.AddUser();
                    view.ProfileName = userController.GetUserUsername(userId);
                    view.SuccessfullRegistration();
                    loggedUserController = new LoggedUserController(userController.GetUser(userId));
                    break;

                case ConsoleKey.NumPad2 or ConsoleKey.D2:
                    int developerId = developerController.AddDeveloper();
                    view.ProfileName = developerController.GetDeveloperFullname(developerId);
                    view.SuccessfullRegistration();
                    loggedDeveloperController = new LoggedDeveloperController(developerController.GetDeveloper(developerId));
                    break;

                case ConsoleKey.NumPad3 or ConsoleKey.D3:
                    UserLoggingController userLoggingController = new UserLoggingController();
                    User loggedUser = userLoggingController.LogUser();
                    loggedUserController = new LoggedUserController(loggedUser);
                    break;

                case ConsoleKey.NumPad4 or ConsoleKey.D4:
                    DeveloperLoggingController devLoggingController = new DeveloperLoggingController();
                    Developer loggedDev = devLoggingController.LogDeveloper();
                    loggedDeveloperController = new LoggedDeveloperController(loggedDev);
                    break;

                case ConsoleKey.NumPad5 or ConsoleKey.D5:
                    GamesController gamesController = new GamesController();
                    view = new IndexView();
                    CommandInterpreter();
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                default:
                    view = new IndexView();
                    CommandInterpreter();
                    break;
            }
        }
    }
}
