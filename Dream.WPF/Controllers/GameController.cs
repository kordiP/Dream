﻿using Data.Models;
using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views;
using Dream.WPF;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace Dream.Controllers
{
    public class GameController
    {
        private GameRepository gameRepository;
        private DeveloperRepository devRepository;
        private GameDeveloperRepository gameDeveloperRepository;
        private DeveloperView developerView;

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
        public GameController(DreamContext context, DeveloperView developerView)
        {
            this.context = context;

            //developerView = new DeveloperView();

            genreController = new GenreController(context);

            gameDeveloperRepository = new GameDeveloperRepository(context);
            devRepository = new DeveloperRepository(context);
            gameRepository = new GameRepository(context);

        }

        public IEnumerable<string> BrowseDownloadedGames(User user)
        {
            List<string> result = new List<string>();
            int index = 1;

            foreach (var game in gameRepository.GetAll().OrderByDescending(x => x.Likes.Count()).ThenByDescending(x => x.Downloads.Count()))
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
        public IEnumerable<string> BrowseLikedGames(User user)
        {
            List<string> result = new List<string>();
            int index = 1;

            foreach (var game in gameRepository.GetAll().OrderByDescending(x => x.Likes.Count()).ThenByDescending(x => x.Likes.Count()))
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

        public IEnumerable<string> BrowseGames() 
        {
            List<string> result = new List<string>();

            foreach (var game in gameRepository.GetAll())
            {
                result.Add($"{game.Name}░{game.Price:f2}$░{game.RequiredMemory:f2}GB░{game.Likes.Count}░{game.Downloads.Count}░{game.Genre.Name}░{game.Description}");
            }

            //BrowseGamesView view = new BrowseGamesView();
            //if (gameRepository.GetAll().Count() == 0)
            //{
            //    view.NoGamesException();
            //}
            //else
            //{
            //    view.MostPopularGenre(genreController.GetMostPopularGenre().Name);
            //    view.MostLikedGame($"{GetMostLikedGame().Name} - {GetMostLikedGame().Likes.Count}");
            //    view.MostDownloadedGame($"{GetMostDownloadedGame().Name} - {GetMostDownloadedGame().Likes.Count}");
            //}
            //view.AllGamesList(result);
            //view.ExitView();

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
        public int AddGame(Developer developer)
        {
            /*Getting values*/

            /*Validating if the name has a valid name*/
            while (string.IsNullOrWhiteSpace(developerView.Name)) 
            {
                developerView.InvalidGameName();
            }

            while (string.IsNullOrWhiteSpace(developerView.GenreName))
            {
                developerView.InvalidGenreName();
            }

            Genre genre = genreController.GetGenreByName(developerView.GenreName);

            /*Creating the game*/
            Game game = new Game()
            {
                Name = developerView.Name,
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
            // successful
            return game.GameId;
        }
    }
}
