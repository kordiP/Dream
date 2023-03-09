using Dream.Data.Models;

namespace Dream.Repositories.IRepositories
{
    public interface IUserRepository
    {
        void Add(User user);
        void Delete(int id);
        IEnumerable<User> GetAll();
        User GetById(int id);
        bool UserExists(int id);
        bool UserExists(string username);
        bool UserEmailExists(string email);
        void Save();
    }
}
