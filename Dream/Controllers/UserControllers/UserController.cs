using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views.UserViews;

namespace Dream.Controllers.UserControllers
{
    public class UserController
    {
        private UserSigningView view;
        private UserRepository userRepository;
        public UserController()
        {
            this.userRepository = new UserRepository();
        }
        public int AddUser()
        {
            view = new UserSigningView();

            /* Validation */
            if (!IsUserEmailValid(view.Email))
            {
                view.InvalidEmail();
            }

            if (!IsUsernameValid(view.Username))
            {
                view.InvalidUsername();
            }

            /* Adding the user */
            User user = new User()
            {
                Username = view.Username,
                Email = view.Email,
                FirstName = view.FirstName,
                LastName = view.LastName,
                Age = view.Age < 0 ? view.Age : 0
            };
            userRepository.Add(user);
            return user.UserId;
        }
        public bool IsUsernameValid(string username)
        {
            return !userRepository.UserExists(username);
        }
        public bool IsUserEmailValid(string email)
        {
            return !userRepository.UserEmailExists(email);
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
