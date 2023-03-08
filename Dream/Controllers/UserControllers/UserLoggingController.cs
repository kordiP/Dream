using Dream.Data.Models;
using Dream.Views.UserViews;

namespace Dream.Controllers.UserControllers
{
    public class UserLoggingController
    {
        private UserLoggingView view;
        private DreamContext context;
        public UserLoggingController()
        {
            this.view = new UserLoggingView();
            this.context = new DreamContext();
        }
        public User LogUser()
        {
            return context.Users.FirstOrDefault(x => x.Username == view.Username);
        }
    }
}
