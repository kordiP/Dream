using Dream.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Repositories.IRepositories
{
    public interface IUserRepository
    {
        void Add(User user);
        void Delete(int id);
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Save();
    }
}
