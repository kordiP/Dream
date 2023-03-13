using Data.Models;
using Dream.Data.Models;
using Dream.Repositories;
using System.Security.Cryptography.X509Certificates;

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
        public IEnumerable<string> GamesOfDeveloper(Developer developer)
        {
            List<string> result = new List<string>();
            List<Game> gamesOfDeveloper = gameDeveloperRepository
                                        .GetByDeveloperId(developer.DeveloperId)
                                        .Select(x => x.Game).ToList();

            foreach (Game game in gamesOfDeveloper)
            {
                List<Developer> coDeveloperOfTheGame = gameDeveloperRepository
                                                        .GetByGameId(game.GameId)
                                                        .Select(x => x.Developer).ToList();
                result.Add($"{game.Name} - {string.Join(", ", coDeveloperOfTheGame.Select(x => x.FirstName))}");
            }

            return result;
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
