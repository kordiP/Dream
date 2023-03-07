using Dream.Data.Models;
using Dream.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Repositories
{
    public class GameRepository : IGameRepository
    {
        private DreamContext context;
        public GameRepository(DreamContext context)
        { this.context = context; }
        public void Add(Game game)
        {
            context.Games.Add(game);
            Save();
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
