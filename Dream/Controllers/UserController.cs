using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Controllers
{
    
    public class UserController
    {
        private UserSigningView view;
        private UserRepository userRepository;
        public UserController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public int AddUser()
        {
            view = new UserSigningView();
            User user = new User()
            {
                Username = view.Username,
                Email = view.Email,
                FirstName = view.FirstName,
                LastName = view.LastName,
                Age = view.Age
            };
            userRepository.Add(user);
            return user.UserId;
        }
        public string GetUserUsername(int id)
        {
            string username = userRepository.GetById(id).Username;
            return username;
        }
        public User GetUser(int id)
        {
            User user = userRepository.GetById(id);
            return user;
        }
    }
}
