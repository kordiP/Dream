using Data.Models;
using Dream.Data.Models;
using Dream.Repositories.IRepositories;

namespace Dream.Repositories
{
    public class GameDeveloperRepository : IRepository<GameDeveloper>
    {
        private DreamContext context;
        public GameDeveloperRepository(DreamContext context)
        { 
            this.context = context; 
        }

        public void Add(GameDeveloper download)
        {
            context.GamesDevelopers.Add(download);
        }

        public void Delete(GameDeveloper download)
        {
            context.GamesDevelopers.Remove(download);
            Save();
        }

        public List<GameDeveloper> GetAll()
        {
            return context.GamesDevelopers.ToList();
        }

        public bool Exists(int id)
        {
            if (context.GamesDevelopers.Any(x => x.GameId == id)) return true;
            else if (context.GamesDevelopers.Any(x => x.DeveloperId == id)) return true;
            else return false;
        }

        public GameDeveloper Get(int id)
        {
            if (context.GamesDevelopers.Any(x => x.GameId == id))
                return context.GamesDevelopers.FirstOrDefault(x => x.GameId == id);
            else if (context.GamesDevelopers.Any(x => x.DeveloperId == id))
                return context.GamesDevelopers.FirstOrDefault(x => x.DeveloperId == id);
            else return null;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(GameDeveloper model)
        {
            context.Update(model);
            Save();
        }
    }
}
