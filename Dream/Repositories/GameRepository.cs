using Dream.Data.Models;
using Dream.Repositories.IRepositories;

namespace Dream.Repositories
{
    public class GameRepository : IRepository<Game>
    {
        private DreamContext context;
        public GameRepository(DreamContext context)
        {
            this.context = context;
        }
        public void Add(Game game)
        {
            context.Games.Add(game);
        }

        public void Delete(Game game)
        {
            context.Games.Remove(game);
            Save();
        }

        public bool Exists(int id)
        {
            return context.Games.Any(x => x.GenreId == id);
        }

        public Game Get(int id)
        {
            return context.Games.FirstOrDefault(x => x.GameId == id);
        }

        public List<Game> GetAll()
        {
            return context.Games.ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Game model)
        {
            context.Update(model).CurrentValues.SetValues(model);
            Save();
        }
    }
}
