using Dream.Controllers.UserControllers;
using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dream.Controllers
{
    public class DownloadController
    {
        private DownloadRepository downloadRepository;
        private GameRepository gameRepository;
        private DownloadView downloadView;

        private DreamContext context;
        public DownloadController(DreamContext context)
        {
            this.context = context;

            this.downloadRepository = new DownloadRepository(context);
            this.gameRepository = new GameRepository(context);
        }

        public Game IsDownloadable(User user)
        {
            GameController gameController = new GameController(context);

            downloadView = new DownloadView(gameController.BrowseDownloadedGames(user));
            Game game = null;

            try
            {
                game = gameRepository.GetAll()
                                    .OrderByDescending(x => x.Likes.Count())
                                    .ThenByDescending(x => x.Downloads.Count())
                                    .ElementAt(downloadView.GameNumber - 1);
            }
            catch (Exception)
            {
                downloadView.InvalidGame();
                return null;
            }

            if (game.Downloads.Any(x => x.UserId == user.UserId))
            {
                //user.Downloads.Remove(game.Downloads.FirstOrDefault(x => x.UserId == user.UserId));
                //game.Downloads.Remove(game.Downloads.FirstOrDefault(x => x.UserId == user.UserId));

                DeleteDownload(game.Downloads.FirstOrDefault(x => x.UserId == user.UserId));
                downloadView.RemovedGame(game.Name);

                downloadRepository.Save();
                return null;
            }

            if (game.Genre.AgeRequirements is not null && user.Age < game.Genre.AgeRequirements)
            {
                downloadView.InvalidAge();
                return null;
            }

            if ((user.Balance is null && game.Price != 0) ||  user.Balance < game.Price) // (user.Balance is null && game.Price == 0) || -- not needed?
            {
                downloadView.InvalidBalance();
                return null;
            }

            return game;
        }

        public int AddDownload(User user)
        {
            Game game = IsDownloadable(user);
            if (game is null) return -1;

            UserDepositController depositController = new UserDepositController(context);
            depositController.Purchase(game.Price, user);

            Download download = new Download()
            {
                UserId = user.UserId,
                GameId = game.GameId
            };

            user.Downloads.Add(download);
            game.Downloads.Add(download);

            downloadRepository.Add(download);
            downloadRepository.Save();
            downloadView.DownloadedGame(game.Name);

            return user.UserId;
        }

        public DateTime DeleteDownload(Download download)
        {
            DateTime time = download.Date;
            downloadRepository.Delete(download);
            return time;
        }

        public int DownloadedGamesByUser(User user)
        {
            IEnumerable<Download> userDownloads = downloadRepository.GetAll().Where(x => x.UserId == user.UserId);
            BrowseDownloadsView downloadsView = new BrowseDownloadsView(userDownloads);

            if (userDownloads.Count() == 0)
            {
                downloadsView.NoDownloads();
            }
            else
            {
                downloadsView.ShowDownloads();
            }
            return userDownloads.Count();
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
