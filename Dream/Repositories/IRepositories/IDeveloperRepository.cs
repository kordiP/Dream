using Dream.Data.Models;

namespace Dream.Repositories.IRepositories
{
    public interface IDeveloperRepository
    {
        void Add(Developer user);
        void Delete(int id);
        IEnumerable<Developer> GetAll();
        Developer Get(int id);
        Developer Get(string email);
        bool DeveloperExists(int id);
        bool DeveloperExists(string email);
        void Save();
    }
}
