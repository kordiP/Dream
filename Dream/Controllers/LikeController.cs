using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views;

namespace Dream.Controllers
{
                /* --- Summary --- */
    /* --- This controller is responsible for --- */
        /* --- like CRUD operations --- */

    public class LikeController
    {
        private LikeRepository likeRepository;
        private GameRepository gameRepository;
        private LikeView likeView;

        private DreamContext context;
        public LikeController(DreamContext context)
        {
            this.context = context;
            this.likeRepository = new LikeRepository(context);
            this.gameRepository = new GameRepository(context);
        }

        /* --- Checks if the game has been already liked --- */
        public Game IsLikeable(User user)
        {
            GameController gameController = new GameController(context);

            likeView = new LikeView(gameController.BrowseLikedGames(user));
            Game game = null;

            try
            {
                game = gameController
                        .GetBestGames()
                        .ElementAt(likeView.GameNumber - 1);
            }
            catch (Exception)
            {
                likeView.InvalidGame();
                return null;
            }

            if (game.Likes.Any(x => x.UserId == user.UserId))
            {
                user.Likes.Remove(game.Likes.FirstOrDefault(x => x.UserId == user.UserId));
                DeleteLike(game.Likes.FirstOrDefault(x => x.UserId == user.UserId));
                likeView.RemovedGame(game.Name);

                likeRepository.Save();
                return null;
            }

            return game;
        }

        public int AddLike(User user)
        {
            Game game = IsLikeable(user);
            if (game is null) return -1;

            /* --- Creating the like --- */
            Like like = new Like()
            {
                UserId = user.UserId,
                GameId = game.GameId
            };

            user.Likes.Add(like);
            game.Likes.Add(like);

            likeRepository.Add(like);

            /* --- Saving the changes --- */
            likeRepository.Save();
            likeView.LikedGame(game.Name);

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
            /* --- Gets liked games of user --- */
            IEnumerable<Like> userLikes = GetUserLikes(user.UserId);

            /* --- Sends them to the interface --- */
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
        public List<Like> GetUserLikes(int userId)
        {
            return likeRepository.GetAll().Where(x => x.UserId == userId).ToList();
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
