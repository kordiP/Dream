using Dream.Data.Models;
using Dream.Views.UserViews;

namespace Dream.Controllers.UserControllers
{
    public class LoggedUserController
    {
        private UserLoggedView loggedView;
        private UserController userController;
        private UserDepositController UserDepositController;
        private User currentUser;
        private IndexController indexController;
        public LoggedUserController(User user)
        {
            currentUser = user;
            this.userController = new UserController();
            loggedView = new UserLoggedView(currentUser);
            CommandInterpreter();
        }
        private void CommandInterpreter()
        {
            switch (loggedView.Key)
            {
                case ConsoleKey.NumPad1 or ConsoleKey.D1:
                    GameController gameController = new GameController();
                    gameController.BrowseGames();
                    loggedView = new UserLoggedView(currentUser);
                    CommandInterpreter();
                    break;
                case ConsoleKey.NumPad2 or ConsoleKey.D2:
                    break;
                case ConsoleKey.NumPad3 or ConsoleKey.D3:
                    break;
                case ConsoleKey.NumPad4 or ConsoleKey.D4:
                    LikeController browseLikesController = new LikeController(currentUser);
                    browseLikesController.LikedGamesByUser();
                    loggedView = new UserLoggedView(currentUser);
                    CommandInterpreter();
                    break;
                case ConsoleKey.NumPad5 or ConsoleKey.D5:
                    DownloadController browseDownloadsController = new DownloadController(currentUser);
                    browseDownloadsController.DownloadedGamesByUser();
                    loggedView = new UserLoggedView(currentUser);
                    CommandInterpreter();
                    break;
                case ConsoleKey.NumPad6 or ConsoleKey.D6:
                    UserUpdateController userUpdateController = new UserUpdateController(currentUser);
                    currentUser = userUpdateController.UpdateUser(currentUser);
                    loggedView = new UserLoggedView(currentUser);
                    CommandInterpreter();
                    break;
                case ConsoleKey.NumPad7 or ConsoleKey.D7:
                    string username = userController.DeleteUser(currentUser);
                    loggedView.DeletedUser(username);
                    indexController = new IndexController();
                    break;
                case ConsoleKey.NumPad8 or ConsoleKey.D8:
                    UserDepositController depositController = new UserDepositController(currentUser);
                    if (depositController.IsDepositValid())
                    {
                        depositController.Deposit();
                    }
                    loggedView = new UserLoggedView(currentUser);
                    CommandInterpreter();
                    break;
                case ConsoleKey.NumPad9 or ConsoleKey.D9:
                    indexController = new IndexController();
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                default:
                    loggedView = new UserLoggedView(currentUser);
                    CommandInterpreter();
                    break;
            }
        }
    }
}
