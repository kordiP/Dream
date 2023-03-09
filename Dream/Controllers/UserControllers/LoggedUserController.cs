using Dream.Data.Models;
using Dream.Views.UserViews;

namespace Dream.Controllers.UserControllers
{
    public class LoggedUserController
    {
        private UserLoggedView view;
        private UserController userController;
        private User currentUser;
        public LoggedUserController(User user)
        {
            currentUser = user;
            this.userController = new UserController();
            view = new UserLoggedView(currentUser.Username);
            CommandInterpreter();
        }
        private void CommandInterpreter()
        {
            switch (view.Key)
            {
                case ConsoleKey.NumPad1 or ConsoleKey.D1:
                    break;
                case ConsoleKey.NumPad2 or ConsoleKey.D2:
                    break;
                case ConsoleKey.NumPad3 or ConsoleKey.D3:
                    break;
                case ConsoleKey.NumPad4 or ConsoleKey.D4:
                    break;
                case ConsoleKey.NumPad5 or ConsoleKey.D5:
                    break;
                case ConsoleKey.NumPad6 or ConsoleKey.D6:
                    break;
                case ConsoleKey.NumPad7 or ConsoleKey.D7:
                    break;
                case ConsoleKey.NumPad8 or ConsoleKey.D8:
                    break;
                case ConsoleKey.NumPad9 or ConsoleKey.D9:
                    IndexController indexController = new IndexController();
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                default:
                    view = new UserLoggedView(currentUser.Username);
                    CommandInterpreter();
                    break;
            }
        }
    }
}
