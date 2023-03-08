using Dream.Data.Models;

namespace Dream.Repositories.IRepositories
{
    public interface IDeveloperRepository
    {
        void Add(Developer user);
        void Delete(int id);
        IEnumerable<Developer> GetAll();
        Developer GetById(int id);
        void Save();
    }
}
