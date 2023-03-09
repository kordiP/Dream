using Dream.Data.Models;
using Dream.Repositories.IRepositories;

namespace Dream.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DreamContext context;
        public UserRepository()
        { this.context = new DreamContext(); }
        public void Add(User user)
        {
            context.Users.Add(user);
            Save();
        }

        public void Delete(int id)
        {
            User user = context.Users.FirstOrDefault(x => x.UserId == id);
            context.Users.Remove(user);
            Save();
        }

        public User GetById(int id)
        {
            return context.Users.FirstOrDefault(x => x.UserId == id);
        }

        public IEnumerable<User> GetAll()
        {
            return context.Users.ToList();
        }
        public bool UserExists(string username)
        {
            return context.Users.Any(x => x.Username == username);
        }

        public bool UserEmailExists(string email)
        {
            return context.Users.Any(x => x.Email == email);
        }

        public bool UserExists(int id)
        {
            return context.Users.Any(x => x.UserId == id);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
