﻿using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Developer developer = new Developer()
            {
                Email = view.Email,
                FirstName = view.FirstName,
                LastName = view.LastName,
            };
            developerRepository.Add(developer);
            return developer.DeveloperId;
        }
        public string GetDeveloperFullname(int id)
        {
            string fullName = developerRepository.GetById(id).FirstName + " " + developerRepository.GetById(id).LastName;
            return fullName;
        }
        public Developer GetDeveloper(int id)
        {
            Developer developer = developerRepository.GetById(id);
            return developer;
        }

    }
}
