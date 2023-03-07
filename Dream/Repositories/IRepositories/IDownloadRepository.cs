using Dream.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Repositories.IRepositories
{
    public interface IDownloadRepository
    {
        void Add(Download download);
        void Delete(int userId, int gameId);
        IEnumerable<Download> GetAll();
        IEnumerable<Download> GetByGameId(int gameId);
        IEnumerable<Download> GetByUserId(int userId);
        Download GetById(int userId, int gameId);
        void Save();
    }
}
