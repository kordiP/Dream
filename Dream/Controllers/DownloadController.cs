using Dream.Controllers.UserControllers;
using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views;

namespace Dream.Controllers
{
                /* --- Summary --- */
    /* --- This controller is responsible for --- */
         /* --- download CRUD operations --- */

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

        /* --- Checks user balance, age and if the game has been already downloaded --- */
        public Game IsDownloadable(User user)
        {
            GameController gameController = new GameController(context);

            downloadView = new DownloadView(gameController.BrowseDownloadedGames(user));
            Game game = null;

            try
            {
                game = gameController
                        .GetBestGames()
                        .ElementAt(downloadView.GameNumber - 1);
            }
            catch (Exception)
            {
                downloadView.InvalidGame();
                return null;
            }

            if (game.Downloads.Any(x => x.UserId == user.UserId))
            {
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

            if ((user.Balance is null && game.Price != 0) ||  user.Balance < game.Price)
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

            /* --- Creating the download --- */
            Download download = new Download()
            {
                UserId = user.UserId,
                GameId = game.GameId
            };

            user.Downloads.Add(download);
            game.Downloads.Add(download);

            downloadRepository.Add(download);

            /* --- Saving the changes --- */
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
            /* --- Gets downloaded games of user --- */
            List<Download> userDownloads = GetUserDownloads(user.UserId);

            /* --- Sends them to the interface --- */
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
        public List<Download> GetUserDownloads(int userId)
        {
            return downloadRepository.GetAll().Where(x => x.UserId == userId).ToList();
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
