using Dream.Data.Models;
using Dream.Repositories.IRepositories;

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
