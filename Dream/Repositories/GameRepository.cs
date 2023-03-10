using Dream.Data.Models;
using Dream.Repositories.IRepositories;

namespace Dream.Repositories
{
    public class GameRepository : IGameRepository
    {
        private DreamContext context;
        public GameRepository()
        { 
            this.context = new DreamContext(); 
        }
        public void Add(Game game)
        {
            context.Games.Add(game);
        }

        public void Delete(int id)
        {
            Game game = context.Games.FirstOrDefault(x => x.GameId == id);
            context.Games.Remove(game);
            Save();
        }

        public Game GetById(int id)
        {
            return context.Games.FirstOrDefault(x => x.GameId == id);
        }

        public IEnumerable<Game> GetAll()
        {
            return context.Games.ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
