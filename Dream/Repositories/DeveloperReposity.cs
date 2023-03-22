using Dream.Data.Models;
using Dream.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Dream.Repositories
{
    public class DeveloperRepository : IRepository<Developer>
    {
        private DreamContext context;
        public DeveloperRepository(DreamContext context)
        { this.context = context; }
        public void Add(Developer developer)
        {
            context.Developers.Add(developer);
        }

        public void Update(Developer developer)
        {
            context.Update(developer);
            Save();
        }

        public void Delete(Developer developer)
        {
            context.Developers.Remove(developer);
            Save();
        }

        public Developer Get(int id)
        {
            return context.Developers.FirstOrDefault(x => x.DeveloperId == id);
        }
        public Developer GetByEmail(string email)
        {
            return context.Developers.FirstOrDefault(x => x.Email == email);
        }

        public List<Developer> GetAll()
        {
            return context.Developers.ToList();
        }
        public bool DeveloperEmailExists(string email)
        {
            return context.Developers.Any(x => x.Email == email);
        }

        public bool Exists(int id)
        {
            return context.Developers.Any(x => x.DeveloperId == id);
        }

        public void Save()
        {
            bool saveFailed;
            do
            {
                saveFailed = false;

                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    /* Update the values of the entity that failed to save from the store */
                    ex.Entries.Single().Reload();
                }

            } while (saveFailed);
        }
    }
}
