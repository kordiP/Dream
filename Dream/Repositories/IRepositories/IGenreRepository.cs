using Dream.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Repositories.IRepositories
{
    public interface IGenreRepository
    {
        void Add(Genre genre);
        void Delete(int id);
        IEnumerable<Genre> GetAll();
        Genre GetById(int id);
        void Save();

    }
}
