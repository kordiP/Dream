using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views.UserViews;

namespace Dream.Controllers.UserControllers
{
                /* --- Summary --- */
    /* --- This controller is responsible for --- */
          /* --- user CRUD operations --- */

    public class UserController
    {
        private UserRepository userRepository;
        private DreamContext context;

        public UserController(DreamContext context)
        {
            this.context = context;

            this.userRepository = new UserRepository(context);
        }

        public int AddUser()
        {
            UserSigningView signingView = new UserSigningView();

            /* --- Validation --- */
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

            /* --- Creating the user --- */
            User user = new User()
            {
                Username = signingView.Username,
                Email = signingView.Email,
                FirstName = signingView.FirstName,
                LastName = signingView.LastName,
                Age = signingView.Age < 0 ? 0 : signingView.Age
            };

            userRepository.Add(user);

            /* --- Saving changes --- */
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

            /* --- Saving changes --- */
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

            /* --- Validation --- */
            while (string.IsNullOrWhiteSpace(logView.Username) || !IsUsernameCreated(logView.Username))
            {
                logView.InvalidUsername();
            }

            return GetUser(logView.Username);
        }

        public bool IsUsernameCreated(string username)
        {
            return userRepository.UserUsernameExists(username);
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

        public User GetUser(string username)
        {
            User user = userRepository.GetByUsername(username);
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
    }
}
