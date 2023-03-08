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

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
