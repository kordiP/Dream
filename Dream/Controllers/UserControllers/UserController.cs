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
            while (!IsUserEmailValid(view.Email))
            {
                view.InvalidEmail();
                view = new UserSigningView();
            }

            while (!IsUsernameValid(view.Username))
            {
                view.InvalidEmail();
                view = new UserSigningView();
            }

            /* Adding the user */
            User user = new User()
            {
                Username = view.Username,
                Email = view.Email,
                FirstName = view.FirstName,
                LastName = view.LastName,
                Age = view.Age < 0 ? 0 : view.Age
            };
            userRepository.Add(user);
            userRepository.Save();
            return user.UserId;
        }
        public string DeleteUser(User user)
        {
            string username = user.Username;
            userRepository.Delete(user.UserId);
            return username;
        }
        public bool IsUsernameValid(string username)
        {
            if(string.IsNullOrEmpty(username) || userRepository.UserExists(username)) return false;
            return true;
        }
        public bool IsUserEmailValid(string email)
        {
            if (string.IsNullOrEmpty(email) || userRepository.UserEmailExists(email)) return false;
            return true;
        }
        public string GetUserUsername(int id)
        {
            string username = userRepository.Get(id).Username;
            return username;
        }
        public User GetUser(int id)
        {
            User user = userRepository.Get(id);
            return user;
        }
        public User GetUser(string username)
        {
            User user = userRepository.Get(username);
            return user;
        }
    }
}
