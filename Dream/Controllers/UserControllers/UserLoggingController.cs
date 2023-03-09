using Dream.Data.Models;
using Dream.Views.UserViews;

namespace Dream.Controllers.UserControllers
{
    public class UserLoggingController
    {
        private UserLoggingView view;
        private UserController controller;
        public UserLoggingController()
        {
            this.view = new UserLoggingView();
            this.controller = new UserController();
        }
        public User LogUser()
        {
            if (!controller.IsUsernameValid(view.Username))
            {
                return controller.GetUser(view.Username);
            }

            view.InvalidUsername();
            return LogUser();
        }
    }
}
