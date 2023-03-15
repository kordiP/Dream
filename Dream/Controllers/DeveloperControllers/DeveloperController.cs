using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views.DeveloperViews;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Dream.Controllers.DeveloperControllers
{
    public class DeveloperController
    {
        private DeveloperRepository developerRepository;
        private GameDeveloperRepository gameDeveloperRepository;

        public DeveloperController()
        {
            this.developerRepository = new DeveloperRepository();
            this.gameDeveloperRepository = new GameDeveloperRepository();
        }
        public int AddDeveloper()
        {
            DeveloperSigningView view = new DeveloperSigningView();

            /* Validation */
            while (string.IsNullOrEmpty(view.Email) || IsDeveloperCreated(view.Email))
            {
                view.InvalidEmail();
                return AddDeveloper();
            }

            while (string.IsNullOrEmpty(view.FirstName) || string.IsNullOrEmpty(view.LastName))
            {
                view.InvalidName();
                return AddDeveloper();
            }

            /* Adding the developer */
            Developer developer = new Developer()
            {
                Email = view.Email,
                FirstName = view.FirstName,
                LastName = view.LastName,
            };
            developerRepository.Add(developer);
            developerRepository.Save();
            return developer.DeveloperId;
        }

        public Developer UpdateDeveloper(Developer developer)
        {
            DeveloperUpdateView updateView = new DeveloperUpdateView(developer.Email, developer.FirstName, developer.LastName);

            while ((IsDeveloperCreated(updateView.Email) && updateView.Email != developer.Email) || string.IsNullOrWhiteSpace(updateView.Email))
            {
                updateView.InvalidEmail();
                updateView = new DeveloperUpdateView(developer.Email, developer.FirstName, developer.LastName);
            }

            while (string.IsNullOrEmpty(updateView.FirstName) || string.IsNullOrEmpty(updateView.LastName))
            {
                updateView.InvalidName();
                updateView = new DeveloperUpdateView(developer.Email, developer.FirstName, developer.LastName);
            }


            developer.Email = updateView.Email;
            developer.FirstName = updateView.FirstName;
            developer.LastName = updateView.LastName;

            developerRepository.Update(developer);
            developerRepository.Save();
            updateView.SuccessfulUpdate();
            return developer;
        }

        public string DeleteDeveloper(Developer dev)
        {
            string name = GetDeveloperFullname(dev.DeveloperId);
            developerRepository.Delete(dev.DeveloperId);
            return name;
        }

        public Developer LogDeveloper()
        {
            DeveloperLoggingView logView = new DeveloperLoggingView();

            while (string.IsNullOrWhiteSpace(logView.Email) || !IsDeveloperCreated(logView.Email))
            {
                logView.InvalidEmail();
            }

            return GetDeveloper(logView.Email);
        }

        public IEnumerable<string> BrowseGamesAsDeveloper(Developer developer)
        {
            List<string> result = new List<string>();
            List<Game> gamesOfDeveloper = gameDeveloperRepository
                                        .GetByDeveloperId(developer.DeveloperId)
                                        .Select(x => x.Game).ToList();

            foreach (Game game in gamesOfDeveloper)
            {
                List<Developer> coDeveloperOfTheGame = gameDeveloperRepository
                                                        .GetByGameId(game.GameId)
                                                        .Select(x => x.Developer).ToList();
                result.Add($"{game.Name} - {string.Join(", ", coDeveloperOfTheGame.Select(x => x.FirstName))}");
            }

            return result;
        }
        public int DeveloperGameCount(Developer developer)
        {
            return gameDeveloperRepository.GetByDeveloperId(developer.DeveloperId).Count();
        }
        public int DeveloperLikeCount(Developer developer)
        {
            return gameDeveloperRepository
                .GetByDeveloperId(developer.DeveloperId)
                .Sum(x => x.Game.Likes.Count());
        }
        public int DeveloperDownloadCount(Developer developer)
        {
            return gameDeveloperRepository
                .GetByDeveloperId(developer.DeveloperId)
                .Sum(x => x.Game.Downloads.Count());
        }


        public string GetDeveloperFullname(int id)
        {
            string fullName = developerRepository.Get(id).FirstName + " " + developerRepository.Get(id).LastName;
            return fullName;
        }
        public bool IsDeveloperCreated(string email)
        {
            return developerRepository.DeveloperExists(email);
        }
        public bool IsDeveloperCreated(int id)
        {
            return developerRepository.DeveloperExists(id);
        }
        public Developer GetDeveloper(int id)
        {
            Developer developer = developerRepository.Get(id);
            return developer;
        }
        public Developer GetDeveloper(string email)
        {
            Developer developer = developerRepository.Get(email);
            return developer;
        }
    }
}
