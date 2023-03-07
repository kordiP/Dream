using Data.Models;
using Dream.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Repositories.IRepositories
{
    public interface IGameDeveloperRepository
    {
        void Add(GameDeveloper gameDeveloper);
        void Delete(int gameId, int developerId);
        IEnumerable<GameDeveloper> GetAll();
        IEnumerable<GameDeveloper> GetByGameId(int gameId);
        IEnumerable<GameDeveloper> GetByDeveloperId(int developerId);
        GameDeveloper GetById(int gameId, int developerId);
        void Save();
    }
}
