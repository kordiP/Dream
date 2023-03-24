using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views;
using Dream.WPF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dream.Controllers
{
    public class LikeController
    {
        private LikeRepository likeRepository;
        private GameRepository gameRepository;
        private UserView userView;

        private DreamContext context;
        public LikeController(DreamContext context, UserView userView)
        {
            this.context = context;
            this.likeRepository = new LikeRepository(context);
            this.gameRepository = new GameRepository(context);
            this.userView = userView;
        }
        public LikeController(DreamContext context)
        {
            this.context = context;
            this.likeRepository = new LikeRepository(context);
            this.gameRepository = new GameRepository(context);
        }

        public Game IsLikeable(User user)
        {
            GameController gameController = new GameController(context);

            // gameController.BrowseLikedGames(user);
            Game game = null;

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

            if (game.Likes.Any(x => x.UserId == user.UserId))
            {
                user.Likes.Remove(game.Likes.FirstOrDefault(x => x.UserId == user.UserId));
                DeleteLike(game.Likes.FirstOrDefault(x => x.UserId == user.UserId));
                userView.DislikedGame(game.Name);

                likeRepository.Save();
                return null;
            }

            return game;
        }

        public int AddLike(User user)
        {
            Game game = IsLikeable(user);
            if (game is null) return -1;

            Like like = new Like()
            {
                UserId = user.UserId,
                GameId = game.GameId
            };

            user.Likes.Add(like);
            game.Likes.Add(like);

            likeRepository.Add(like);
            likeRepository.Save();
            userView.LikedGame(game.Name);

            return user.UserId;
        }

        public DateTime DeleteLike(Like like)
        {
            DateTime time = like.Date;
            likeRepository.Delete(like);
            return time;
        }

        public int LikedGamesByUser(User user)
        {
            IEnumerable<Like> userLikes = likeRepository.GetAll().Where(x => x.UserId == user.UserId);
            BrowseLikesView likesView = new BrowseLikesView(userLikes);

            if (userLikes.Count() == 0)
            {
                likesView.NoLikes();
            }
            else
            {
                likesView.ShowLikes();
            }
            return userLikes.Count();
        }
        public int GetUserLikesCount(int userId)
        {
            int result = likeRepository.GetAll().Where(x => x.UserId == userId).Count();
            return result;
        }
        public int GetDeveloperLikesCount(int developerId)
        {
            return gameRepository
                .GetAll()
                .Where(x => x.GameDevelopers.Any(y => y.DeveloperId == developerId))
                .Sum(x => x.Likes.Count());
        }
    }
}
