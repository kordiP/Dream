using Dream.Views;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Controllers
{
    public class IndexController
    {
        private IndexView view;
        private UserController userController;
        public IndexController(UserController userController)
        {
            view = new IndexView();
            this.userController = userController;
            CommandInterpreter();
        }
        private void CommandInterpreter()
        {
            switch (view.Key)
            {
                case ConsoleKey.NumPad1 or ConsoleKey.D1:
                    view.Print(new string('-', 50));

                    int userId = userController.AddUser();

                    view.Print($"Succesfully added {userController.GetUserUsername(userId)}");
                    view.Print(new string('-', 50));

                    LoggedUserController loggedUserController = new LoggedUserController(userController.GetUser(userId), userController);
                    break;
                case ConsoleKey.NumPad2 or ConsoleKey.D2:
                    view.Print(new string('-', 50));
                    view.Print("Sign up as developer");
                    break;
                case ConsoleKey.NumPad3 or ConsoleKey.D3:
                    view.Print(new string('-', 50));
                    view.Print("Sign in");
                    break;
                case ConsoleKey.NumPad4 or ConsoleKey.D4:
                    view.Print(new string('-', 50));
                    view.Print("Browse games");
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                default:
                    view.Print(new string('-', 50));
                    view = new IndexView();
                    CommandInterpreter();
                    break;
            }
        }

    }
}
