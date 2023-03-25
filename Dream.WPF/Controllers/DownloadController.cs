using Dream.Controllers.UserControllers;
using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views;
using Dream.WPF;
using Dream.WPF.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dream.Controllers
{
    public class DownloadController
    {
        private DreamContext context;

        private DownloadRepository downloadRepository;
        private GameRepository gameRepository;

        private UserView userView;
        public DownloadController(DreamContext context)
        {
            this.context = context;

            this.downloadRepository = new DownloadRepository(context);
            this.gameRepository = new GameRepository(context);
        }
        public DownloadController(DreamContext context, UserView userView)
        {
            this.context = context;
            this.downloadRepository = new DownloadRepository(context);
            this.gameRepository = new GameRepository(context);
            this.userView = userView;
        }

        public Game IsDownloadable(User user)
        {
            GameController gameController = new GameController(context);
            Game game = null;

            /* Searching if game exists */
            try
            {
                game = gameRepository.GetAll()
                                    .OrderByDescending(x => x.Likes.Count())
                                    .ThenByDescending(x => x.Downloads.Count())
                                    .ElementAt(userView.GameNumber);
            }
            catch (Exception)
            {
                userView.InvalidGame();
                return null;
            }

            /* Validation */
            if (game.Downloads.Any(x => x.UserId == user.UserId))
            {
                /* If it's already downloaded, we delete it from library */
                DeleteDownload(game.Downloads.FirstOrDefault(x => x.UserId == user.UserId));
                userView.RemovedGame(game.Name);

                downloadRepository.Save();
                return null;
            }

            /* Checking if user is old enough to play */
            if (game.Genre.AgeRequirements is not null && user.Age < game.Genre.AgeRequirements)
            {
                userView.InvalidAge();
                return null;
            }

            /* Checking if user has enough money */
            if ((user.Balance is null && game.Price != 0) ||  user.Balance < game.Price)
            {
                userView.InvalidBalance();
                return null;
            }

            return game;
        }

        public int AddDownload(User user)
        {
            /* Checking if game can be downloaded */
            Game game = IsDownloadable(user);
            if (game is null) return -1;

            /* Purchasing game */
            WPF.Controllers.UserDepositController depositController = new WPF.Controllers.UserDepositController(context);
            depositController.Purchase(game.Price, user);

            /* Downloading game */
            Download download = new Download()
            {
                UserId = user.UserId,
                GameId = game.GameId
            };

            user.Downloads.Add(download);
            game.Downloads.Add(download);

            downloadRepository.Add(download);
            downloadRepository.Save();
            userView.DownloadedGame(game.Name);

            return user.UserId;
        }

        public DateTime DeleteDownload(Download download)
        {
            DateTime time = download.Date;
            downloadRepository.Delete(download);
            return time;
        }

        public int GetUserDownloadsCount(int userId)
        {
            int result = downloadRepository.GetAll().Where(x => x.UserId == userId).Count();
            return result;        
        }
        public int GetDeveloperDownloadsCount(int developerId)
        {
            return gameRepository
                .GetAll()
                .Where(x => x.GameDevelopers.Any(y => y.DeveloperId == developerId))
                .Sum(x => x.Downloads.Count());
        }

    }
}
