using Data.Models;
using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views;

namespace Dream.Controllers
{                
                /* --- Summary --- */
    /* --- This controller is responsible for --- */
           /* --- game CRUD operations --- */

    public class GameController
    {
        private GameRepository gameRepository;
        private DeveloperRepository devRepository;
        private GameDeveloperRepository gameDeveloperRepository;

        private GenreController genreController;

        private DreamContext context;

        public GameController(DreamContext context)
        {
            this.context = context;

            genreController = new GenreController(context);

            gameDeveloperRepository = new GameDeveloperRepository(context);
            devRepository = new DeveloperRepository(context);
            gameRepository = new GameRepository(context);
        }

        /* --- Returns a collection of games downloaded by a user --- */
        public IEnumerable<string> BrowseDownloadedGames(User user)
        {
            List<string> result = new List<string>();
            int index = 1;

            foreach (var game in GetBestGames())
            {
                if (game.Downloads.Any(x => x.UserId == user.UserId))
                {
                    result.Add($"{index}. {game.Name} - {game.Price:f2}$ - {game.RequiredMemory:f2}GB - Genre: {game.Genre.Name} - Downloaded");
                }
                else
                {
                    result.Add($"{index}. {game.Name} - {game.Price:f2}$ - {game.RequiredMemory:f2}GB - Genre: {game.Genre.Name}");
                }
                index++;
            }

            return result;
        }

        /* --- Returns a collection of games liked by a user --- */
        public IEnumerable<string> BrowseLikedGames(User user)
        {
            List<string> result = new List<string>();
            int index = 1;

            foreach (var game in GetBestGames())
            {
                if (game.Likes.Any(x => x.UserId == user.UserId))
                {
                    result.Add($"{index}. {game.Name} - {game.Price:f2}$ - {game.RequiredMemory:f2}GB - Genre: {game.Genre.Name} - Liked");
                }
                else
                {
                    result.Add($"{index}. {game.Name} - {game.Price:f2}$ - {game.RequiredMemory:f2}GB - Genre: {game.Genre.Name}");
                }
                index++;
            }

            return result;
        }

        /* --- Returns a collection of all games and navigates "BrowseGamesView" interface --- */
        public IEnumerable<string> BrowseGames() 
        {
            List<string> result = new List<string>();

            foreach (var game in gameRepository.GetAll())
            {
                result.Add($"{game.Name} - {game.Price:f2}$ - {game.RequiredMemory:f2}GB - Likes: {game.Likes.Count} - Downloads: {game.Downloads.Count}" +
                    $"\nGenre: {game.Genre.Name} - Description: {game.Description}");
            }

            BrowseGamesView view = new BrowseGamesView();
            if (gameRepository.GetAll().Count() == 0)
            {
                view.NoGamesException();
            }
            else
            {
                view.MostPopularGenre(genreController.GetMostPopularGenre().Name);
                view.MostLikedGame($"{GetMostLikedGame().Name} - {GetMostLikedGame().Likes.Count}");
                view.MostDownloadedGame($"{GetMostDownloadedGame().Name} - {GetMostDownloadedGame().Likes.Count}");
            }
            view.AllGamesList(result);
            view.ExitView();

            return result;
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
        public int GetDeveloperGameCount(int developerId)
        {
            return gameDeveloperRepository.GetAll().Where(x => x.DeveloperId == developerId).Count();
        }

        public List<Game> GetGamesOfDeveloper(int developerId)
        {
            return gameDeveloperRepository.GetAll()
                   .Where(x => x.DeveloperId == developerId)
                   .Select(x => x.Game).ToList();
        }

        public List<Game> GetBestGames()
        {
            return gameRepository.GetAll()
                .OrderByDescending(x => x.Likes.Count())
                .ThenByDescending(x => x.Downloads.Count())
                .ToList();
        }
        public int AddGame(Developer developer)
        {
            /* --- Getting values --- */
            AddingGameView gameView = new AddingGameView();

            /* --- Validating if the game has a valid name/genre --- */
            while (string.IsNullOrWhiteSpace(gameView.Name)) 
            {
                gameView.InvalidGameName();
                AddGame(developer);
            }

            while (string.IsNullOrWhiteSpace(gameView.GenreName))
            {
                gameView.InvalidGenreName();
                AddGame(developer);
            }

            Genre genre = genreController.GetGenreByName(gameView.GenreName);
            if (genre is null)
            {
                genre = genreController.AddGenre(gameView.GenreName);
            }

            /* --- Creating the game --- */
            Game game = new Game()
            {
                Name = gameView.Name,
                Price = gameView.Price < 0 ? 0 : gameView.Price,
                RequiredMemory = gameView.RequiredMemory < 0 ? 0 : gameView.RequiredMemory,
                Description = gameView.Description
            };

            /* --- Entity mapping --- */
            genre.Games.Add(game);
            game.GenreId = genre.GenreId;

            GameDeveloper gameCurrentDeveloper = new GameDeveloper()
            {
                DeveloperId = developer.DeveloperId,
                GameId = game.GameId
            };

            gameDeveloperRepository.Add(gameCurrentDeveloper);
            developer.GameDevelopers.Add(gameCurrentDeveloper);
            game.GameDevelopers.Add(gameCurrentDeveloper);

            foreach (var coDevEmail in gameView.DeveloperEmails)
            {
                Developer coDev = devRepository.GetByEmail(coDevEmail);
                if (coDev != null)
                {
                    GameDeveloper gameDeveloper = new GameDeveloper()
                    {
                        DeveloperId = coDev.DeveloperId,
                        GameId = game.GameId
                    };
                    gameDeveloperRepository.Add(gameDeveloper);
                    coDev.GameDevelopers.Add(gameDeveloper);
                    game.GameDevelopers.Add(gameDeveloper);
                }
            }

            /* --- Saving the changes --- */
            gameRepository.Add(game);
            gameRepository.Save();
            return game.GameId;
        }
    }
}
