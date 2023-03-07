using Dream.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Repositories
{
    public class GenreRepository
    {
        private DreamContext context;
        public GenreRepository(DreamContext context)
        { this.context = context; }
        public void Add(Genre genre)
        {
            context.Genres.Add(genre);
            Save();
        }

        public void Delete(int id)
        {
            Genre genre = context.Genres.FirstOrDefault(x => x.GenreId == id);
            context.Genres.Remove(genre);
            Save();
        }
        public Genre GetById(int id)
        {
            return context.Genres.FirstOrDefault(x => x.GenreId == id);
        }

        public IEnumerable<Genre> GetAll()
        {
            return context.Genres.ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
