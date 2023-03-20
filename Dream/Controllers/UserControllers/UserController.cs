using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views.UserViews;

namespace Dream.Controllers.UserControllers
{
    public class UserController
    {
        private UserRepository userRepository;

        public UserController()
        {
            this.userRepository = new UserRepository();
        }
        public int AddUser()
        {
            UserSigningView signingView = new UserSigningView();

            /* Validation */
            while (string.IsNullOrWhiteSpace(signingView.Email) || IsUserEmailCreated(signingView.Email))
            {
                signingView.InvalidEmail();
                return AddUser();
            }

            while (string.IsNullOrWhiteSpace(signingView.Username) || IsUsernameCreated(signingView.Username))
            {
                signingView.InvalidUsername();
                return AddUser();
            }

            while (string.IsNullOrWhiteSpace(signingView.FirstName) || string.IsNullOrWhiteSpace(signingView.LastName))
            {
                signingView.InvalidName();
                return AddUser();
            }

            /* Adding the user */
            User user = new User()
            {
                Username = signingView.Username,
                Email = signingView.Email,
                FirstName = signingView.FirstName,
                LastName = signingView.LastName,
                Age = signingView.Age < 0 ? 0 : signingView.Age
            };

            userRepository.Add(user);
            userRepository.Save();

            return user.UserId;
        }

        public User UpdateUser(User user)
        {
            UserUpdateView updateView = new UserUpdateView(user.Username, user.Email, user.FirstName, user.LastName, user.Age);

            /* Validation */
            while ((IsUsernameCreated(updateView.Username) && updateView.Username != user.Username) || string.IsNullOrWhiteSpace(updateView.Username))
            {
                updateView.InvalidUsername();
                UpdateUser(user);
            }

            while ((IsUserEmailCreated(updateView.Email) && updateView.Email != user.Email) || string.IsNullOrWhiteSpace(updateView.Email))
            {
                updateView.InvalidEmail();
                UpdateUser(user);
            }

            while (string.IsNullOrWhiteSpace(updateView.FirstName) || string.IsNullOrWhiteSpace(updateView.LastName))
            {
                updateView.InvalidName();
                UpdateUser(user);
            }

            /* Updating the user*/
            user.Username = updateView.Username;
            user.Email = updateView.Email;
            user.FirstName = updateView.FirstName;
            user.LastName = updateView.LastName;
            user.Age = updateView.Age < 0 ? 0 : updateView.Age;

            userRepository.Update(user);
            updateView.SuccessfulUpdate();
            return user;
        }

        public string DeleteUser(User user)
        {
            string username = user.Username;
            userRepository.Delete(user);
            return username;
        }

        public User LogUser()
        {
            UserLoggingView logView = new UserLoggingView();

            while (string.IsNullOrWhiteSpace(logView.Username) || !IsUsernameCreated(logView.Username))
            {
                logView.InvalidUsername();
            }

            return GetUser(logView.Username);
        }

        public bool IsUsernameCreated(string username)
        {
            return userRepository.UserExists(username);
        }
        public bool IsUserEmailCreated(string email)
        {
            return userRepository.UserEmailExists(email);
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
        public decimal GetUserBalance(int id)
        {
            User user = userRepository.Get(id);
            if (user.Balance is null)
            {
                return 0;
            }
            else
            {
                return (decimal)user.Balance;
            }
        }
        public User GetUser(string username)
        {
            User user = userRepository.Get(username);
            return user;
        }
    }
}
