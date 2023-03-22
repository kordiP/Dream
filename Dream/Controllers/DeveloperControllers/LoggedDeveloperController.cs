using Dream.Data.Models;
using Dream.Views;
using Dream.Views.DeveloperViews;

namespace Dream.Controllers.DeveloperControllers
{
    public class LoggedDeveloperController
    {
        private DeveloperLoggedView loggedView;
        private BrowseGamesView gamesView;

        private Developer currentDeveloper;

        private IndexController indexController;
        private DeveloperController developerController;
        private GameController gameController;
        private LikeController likeController;
        private DownloadController downloadController;

        private DreamContext context;

        public LoggedDeveloperController(Developer developer, DreamContext context)
        {
            currentDeveloper = developer;
            this.context = context;

            this.developerController = new DeveloperController(context);

            gameController = new GameController(context);
            likeController = new LikeController(context);
            downloadController = new DownloadController(context);

            CreateNewLoggedView();
        }

        /*Loggs the developer with updated information*/
        private void CreateNewLoggedView()
        {
            loggedView = new DeveloperLoggedView
             (
                    developerController.GetDeveloperFullname(currentDeveloper.DeveloperId),
                    gameController.GetDeveloperGameCount(currentDeveloper.DeveloperId),
                    likeController.GetDeveloperLikesCount(currentDeveloper.DeveloperId),
                    downloadController.GetDeveloperDownloadsCount(currentDeveloper.DeveloperId)
            );
            CommandInterpreter();
        }

        private void CommandInterpreter()
        {
            switch (loggedView.Key)
            {
                case ConsoleKey.NumPad1 or ConsoleKey.D1: /*--- Create new game. ---*/
                    gameController.AddGame(currentDeveloper);

                    CreateNewLoggedView();
                    break;

                case ConsoleKey.NumPad2 or ConsoleKey.D2: /*--- Browse games of currently logged developer. ---*/
                    gamesView = new BrowseGamesView();

                    gamesView.AllGamesList(developerController.BrowseGamesAsDeveloper(currentDeveloper));
                    gamesView.ExitView();

                    CreateNewLoggedView();
                    break;

                case ConsoleKey.NumPad3 or ConsoleKey.D3: /*--- Edit developer profile. ---*/
                    currentDeveloper = developerController.UpdateDeveloper(currentDeveloper);

                    CreateNewLoggedView();
                    break;

                case ConsoleKey.NumPad4 or ConsoleKey.D4: /*--- Delete developer profile. ---*/
                    string name = developerController.DeleteDeveloper(currentDeveloper);
                    loggedView.DeletedDeveloper(name);

                    indexController = new IndexController(context);
                    break;

                case ConsoleKey.NumPad5 or ConsoleKey.D5: /*--- Log out of developer profile. ---*/
                    indexController = new IndexController(context);
                    break;

                case ConsoleKey.Escape: /*--- Close the app. ---*/
                    Environment.Exit(0);
                    break;

                default:
                    CreateNewLoggedView();
                    break;
            }
        }
    }
}
