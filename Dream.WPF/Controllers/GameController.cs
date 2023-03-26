using Data.Models;
using Dream.Data.Models;
using Dream.Repositories;
using Dream.WPF;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Dream.Controllers
{
    public class GameController
    {
        private DreamContext context;

        private GameRepository gameRepository;
        private DeveloperRepository devRepository;
        private GameDeveloperRepository gameDeveloperRepository;

        private GenreController genreController;

        private DeveloperView developerView;
        public GameController(DreamContext context)
        {
            this.context = context;

            genreController = new GenreController(context, developerView);

            gameDeveloperRepository = new GameDeveloperRepository(context);
            devRepository = new DeveloperRepository(context);
            gameRepository = new GameRepository(context);
        }
        public GameController(DreamContext context, DeveloperView developerView)
        {
            this.context = context;

            this.developerView = developerView;

            genreController = new GenreController(context, developerView);

            gameDeveloperRepository = new GameDeveloperRepository(context);
            devRepository = new DeveloperRepository(context);
            gameRepository = new GameRepository(context);

        }

        public IEnumerable<string> BrowseDownloadedGames(User user)
        {
            List<string> result = new List<string>();
            int index = 1;
            /* Outputs all games downloaded by given user */
            foreach (var game in gameRepository.GetAll().OrderByDescending(x => x.GenreId).ThenByDescending(x => x.Name))
            {
                if (game.Downloads.Any(x => x.UserId == user.UserId))
                {
                    result.Add($"{game.Name}░{game.Price:f2}$░{game.RequiredMemory:f2}GB░{game.Genre.Name}");
                }
                index++;
            }

            return result;
        }
        public IEnumerable<string> BrowseLikedGames(User user)
        {
            List<string> result = new List<string>();
            int index = 1;

            /* Outputs all games liked by given user */
            foreach (var game in gameRepository.GetAll().OrderByDescending(x => x.GenreId).ThenByDescending(x => x.Name))
            {
                if (game.Likes.Any(x => x.UserId == user.UserId))
                {
                    result.Add($"{game.Name}░{game.Price:f2}$░{game.RequiredMemory:f2}GB░{game.Genre.Name}");
                }
                index++;
            }

            return result;
        }
        public IEnumerable<string> BrowseGames()
        {
            List<string> result = new List<string>();

            /* Outputs all games */
            foreach (var game in gameRepository.GetAll().OrderByDescending(x => x.Likes.Count()).ThenByDescending(x => x.Downloads.Count()))
            {
                result.Add($"{game.Name}░{game.Price:f2}$░{game.RequiredMemory:f2}GB░{game.Likes.Count}░{game.Downloads.Count}░{game.Genre.Name}░{game.Description}");
            }
            return result;
        }

        public Game GetMostLikedGame()
        {
            return gameRepository.GetAll()
                .OrderByDescending(x => x.Likes.Count)
                .FirstOrDefault();
        }
        public Game GetMostDownloadedGame()
        {
            return gameRepository.GetAll()
                .OrderByDescending(x => x.Downloads.Count)
                .FirstOrDefault();
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

        public int AddGame(Developer developer)
        {
            /*Validating if the game has a valid name*/
            if (string.IsNullOrWhiteSpace(developerView.GameName))
            {
                developerView.InvalidGameName();
            }
            else if (string.IsNullOrWhiteSpace(developerView.GenreName))
            {
                developerView.InvalidGenreName();
            }
            else if (genreController.GetGenreByName(developerView.GenreName) == null && developerView.GenreAgeRequirement_Textbox.Visibility == Visibility.Hidden)
            {
                developerView.ShowGenreInput();
            }
            else if (genreController.GetGenreByName(developerView.GenreName) == null && developerView.GenreAgeRequirement_Textbox.Visibility == Visibility.Visible)
            {
                /* Creating the new genre */

                if (int.TryParse(developerView.GenreAgeRequirement_Textbox.Text, out int num))
                {
                    if (int.Parse(developerView.GenreAgeRequirement_Textbox.Text) > 0)
                        developerView.AgeRequirements = int.Parse(developerView.GenreAgeRequirement_Textbox.Text);
                }
                else
                {
                    developerView.AgeRequirements = 0;
                }

                genreController.AddGenre();
                Genre genre = genreController.GetGenreByName(developerView.GenreName);

                /* Creating the game */
                return AddingGame(genre, developer);
            }
            else
            {
                Genre genre = genreController.GetGenreByName(developerView.GenreName);
                return AddingGame(genre, developer);
            }
            return 0;
        }
        private int AddingGame(Genre genre, Developer developer)
        {
            /* Creating the game */
            Game game = new Game()
            {
                Name = developerView.GameName,
                Price = developerView.Price,
                RequiredMemory = developerView.RequiredMemory,
                Description = developerView.Description
            };

            /*Mapping genre and game*/
            genre.Games.Add(game);
            game.GenreId = genre.GenreId;

            /*Mapping game and its main developer*/
            GameDeveloper gameCurrentDeveloper = new GameDeveloper()
            {
                DeveloperId = developer.DeveloperId,
                GameId = game.GameId
            };

            gameDeveloperRepository.Add(gameCurrentDeveloper);
            developer.GameDevelopers.Add(gameCurrentDeveloper);
            game.GameDevelopers.Add(gameCurrentDeveloper);

            /*Mapping the game with all codevelopers*/
            foreach (var coDevEmail in developerView.DeveloperEmails)
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

            /*Saving the changes*/
            gameRepository.Add(game);
            gameRepository.Save();

            gameCurrentDeveloper.Developer = developer;
            gameRepository.Save();

            developerView.SuccesfullyCreatedGame();

            return game.GameId;
        }
    }
}
