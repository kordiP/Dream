using Dream.Data.Models;

namespace Dream.Repositories.IRepositories
{
    public interface IUserRepository
    {
        void Add(User user);
        void Delete(User user);
        List<User> GetAll();
        User Get(int id);
        User Get(string username);
        bool UserExists(int id);
        bool UserExists(string username);
        bool UserEmailExists(string email);
        void Save();
        void Update(User user);
    }
}
