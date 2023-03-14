using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views;

namespace Dream.Controllers
{
    public class LikeController
    {
        private LikeRepository likeRepository;
        private BrowseLikesView likesView;
        private User currentUser;
        public LikeController(User user)
        {
            currentUser = user;
            likeRepository = new LikeRepository();
        }
        public int LikedGamesByUser()
        {
            likesView = new BrowseLikesView(likeRepository.GetByUserId(currentUser.UserId));

            if (likeRepository.GetByUserId(currentUser.UserId).Count() == 0)
            {
                likesView.NoLikes();
            }
            else
            {
                likesView.ShowLikes();
            }
            return likeRepository.GetByUserId(currentUser.UserId).Count();
        }
    }
}
