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

        private DreamContext context;
        public LoggedUserController(User user, DreamContext context)
        {
            currentUser = user;
            this.context = context;

            this.userController = new UserController(context);
            this.downloadController = new DownloadController(context);
            this.likeController = new LikeController(context);

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

                    GameController gameController = new GameController(context);
                    gameController.BrowseGames();

                    CreateNewLoggedView();
                    break;

                case ConsoleKey.NumPad2 or ConsoleKey.D2:

                    likeController = new LikeController(context);
                    userController = new UserController(context);
                    likeController.AddLike(currentUser);

                    CreateNewLoggedView();
                    break;

                case ConsoleKey.NumPad3 or ConsoleKey.D3:

                    downloadController = new DownloadController(context);
                    userController = new UserController(context);
                    downloadController.AddDownload(currentUser);

                    CreateNewLoggedView();
                    break;

                case ConsoleKey.NumPad4 or ConsoleKey.D4:

                    likeController = new LikeController(context);
                    likeController.LikedGamesByUser(currentUser);

                    CreateNewLoggedView();
                    break;

                case ConsoleKey.NumPad5 or ConsoleKey.D5:

                    downloadController = new DownloadController(context);
                    downloadController.DownloadedGamesByUser(currentUser);

                    CreateNewLoggedView();
                    break;

                case ConsoleKey.NumPad6 or ConsoleKey.D6:

                    userController = new UserController(context);
                    currentUser = userController.UpdateUser(currentUser);

                    CreateNewLoggedView();
                    break;

                case ConsoleKey.NumPad7 or ConsoleKey.D7:

                    string username = userController.DeleteUser(currentUser);
                    loggedView.DeletedUser(username);

                    indexController = new IndexController(context);
                    break;

                case ConsoleKey.NumPad8 or ConsoleKey.D8:

                    UserDepositController depositController = new UserDepositController(context);
                    depositController.Deposit(currentUser);

                    userController = new UserController(context);

                    CreateNewLoggedView();
                    break;

                case ConsoleKey.NumPad9 or ConsoleKey.D9:

                    indexController = new IndexController(context);

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
