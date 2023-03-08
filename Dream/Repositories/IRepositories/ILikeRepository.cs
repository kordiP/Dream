using Dream.Data.Models;

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
