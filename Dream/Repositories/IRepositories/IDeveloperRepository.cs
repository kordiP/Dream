using Dream.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
