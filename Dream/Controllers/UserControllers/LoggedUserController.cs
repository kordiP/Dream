using Dream.Data.Models;
using Dream.Views.UserViews;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Dream.Controllers.UserControllers
{
    public class LoggedUserController
    {
        private UserLoggedView loggedView;
        private UserDepositView depositView;
        private UserController userController;
        private User currentUser;
        private IndexController indexController;
        public LoggedUserController(User user)
        {
            currentUser = user;
            this.userController = new UserController();
            loggedView = new UserLoggedView(currentUser.Username);
            CommandInterpreter();
        }
        private void CommandInterpreter()
        {
            switch (loggedView.Key)
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
                    string username = userController.DeleteUser(currentUser);
                    loggedView.DeletedUser(username);
                    indexController = new IndexController();
                    break;
                case ConsoleKey.NumPad8 or ConsoleKey.D8:
                    depositView = new UserDepositView(currentUser.Username);
                    decimal currentBalance = userController.Deposit(currentUser, depositView.Amount);
                    depositView.SuccessfulDeposit(currentBalance);
                    loggedView = new UserLoggedView(currentUser.Username);
                    CommandInterpreter();
                    break;
                case ConsoleKey.NumPad9 or ConsoleKey.D9:
                    indexController = new IndexController();
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                default:
                    loggedView = new UserLoggedView(currentUser.Username);
                    CommandInterpreter();
                    break;
            }
        }
    }
}
