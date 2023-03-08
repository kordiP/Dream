using Dream.Data.Models;
using Dream.Views.DeveloperViews;

namespace Dream.Controllers.UserControllers
{
    public class DeveloperLoggingController
    {
        private DeveloperLoggingView view;
        private DreamContext context;
        public DeveloperLoggingController()
        {
            this.view = new DeveloperLoggingView();
            this.context = new DreamContext();
        }
        public Developer LogDeveloper()
        {
            return context.Developers.FirstOrDefault(x => x.Email == view.Email);
        }
    }
}
