using Dream.Controllers.DeveloperControllers;
using Dream.Controllers.UserControllers;
using Dream.Data.Models;
using Dream.Views;

namespace Dream.Controllers
{
                /* --- Summary --- */
    /* --- This controller is responsible for --- */
    /* --- main windows/interfaces navigation --- */

    public class IndexController
    {
        private IndexView indexView;
        private UserController userController;
        private DeveloperController developerController;
        private LoggedUserController loggedUserController;
        private LoggedDeveloperController loggedDeveloperController;

        private DreamContext context;
        public IndexController(DreamContext context)
        {
            this.context = context;
            indexView = new IndexView();

            userController = new UserController(context);
            developerController = new DeveloperController(context);

            CommandInterpreter();
        }
        private void CommandInterpreter()
        {
            switch (indexView.Key)
            {
                case ConsoleKey.NumPad1 or ConsoleKey.D1: /* --- Sign up as a user. --- */

                    int userId = userController.AddUser();
                    indexView.ProfileName = userController.GetUserUsername(userId);

                    indexView.SuccessfullRegistration();
                    loggedUserController = new LoggedUserController(userController.GetUser(userId), context);
                    break;

                case ConsoleKey.NumPad2 or ConsoleKey.D2: /* --- Sign up as a developer. --- */

                    int developerId = developerController.AddDeveloper();
                    indexView.ProfileName = developerController.GetDeveloperFullname(developerId);

                    indexView.SuccessfullRegistration();
                    loggedDeveloperController = new LoggedDeveloperController(developerController.GetDeveloper(developerId), context);
                    break;

                case ConsoleKey.NumPad3 or ConsoleKey.D3: /* --- Sign in as a user. --- */

                    userController = new UserController(context);
                    loggedUserController = new LoggedUserController(userController.LogUser(), context);
                    break;

                case ConsoleKey.NumPad4 or ConsoleKey.D4: /* --- Sign in as a developer. --- */

                    loggedDeveloperController = new LoggedDeveloperController(developerController.LogDeveloper(), context);
                    break;

                case ConsoleKey.NumPad5 or ConsoleKey.D5: /* --- Browse all games. --- */

                    GameController gameController = new GameController(context);
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
