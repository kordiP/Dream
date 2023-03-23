using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views.DeveloperViews;

namespace Dream.Controllers.DeveloperControllers
{
                /* --- Summary --- */
    /* --- This controller is responsible for --- */
        /* --- developer CRUD operations --- */

    public class DeveloperController
    {
        private DeveloperRepository developerRepository;
        private GameDeveloperRepository gameDeveloperRepository;

        private GameController gameController;

        private DreamContext context;

        public DeveloperController(DreamContext context)
        {
            this.context = context;
            this.gameController = new GameController(context);
            this.developerRepository = new DeveloperRepository(context);
            this.gameDeveloperRepository = new GameDeveloperRepository(context);
        }
        public int AddDeveloper()
        {
            DeveloperSigningView view = new DeveloperSigningView();

            /* --- Validation --- */
            while (string.IsNullOrEmpty(view.Email) || IsDeveloperCreated(view.Email))
            {
                view.InvalidEmail();
                return AddDeveloper();
            }

            while (string.IsNullOrWhiteSpace(view.FirstName) || string.IsNullOrWhiteSpace(view.LastName))
            {
                view.InvalidName();
                return AddDeveloper();
            }

            /* --- Creating the developer --- */
            Developer developer = new Developer()
            {
                Email = view.Email,
                FirstName = view.FirstName,
                LastName = view.LastName,
            };

            developerRepository.Add(developer);

            /* --- Saving changes --- */
            developerRepository.Save();
            return developer.DeveloperId;
        }

        public Developer UpdateDeveloper(Developer developer)
        {
            DeveloperUpdateView updateView = new DeveloperUpdateView(developer.Email, developer.FirstName, developer.LastName);

            /* --- Validation --- */
            while ((IsDeveloperCreated(updateView.Email) && updateView.Email != developer.Email) || string.IsNullOrWhiteSpace(updateView.Email))
            {
                updateView.InvalidEmail();
                UpdateDeveloper(developer);
            }

            while (string.IsNullOrEmpty(updateView.FirstName) || string.IsNullOrEmpty(updateView.LastName))
            {
                updateView.InvalidName();
                UpdateDeveloper(developer);
            }

            /* --- Updating the developer ---- */
            developer.Email = updateView.Email;
            developer.FirstName = updateView.FirstName;
            developer.LastName = updateView.LastName;

            developerRepository.Update(developer);

            /* --- Saving changes --- */
            developerRepository.Save();
            updateView.SuccessfulUpdate();

            return developer;
        }

        public string DeleteDeveloper(Developer dev)
        {
            string name = GetDeveloperFullname(dev.DeveloperId);
            developerRepository.Delete(dev);
            return name;
        }

        public Developer LogDeveloper()
        {
            DeveloperLoggingView logView = new DeveloperLoggingView();

            /* --- Validation --- */
            while (string.IsNullOrWhiteSpace(logView.Email) || !IsDeveloperCreated(logView.Email))
            {
                logView.InvalidEmail();
            }

            return GetDeveloper(logView.Email);
        }

        /* --- Returns a collection of games made by developer with additional information --- */
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

        public bool IsDeveloperCreated(string email)
        {
            return developerRepository.DeveloperEmailExists(email);
        }

        public bool IsDeveloperCreated(int id)
        {
            return developerRepository.Exists(id);
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
    }
}
