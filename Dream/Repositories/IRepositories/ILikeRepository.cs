using Dream.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Repositories.IRepositories
{
    public interface ILikeRepository
    {
        void Add(Like like);
        void Delete(int userId, int gameId);
        IEnumerable<Like> GetAll();
        IEnumerable<Like> GetByGameId(int gameId);
        IEnumerable<Like> GetByUserId(int userId);
        Like GetById(int userId, int gameId);
        void Save();
    }
}
