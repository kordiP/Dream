using Data.Models;
using Dream.Data.Models;
using Dream.Repositories.IRepositories;

namespace Dream.Repositories
{
    public class GameDeveloperRepository : IGameDeveloperRepository
    {
        private DreamContext context;
        public GameDeveloperRepository(DreamContext context)
        { this.context = context; }

        public void Add(GameDeveloper download)
        {
            context.GamesDevelopers.Add(download);
            Save();
        }

        public void Delete(int developerId, int gameId)
        {
            GameDeveloper download = context.GamesDevelopers.FirstOrDefault(x => x.DeveloperId == developerId && x.GameId == gameId);
            context.GamesDevelopers.Remove(download);
            Save();
        }

        public IEnumerable<GameDeveloper> GetAll()
        {
            return context.GamesDevelopers.ToList();
        }

        public IEnumerable<GameDeveloper> GetByGameId(int gameId)
        {
            return context.GamesDevelopers.Where(x => x.GameId == gameId).ToList();
        }

        public GameDeveloper GetById(int developerId, int gameId)
        {
            return context.GamesDevelopers.FirstOrDefault(x => x.DeveloperId == developerId && x.GameId == gameId);
        }

        public IEnumerable<GameDeveloper> GetByDeveloperId(int developerId)
        {
            return context.GamesDevelopers.Where(x => x.DeveloperId == developerId).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
