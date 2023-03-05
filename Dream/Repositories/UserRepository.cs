using Dream.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Repositories
{
    public class UserRepository
    {
        private DreamContext context;
        public UserRepository(DreamContext context)
        { this.context = context; }
        public void Add(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
        public User GetById(int id)
        {
            return context.Users.FirstOrDefault(x => x.UserId == id);
        }
    }
}
