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
        public string DeleteUser(User user)
        {
            string username = user.Username;
            userRepository.Delete(user.UserId);
            return username;
        }
        public decimal Deposit(User user, decimal amount)
        {
            if (user.Balance is null)
            {
                user.Balance = amount;
            }
            else
            {
                user.Balance += amount;
            }
            userRepository.Update(user);
            return (decimal) user.Balance;
        }
        public bool IsUsernameValid(string username)
        {
            if(username == "" || userRepository.UserExists(username)) return false;
            return true;
        }
        public bool IsUserEmailValid(string email)
        {
            if (email == "" || userRepository.UserEmailExists(email)) return false;
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
