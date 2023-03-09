using Dream.Controllers.DeveloperControllers;
using Dream.Data.Models;
using Dream.Views.DeveloperViews;

namespace Dream.Controllers.UserControllers
{
    public class DeveloperLoggingController
    {
        private DeveloperLoggingView view;
        private DeveloperController controller;
        public DeveloperLoggingController()
        {
            this.view = new DeveloperLoggingView();
            this.controller = new DeveloperController();
        }
        public Developer LogDeveloper()
        {
            if (!controller.IsDeveloperEmailValid(view.Email))
            {
                return controller.GetDeveloper(view.Email);
            }

            view.InvalidEmail();
            return LogDeveloper();
        }
    }
}
