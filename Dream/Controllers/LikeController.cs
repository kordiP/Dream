using Dream.Data.Models;
using Dream.Repositories;
using Dream.Repositories.IRepositories;
using Dream.Views;

namespace Dream.Controllers
{
    public class LikeController
    {
        private LikeRepository likeRepository;
        private BrowseLikesView likesView;
        public LikeController()
        {
            likeRepository = new LikeRepository();
        }
        public int LikedGamesByUser(User user)
        {
            likesView = new BrowseLikesView(likeRepository.GetByUserId(user.UserId));

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
