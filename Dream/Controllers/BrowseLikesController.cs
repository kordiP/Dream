using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views;

namespace Dream.Controllers
{
    public class BrowseLikesController
    {
        private LikeRepository likeRepository;
        private BrowseLikesView likesView;
        private User currentUser;
        public BrowseLikesController(User user)
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
