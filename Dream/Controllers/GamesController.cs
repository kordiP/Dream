using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views;
using System.Security.Cryptography.X509Certificates;

namespace Dream.Controllers
{
    public class GamesController
    {
        private GameRepository gameRepository;
        private GenreRepository genreRepository;
        private BrowsingGamesView view;

        public GamesController()
        {
            gameRepository = new GameRepository();
            genreRepository = new GenreRepository();
            List<string> result = new List<string>();

            foreach (var game in gameRepository.GetAll())
            {
                result.Add($"{game.Name} - {game.Price:f2}$ - {game.RequiredMemory:f2}GB - Likes: {game.Likes.Count} - Downloads: {game.Downloads.Count}" +
                    $"\nGenre: {game.Genre.Name} - Description: {game.Description}");
            }
            view = new BrowsingGamesView(result);

            view.MostPopularGenre(GetMostPopularGenre().Name);
            view.MostLikedGame($"{GetMostLikedGame().Name} - {GetMostLikedGame().Likes.Count}");
            view.MostDownloadedGame($"{GetMostDownloadedGame().Name} - {GetMostDownloadedGame().Likes.Count}");
            view.AllGamesList();
            view.ExitView();
        }
        public Genre GetMostPopularGenre()
        {
            return genreRepository.GetAll()
                .OrderByDescending(x => x.Games.Count)
                .First();
        }
        public Game GetMostLikedGame()
        {
            return gameRepository.GetAll()
                .OrderByDescending(x => x.Likes.Count)
                .First();
        }
        public Game GetMostDownloadedGame()
        {
            return gameRepository.GetAll()
                .OrderByDescending(x => x.Downloads.Count)
                .First();
        }
    }
}
