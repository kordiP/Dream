﻿using Dream.Data.Models;
using Dream.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Controllers
{
    public class LoggedUserController
    {
        private UserLoggedView view;
        private UserController userController;
        private User currentUser;
        public LoggedUserController(User user, UserController userController)
        {
            this.currentUser = user;            
            this.userController = userController;
            view = new UserLoggedView(currentUser.Username);
            CommandInterpreter();
        }
        private void CommandInterpreter()
        {
            switch (view.Key)
            {
                case ConsoleKey.NumPad1 or ConsoleKey.D1:
                    view.Print("1. Browse games");
                    break;
                case ConsoleKey.NumPad2 or ConsoleKey.D2:
                    view.Print("2. Like game");
                    break;
                case ConsoleKey.NumPad3 or ConsoleKey.D3:
                    view.Print("3. Download game");
                    break;
                case ConsoleKey.NumPad4 or ConsoleKey.D4:
                    view.Print("4. Browse liked games");
                    break;
                case ConsoleKey.NumPad5 or ConsoleKey.D5:
                    view.Print("5. Browse downloaded games");
                    break;
                case ConsoleKey.NumPad6 or ConsoleKey.D6:
                    view.Print("6. Edit profile info");
                    break;
                case ConsoleKey.NumPad7 or ConsoleKey.D7:
                    view.Print("7. Delete profile");
                    break;
                case ConsoleKey.NumPad8 or ConsoleKey.D8:
                    view.Print("8. Deposit money");
                    break;
                case ConsoleKey.NumPad9 or ConsoleKey.D9:
                    view.Print(new string('-', 50));
                    IndexController indexController = new IndexController(userController);
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                default:
                    view.Print(new string('-', 50));
                    view = new UserLoggedView(currentUser.Username);
                    CommandInterpreter();
                    break;
            }

        }

    }
}
