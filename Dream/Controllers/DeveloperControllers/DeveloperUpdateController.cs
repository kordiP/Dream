using Dream.Controllers.UserControllers;
using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views.DeveloperViews;
using Dream.Views.UserViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Controllers.DeveloperControllers
{
    public class DeveloperUpdateController
    {
        private DeveloperController developerController;
        private DeveloperRepository developerRepository;
        private Developer currentDeveloper;
        private DeveloperUpdateView view;

        public DeveloperUpdateController(Developer developer)
        {
            developerController = new DeveloperController();
            currentDeveloper = developer;
            this.developerRepository = new DeveloperRepository();
            view = new DeveloperUpdateView(developer.Email, developer.FirstName, developer.LastName);
        }
        public Developer UpdateDeveloper(Developer developer)
        {
            while (view.OldEmail != view.Email && !developerController.IsDeveloperEmailValid(view.Email))
            {
                view.InvalidEmail();
                view = new DeveloperUpdateView(developer.Email, developer.FirstName, developer.LastName);
            }

            developer.Email = view.Email;
            developer.FirstName = view.FirstName;
            developer.LastName = view.LastName;
            developerRepository.Update(developer);
            view.SuccessfulUpdate();
            return developer;
        }

    }
}
