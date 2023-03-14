using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views;

namespace Dream.Controllers
{
    public class DownloadController
    {
        private DownloadRepository downloadRepository;
        private BrowseDownloadsView downloadsView;
        private User currentUser;
        public DownloadController(User user)
        {
            currentUser = user;
            downloadRepository = new DownloadRepository();
        }
        public int DownloadedGamesByUser()
        {
            downloadsView = new BrowseDownloadsView(downloadRepository.GetByUserId(currentUser.UserId));

            if (downloadRepository.GetByUserId(currentUser.UserId).Count() == 0)
            {
                downloadsView.NoDownloads();
            }
            else
            {
                downloadsView.ShowDownloads();
            }
            return downloadRepository.GetByUserId(currentUser.UserId).Count();
        }
    }
}
