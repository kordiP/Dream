using Dream.Data.Models;
using Dream.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Repositories
{
    public class DeveloperRepository : IDeveloperRepository
    {
        private DreamContext context;
        public DeveloperRepository()
        { this.context = new DreamContext(); }
        public void Add(Developer developer)
        {
            context.Developers.Add(developer);
            Save();
        }

        public void Delete(int id)
        {
            Developer developer = context.Developers.FirstOrDefault(x => x.DeveloperId == id);
            context.Developers.Remove(developer);
            Save();
        }
        public Developer GetById(int id)
        {
            return context.Developers.FirstOrDefault(x => x.DeveloperId == id);
        }

        public IEnumerable<Developer> GetAll()
        {
            return context.Developers.ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
