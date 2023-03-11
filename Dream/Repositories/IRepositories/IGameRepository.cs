using Dream.Data.Models;

namespace Dream.Repositories.IRepositories
{
    public interface IGameRepository
    {
        void Add(Game game);
        void Delete(int id);
        Game GetById(int id);
        IEnumerable<Game> GetAll();
        void Save();
    }
}
