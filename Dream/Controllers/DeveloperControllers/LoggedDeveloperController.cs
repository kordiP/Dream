﻿using Dream.Controllers.UserControllers;
using Dream.Data.Models;
using Dream.Views.DeveloperViews;
using Dream.Views.UserViews;

namespace Dream.Controllers.DeveloperControllers
{
    public class LoggedDeveloperController
    {
        private DeveloperLoggedView view;
        private DeveloperController developerController;
        private GameDeveloperController gameController;
        private Developer currentDeveloper;
        private IndexController indexController;

        public LoggedDeveloperController(Developer developer)
        {
            currentDeveloper = developer;
            this.developerController = new DeveloperController();
            gameController = new GameDeveloperController();
            view = new DeveloperLoggedView
                (developerController.GetDeveloperFullname(currentDeveloper.DeveloperId),
                gameController.GamesCount(currentDeveloper),
                gameController.LikesCount(currentDeveloper),
                gameController.DownloadsCount(currentDeveloper));
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
                    DeveloperUpdateController devUpdateController = new DeveloperUpdateController(currentDeveloper);
                    currentDeveloper = devUpdateController.UpdateDeveloper(currentDeveloper);
                    view = new DeveloperLoggedView
                        (developerController.GetDeveloperFullname(currentDeveloper.DeveloperId),
                        gameController.GamesCount(currentDeveloper),
                        gameController.LikesCount(currentDeveloper),
                        gameController.DownloadsCount(currentDeveloper));
                    CommandInterpreter();
                    break;
                case ConsoleKey.NumPad4 or ConsoleKey.D4:
                    string name = developerController.DeleteDeveloper(currentDeveloper);
                    view.DeletedDeveloper(name);
                    indexController = new IndexController();
                    break;
                case ConsoleKey.NumPad5 or ConsoleKey.D5:
                    indexController = new IndexController();
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                default:
                    view = new DeveloperLoggedView
                        (developerController.GetDeveloperFullname(currentDeveloper.DeveloperId),
                        gameController.GamesCount(currentDeveloper),
                        gameController.LikesCount(currentDeveloper),
                        gameController.DownloadsCount(currentDeveloper));
                    CommandInterpreter();
                    break;
            }
        }
    }
}
