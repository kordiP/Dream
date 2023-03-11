using Dream.Data.Models;
using Dream.Repositories;

namespace Dream.Controllers
{
    public class GameDeveloperController
    {
        private GameRepository gameRepository;
        private GameDeveloperRepository gameDeveloperRepository;
        public GameDeveloperController()
        {
            gameRepository = new GameRepository();
            gameDeveloperRepository = new GameDeveloperRepository();
        }
        public int GamesCount(Developer developer)
        {
            return gameDeveloperRepository.GetByDeveloperId(developer.DeveloperId).Count();
        }
        public int LikesCount(Developer developer)
        {
            return gameDeveloperRepository
                .GetByDeveloperId(developer.DeveloperId)
                .Sum(x => x.Game.Likes.Count());
        }
        public int DownloadsCount(Developer developer)
        {
            return gameDeveloperRepository
                .GetByDeveloperId(developer.DeveloperId)
                .Sum(x => x.Game.Downloads.Count());
        }

    }
}
