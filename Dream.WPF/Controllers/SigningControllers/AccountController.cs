using Dream.Controllers;
using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views.DeveloperViews;
using Dream.Views.UserViews;
using System.Collections.Generic;
using System.Linq;

namespace Dream.WPF.Controllers.SigningControllers
{
    public class AccountController
    {
        private UserRepository userRepository;
        private DreamContext context;
        private DeveloperRepository developerRepository;
        private GameDeveloperRepository gameDeveloperRepository;
        private SignUp signUpView;
        private LogIn logInView;

        private GameController gameController;
        public AccountController(DreamContext context)
        {
            this.context = context;

            userRepository = new UserRepository(context);

            gameController = new GameController(context);
            developerRepository = new DeveloperRepository(context);
            gameDeveloperRepository = new GameDeveloperRepository(context);
        }

        public AccountController(DreamContext context, SignUp signUpView) // for sign up
        {
            this.context = context;

            this.signUpView = signUpView;

            userRepository = new UserRepository(context);

            gameController = new GameController(context);
            developerRepository = new DeveloperRepository(context);
            gameDeveloperRepository = new GameDeveloperRepository(context);
        }
        public AccountController(DreamContext context, LogIn logIn) // for log in
        {
            this.context = context;

            this.logInView = logIn;

            userRepository = new UserRepository(context);

            gameController = new GameController(context);
            developerRepository = new DeveloperRepository(context);
            gameDeveloperRepository = new GameDeveloperRepository(context);
        }

        public User AddUser()
        {
            /* Validation */
            if (string.IsNullOrWhiteSpace(signUpView.User_Email) || IsUserEmailCreated(signUpView.User_Email))
            {
                signUpView.InvalidEmail();
            }

            else if (string.IsNullOrWhiteSpace(signUpView.User_Username) || IsUsernameCreated(signUpView.User_Username))
            {
                signUpView.InvalidUsername();
            }

            else if (string.IsNullOrWhiteSpace(signUpView.User_FirstName) || string.IsNullOrWhiteSpace(signUpView.User_LastName))
            {
                signUpView.InvalidName();
            }
            else
            {
                /* Adding the user */
                User user = new User()
                {
                    Username = signUpView.User_Username,
                    Email = signUpView.User_Email,
                    FirstName = signUpView.User_FirstName,
                    LastName = signUpView.User_LastName,
                    Age = signUpView.User_Age < 0 ? 0 : signUpView.User_Age
                };

                userRepository.Add(user);
                userRepository.Save();

                signUpView.LogUserIn(user);

                return user;
            }
            return null;
        }
        public Developer AddDeveloper()
        {
            /* Validation */
            if (string.IsNullOrEmpty(signUpView.Dev_Email) || IsDeveloperCreated(signUpView.Dev_Email))
            {
                signUpView.InvalidEmail();
            }

            else if (string.IsNullOrWhiteSpace(signUpView.Dev_FirstName) || string.IsNullOrWhiteSpace(signUpView.Dev_LastName))
            {
                signUpView.InvalidName();
            }
            else 
            {
                /* Adding the developer */
                Developer developer = new Developer()
                {
                    Email = signUpView.Dev_Email,
                    FirstName = signUpView.Dev_FirstName,
                    LastName = signUpView.Dev_LastName,
                };

                developerRepository.Add(developer);
                developerRepository.Save();

                signUpView.LogDevIn(developer);

                return developer;
            }
            return null;
        }

        public User LogUser()
        {

            if (string.IsNullOrWhiteSpace(logInView.User_Username) || !IsUsernameCreated(logInView.User_Username))
            {
                logInView.InvalidUsername();
            }
            else
            {
                logInView.LogUserIn(GetUser(logInView.User_Username));
            }
            return GetUser(logInView.User_Username);
        }
        public Developer LogDeveloper()
        {

            if (string.IsNullOrWhiteSpace(logInView.Dev_Email) || !IsDeveloperCreated(logInView.Dev_Email))
            {
                logInView.InvalidEmail();
            }
            else
            {
                logInView.LogDevIn(GetDeveloper(logInView.Dev_Email));
            }
            return GetDeveloper(logInView.Dev_Email);
        }
        public string DeleteUser(User user)
        {
            string username = user.Username;
            userRepository.Delete(user);
            return username;
        }
        public string DeleteDeveloper(Developer dev)
        {
            string name = GetDeveloperFullname(dev.DeveloperId);
            developerRepository.Delete(dev);
            return name;
        }

        public User UpdateUser(User user)
        {
            UserUpdateView updateView = new UserUpdateView(user.Username, user.Email, user.FirstName, user.LastName, user.Age);

            /* Validation */
            while (IsUsernameCreated(updateView.Username) && updateView.Username != user.Username || string.IsNullOrWhiteSpace(updateView.Username))
            {
                updateView.InvalidUsername();
                UpdateUser(user);
            }

            while (IsUserEmailCreated(updateView.Email) && updateView.Email != user.Email || string.IsNullOrWhiteSpace(updateView.Email))
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
        public Developer UpdateDeveloper(Developer developer)
        {
            DeveloperUpdateView updateView = new DeveloperUpdateView(developer.Email, developer.FirstName, developer.LastName);

            /* Validation */
            while (IsDeveloperCreated(updateView.Email) && updateView.Email != developer.Email || string.IsNullOrWhiteSpace(updateView.Email))
            {
                updateView.InvalidEmail();
                UpdateDeveloper(developer);
            }

            while (string.IsNullOrEmpty(updateView.FirstName) || string.IsNullOrEmpty(updateView.LastName))
            {
                updateView.InvalidName();
                UpdateDeveloper(developer);
            }

            /* Updating the developer */
            developer.Email = updateView.Email;
            developer.FirstName = updateView.FirstName;
            developer.LastName = updateView.LastName;

            developerRepository.Update(developer);
            developerRepository.Save();
            updateView.SuccessfulUpdate();

            return developer;
        }

        public bool IsUsernameCreated(string username)
        {
            return userRepository.UserUsernameExists(username);
        }
        public bool IsDeveloperCreated(string email)
        {
            return developerRepository.DeveloperEmailExists(email);
        }

        public bool IsUserEmailCreated(string email)
        {
            return userRepository.UserEmailExists(email);
        }
        public bool IsDeveloperCreated(int id)
        {
            return developerRepository.Exists(id);
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
        public Developer GetDeveloper(int id)
        {
            Developer developer = developerRepository.Get(id);
            return developer;
        }
        public Developer GetDeveloper(string email)
        {
            Developer developer = developerRepository.GetAll().FirstOrDefault(x => x.Email == email);
            return developer;
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

        public IEnumerable<string> BrowseGamesAsDeveloper(Developer developer)
        {
            List<string> result = new List<string>();

            foreach (Game game in gameController.GetGamesOfDeveloper(developer.DeveloperId))
            {
                result.Add($"{game.Name} - Made by: {string.Join(", ", GetCoDevelopersOfGame(game.GameId).Select(x => x.FirstName))}");
            }

            return result;
        }

        public List<Developer> GetCoDevelopersOfGame(int gameId)
        {
            return gameDeveloperRepository
                   .GetAll()
                   .Where(x => x.GameId == gameId)
                   .Select(x => x.Developer).ToList();
        }

        public string GetDeveloperFullname(int id)
        {
            string fullName = developerRepository.Get(id).FirstName + " " + developerRepository.Get(id).LastName;
            return fullName;
        }


    }
}
