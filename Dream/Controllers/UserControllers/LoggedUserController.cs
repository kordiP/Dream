using Dream.Data.Models;
using Dream.Views.UserViews;

namespace Dream.Controllers.UserControllers
{
    public class LoggedUserController
    {
        private UserLoggedView loggedView;

        private User currentUser;

        private IndexController indexController;
        private UserController userController;
        private DownloadController downloadController;
        private LikeController likeController;
        public LoggedUserController(User user)
        {
            currentUser = user;

            this.userController = new UserController();
            this.downloadController = new DownloadController();
            this.likeController = new LikeController();

            CreateNewLoggedView();
        }

        /*Loggs the user with updated information*/
        private void CreateNewLoggedView()
        {
            loggedView = new UserLoggedView
                    (
                        userController.GetUserUsername(currentUser.UserId),
                        userController.GetUserBalance(currentUser.UserId),
                        downloadController.GetUserDownloadsCount(currentUser.UserId),
                        likeController.GetUserLikesCount(currentUser.UserId)
                    );
            CommandInterpreter();
        }
        private void CommandInterpreter()
        {
            switch (loggedView.Key)
            {
                case ConsoleKey.NumPad1 or ConsoleKey.D1:

                    GameController gameController = new GameController();
                    gameController.BrowseGames();

                    CreateNewLoggedView();
                    break;

                case ConsoleKey.NumPad2 or ConsoleKey.D2:

                    likeController = new LikeController();
                    userController = new UserController();
                    likeController.AddLike(currentUser);

                    CreateNewLoggedView();
                    break;

                case ConsoleKey.NumPad3 or ConsoleKey.D3:

                    downloadController = new DownloadController();
                    userController = new UserController();
                    downloadController.AddDownload(currentUser);

                    CreateNewLoggedView();
                    break;

                case ConsoleKey.NumPad4 or ConsoleKey.D4:

                    likeController = new LikeController();
                    likeController.LikedGamesByUser(currentUser);

                    CreateNewLoggedView();
                    break;

                case ConsoleKey.NumPad5 or ConsoleKey.D5:

                    downloadController = new DownloadController();
                    downloadController.DownloadedGamesByUser(currentUser);

                    CreateNewLoggedView();
                    break;

                case ConsoleKey.NumPad6 or ConsoleKey.D6:

                    userController = new UserController();
                    currentUser = userController.UpdateUser(currentUser);

                    CreateNewLoggedView();
                    break;

                case ConsoleKey.NumPad7 or ConsoleKey.D7:

                    string username = userController.DeleteUser(currentUser);
                    loggedView.DeletedUser(username);

                    indexController = new IndexController();
                    break;

                case ConsoleKey.NumPad8 or ConsoleKey.D8:

                    UserDepositController depositController = new UserDepositController();
                    depositController.Deposit(currentUser);

                    userController = new UserController();

                    CreateNewLoggedView();
                    break;

                case ConsoleKey.NumPad9 or ConsoleKey.D9:

                    indexController = new IndexController();

                    break;

                case ConsoleKey.Escape:

                    Environment.Exit(0);

                    break;

                default:
                    CreateNewLoggedView();
                    break;
            }
        }
    }
}
