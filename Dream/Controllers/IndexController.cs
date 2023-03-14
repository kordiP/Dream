using Dream.Controllers.DeveloperControllers;
using Dream.Controllers.UserControllers;
using Dream.Data.Models;
using Dream.Views;

namespace Dream.Controllers
{
    public class IndexController
    {
        private IndexView indexView;
        private UserController userController;
        private DeveloperController developerController;
        private LoggedUserController loggedUserController;
        private LoggedDeveloperController loggedDeveloperController;
        public IndexController()
        {
            indexView = new IndexView();
            userController = new UserController();
            developerController = new DeveloperController();
            CommandInterpreter();
        }
        private void CommandInterpreter()
        {
            switch (indexView.Key)
            {
                case ConsoleKey.NumPad1 or ConsoleKey.D1:
                    int userId = userController.AddUser();
                    indexView.ProfileName = userController.GetUserUsername(userId);
                    indexView.SuccessfullRegistration();
                    loggedUserController = new LoggedUserController(userController.GetUser(userId));
                    break;

                case ConsoleKey.NumPad2 or ConsoleKey.D2:
                    int developerId = developerController.AddDeveloper();
                    indexView.ProfileName = developerController.GetDeveloperFullname(developerId);
                    indexView.SuccessfullRegistration();
                    loggedDeveloperController = new LoggedDeveloperController(developerController.GetDeveloper(developerId));
                    break;

                case ConsoleKey.NumPad3 or ConsoleKey.D3:
                    UserLoggingController userLoggingController = new UserLoggingController();
                    User loggedUser = userLoggingController.LogUser();
                    loggedUserController = new LoggedUserController(loggedUser);
                    break;

                case ConsoleKey.NumPad4 or ConsoleKey.D4:
                    loggedDeveloperController = new LoggedDeveloperController(developerController.LogDeveloper());
                    break;

                case ConsoleKey.NumPad5 or ConsoleKey.D5:
                    GameController gameController = new GameController();
                    gameController.BrowseGames();
                    indexView = new IndexView();
                    CommandInterpreter();
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                default:
                    indexView = new IndexView();
                    CommandInterpreter();
                    break;
            }
        }
    }
}
