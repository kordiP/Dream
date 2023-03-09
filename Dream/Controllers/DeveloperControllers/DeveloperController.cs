using Dream.Data.Models;
using Dream.Repositories;
using Dream.Repositories.IRepositories;
using Dream.Views.DeveloperViews;

namespace Dream.Controllers.DeveloperControllers
{
    public class DeveloperController
    {
        private DeveloperSigningView view;
        private DeveloperRepository developerRepository;
        public DeveloperController()
        {
            this.developerRepository = new DeveloperRepository();
        }
        public int AddDeveloper()
        {
            view = new DeveloperSigningView();

            /* Validation */
            if (!IsDeveloperEmailValid(view.Email))
            {
                view.InvalidEmail();
            }

            /* Adding the developer */
            Developer developer = new Developer()
            {
                Email = view.Email,
                FirstName = view.FirstName,
                LastName = view.LastName,
            };
            developerRepository.Add(developer);
            return developer.DeveloperId;
        }
        public bool IsDeveloperEmailValid(string email)
        {
            return !developerRepository.DeveloperExists(email);
        }

        public string GetDeveloperFullname(int id)
        {
            string fullName = developerRepository.Get(id).FirstName + " " + developerRepository.Get(id).LastName;
            return fullName;
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
