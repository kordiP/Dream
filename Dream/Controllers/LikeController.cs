using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views;

namespace Dream.Controllers
{
    public class LikeController
    {
        private LikeRepository likeRepository;
        private GameRepository gameRepository;
        private LikeView likeView;
        public LikeController()
        {
            likeRepository = new LikeRepository();
            gameRepository = new GameRepository();
        }

        public Game IsLikeable(User user)
        {
            GameController gameController = new GameController();

            likeView = new LikeView(gameController.BrowseLikedGames(user));
            Game game = null;

            try
            {
                game = gameRepository.GetAll()
                                    .OrderByDescending(x => x.Likes.Count())
                                    .ThenByDescending(x => x.Downloads.Count())
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

            Like like = new Like()
            {
                UserId = user.UserId,
                GameId = game.GameId
            };

            user.Likes.Add(like);
            game.Likes.Add(like);

            likeRepository.Add(like);
            likeRepository.Save();
            likeView.LikedGame(game.Name);

            return user.UserId;
        }

        public DateTime DeleteLike(Like like)
        {
            DateTime time = like.Date;
            likeRepository.Delete(like.UserId, like.GameId);
            return time;
        }

        public int LikedGamesByUser(User user)
        {
            BrowseLikesView likesView = new BrowseLikesView(likeRepository.GetByUserId(user.UserId));

            if (likeRepository.GetByUserId(user.UserId).Count() == 0)
            {
                likesView.NoLikes();
            }
            else
            {
                likesView.ShowLikes();
            }
            return likeRepository.GetByUserId(user.UserId).Count();
        }
        public int GetUserLikesCount(int userId)
        {
            int result = likeRepository.GetByUserId(userId).Count();
            return result;
        }
    }
}
