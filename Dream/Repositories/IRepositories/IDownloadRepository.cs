using Dream.Data.Models;

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
