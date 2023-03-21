using Dream.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T model);
        void Delete(T model);
        void Update(T model);
        List<T> GetAll();
        T Get(int id);
        bool Exists(int id);
        void Save();

    }
}
