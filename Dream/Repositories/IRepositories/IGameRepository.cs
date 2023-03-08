using Dream.Data.Models;

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
