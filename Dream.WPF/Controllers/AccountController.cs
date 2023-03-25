using Dream.Controllers;
using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views.DeveloperViews;
using Dream.Views.UserViews;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace Dream.WPF.Controllers
{
    public class AccountController
    {
        private UserRepository userRepository;
        private DreamContext context;
        private DeveloperRepository developerRepository;
        private GameDeveloperRepository gameDeveloperRepository;
        private SignUp signUpView;
        private LogIn logInView;
        private UserView userView;
        private DeveloperView developerView;

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

            logInView = logIn;

            userRepository = new UserRepository(context);

            gameController = new GameController(context);
            developerRepository = new DeveloperRepository(context);
            gameDeveloperRepository = new GameDeveloperRepository(context);
        }
        public AccountController(DreamContext context, DeveloperView developerView)
        {
            this.context = context;

            this.developerView = developerView;

            userRepository = new UserRepository(context);

            gameController = new GameController(context);
            developerRepository = new DeveloperRepository(context);
            gameDeveloperRepository = new GameDeveloperRepository(context);

        }
        public AccountController(DreamContext context, UserView userView)
        {
            this.context = context;

            this.userView = userView;

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
            context.ChangeTracker.Clear();
            userRepository.Delete(user);
            return username;
        }
        public string DeleteDeveloper(Developer dev)
        {
            string name = GetDeveloperFullname(dev.DeveloperId);
            context.ChangeTracker.Clear();
            developerRepository.Delete(dev);
            return name;
        }

        public User UpdateUser(User user)
        {

            /* Validation */
            if (IsUsernameCreated(userView.UserUsername) && userView.UserUsername != user.Username || string.IsNullOrWhiteSpace(userView.UserUsername))
            {
                userView.InvalidUsername();
            }

            else if (IsUserEmailCreated(userView.UserEmail) && userView.UserEmail != user.Email || string.IsNullOrWhiteSpace(userView.UserEmail))
            {
                userView.InvalidEmail();
            }

            else if (string.IsNullOrWhiteSpace(userView.UserFirstName) || string.IsNullOrWhiteSpace(userView.UserLastName))
            {
                userView.InvalidName();
            }
            else
            {
                /* Updating the user*/
                user.Username = userView.UserUsername;
                user.Email = userView.UserEmail;
                user.FirstName = userView.UserFirstName;
                user.LastName = userView.UserLastName;
                user.Age = userView.UserAge < 0 ? 0 : userView.UserAge;

                userRepository.Update(user);
                userView.SuccessfulUpdate();
                return user;
            }
            return user;
        }
        public Developer UpdateDeveloper(Developer developer)
        {
            /* Validation */
            if (IsDeveloperCreated(developerView.DevEmail) && developerView.DevEmail != developer.Email || string.IsNullOrWhiteSpace(developerView.DevEmail))
            {
                developerView.InvalidEmail();
            }

            else if (string.IsNullOrEmpty(developerView.DevFirstName) || string.IsNullOrEmpty(developerView.DevLastName))
            {
                developerView.InvalidName();
            }
            else 
            {
                /* Updating the developer */
                developer.Email = developerView.DevEmail;
                developer.FirstName = developerView.DevFirstName;
                developer.LastName = developerView.DevLastName;

                context.ChangeTracker.Clear();
                developerRepository.Update(developer);
                developerRepository.Save();
                developerView.SuccessfulUpdate();

                return developer;

            }
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
                result.Add($"{game.Name}░{game.Price:f2}$░{string.Join(", ", GetCoDevelopersOfGame(game.GameId).Select(x => x.FirstName))}░{game.Likes.Count}░{game.Downloads.Count}░{game.Genre.Name}░{game.Description}");
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
