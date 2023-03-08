using Dream.Data.Models;

namespace Dream.Repositories.IRepositories
{
    public interface IGenreRepository
    {
        void Add(Genre genre);
        void Delete(int id);
        IEnumerable<Genre> GetAll();
        Genre GetById(int id);
        void Save();
    }
}
