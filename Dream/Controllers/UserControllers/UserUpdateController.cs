using Azure;
using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views.UserViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Controllers.UserControllers
{
    public class UserUpdateController
    {
        private UserController userController;
        private UserRepository userRepository;
        private User currentUser;
        private UserUpdateView view;

        public UserUpdateController(User user)
        {
            userController = new UserController();
            currentUser = user;
            this.userRepository = new UserRepository();
            view = new UserUpdateView(user.Username, user.Email, user.FirstName, user.LastName, user.Age);
        }
        public User UpdateUser(User user)
        {
            while (view.OldEmail != view.Email && !userController.IsUserEmailValid(view.Email))
            {
                view.InvalidEmail();
                view = new UserUpdateView(user.Username, user.Email, user.FirstName, user.LastName, user.Age);
            }

            while (view.OldUsername != view.Username && !userController.IsUsernameValid(view.Username))
            {
                view.InvalidUsername();
                view = new UserUpdateView(user.Username, user.Email, user.FirstName, user.LastName, user.Age);
            }

            user.Username = view.Username;
            user.Email = view.Email;
            user.FirstName = view.FirstName;
            user.LastName = view.LastName;
            user.Age = view.Age < 0 ? 0 : view.Age;
            userRepository.Update(user);
            view.SuccessfulUpdate();
            return user;
        }
    }
}
