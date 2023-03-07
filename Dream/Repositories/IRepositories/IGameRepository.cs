using Dream.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Repositories.IRepositories
{
    public interface IGameRepository
    {
        void Add(Game game);
        void Delete(int id);
        IEnumerable<Game> GetAll();
        Game GetById(int id);
        void Save();
    }
}
