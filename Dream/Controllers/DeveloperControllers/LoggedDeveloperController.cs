using Dream.Data.Models;
using Dream.Views;
using Dream.Views.DeveloperViews;

namespace Dream.Controllers.DeveloperControllers
{
    public class LoggedDeveloperController
    {
        private DeveloperLoggedView loggeView;
        private BrowsingGamesView gamesView;

        private Developer currentDeveloper;

        private IndexController indexController;
        private DeveloperController developerController;
        private GameController gameController;

        public LoggedDeveloperController(Developer developer)
        {
            currentDeveloper = developer;
            this.developerController = new DeveloperController();

            gameController = new GameController();
            loggeView = new DeveloperLoggedView
                (developerController.GetDeveloperFullname(currentDeveloper.DeveloperId),
                developerController.DeveloperGameCount(currentDeveloper),
                developerController.DeveloperLikeCount(currentDeveloper),
                developerController.DeveloperDownloadCount(currentDeveloper));
            CommandInterpreter();
        }
        private void CommandInterpreter()
        {
            switch (loggeView.Key)
            {
                case ConsoleKey.NumPad1 or ConsoleKey.D1:
                    gameController.AddGame(currentDeveloper);
                    loggeView = new DeveloperLoggedView
                        (developerController.GetDeveloperFullname(currentDeveloper.DeveloperId),
                        developerController.DeveloperGameCount(currentDeveloper),
                        developerController.DeveloperLikeCount(currentDeveloper),
                        developerController.DeveloperDownloadCount(currentDeveloper));
                    CommandInterpreter();
                    break;
                case ConsoleKey.NumPad2 or ConsoleKey.D2:
                    gamesView = new BrowsingGamesView();
                    gamesView.AllGamesList(developerController.BrowseGamesAsDeveloper(currentDeveloper));
                    gamesView.ExitView();
                    loggeView = new DeveloperLoggedView
                        (developerController.GetDeveloperFullname(currentDeveloper.DeveloperId),
                        developerController.DeveloperGameCount(currentDeveloper),
                        developerController.DeveloperLikeCount(currentDeveloper),
                        developerController.DeveloperDownloadCount(currentDeveloper));
                    CommandInterpreter();
                    break;
                case ConsoleKey.NumPad3 or ConsoleKey.D3:
                    currentDeveloper = developerController.UpdateDeveloper(currentDeveloper);
                    loggeView = new DeveloperLoggedView
                        (developerController.GetDeveloperFullname(currentDeveloper.DeveloperId),
                        developerController.DeveloperGameCount(currentDeveloper),
                        developerController.DeveloperLikeCount(currentDeveloper),
                        developerController.DeveloperDownloadCount(currentDeveloper));
                    CommandInterpreter();
                    break;
                case ConsoleKey.NumPad4 or ConsoleKey.D4:
                    string name = developerController.DeleteDeveloper(currentDeveloper);
                    loggeView.DeletedDeveloper(name);
                    indexController = new IndexController();
                    break;
                case ConsoleKey.NumPad5 or ConsoleKey.D5:
                    indexController = new IndexController();
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                default:
                    loggeView = new DeveloperLoggedView
                        (developerController.GetDeveloperFullname(currentDeveloper.DeveloperId),
                        developerController.DeveloperGameCount(currentDeveloper),
                        developerController.DeveloperLikeCount(currentDeveloper),
                        developerController.DeveloperDownloadCount(currentDeveloper));
                    CommandInterpreter();
                    break;
            }
        }
    }
}
