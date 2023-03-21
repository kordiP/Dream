using Dream.Data.Models;
using Dream.Repositories.IRepositories;

namespace Dream.Repositories
{
    public class GenreRepository : IRepository<Genre>
    {
        private DreamContext context;
        public GenreRepository(DreamContext context)
        {
            this.context = context;
        }
        public void Add(Genre genre)
        {
            context.Genres.Add(genre);
        }

        public void Delete(Genre genre)
        {
            context.Genres.Remove(genre);
            Save();
        }

        public bool Exists(int id)
        {
            return context.Genres.Any(x => x.GenreId == id);
        }

        public Genre Get(int id)
        {
            return context.Genres.FirstOrDefault(x => x.GenreId == id);
        }

        public List<Genre> GetAll()
        {
            return context.Genres.ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(Genre model)
        {
            context.Update(model).CurrentValues.SetValues(model);
            Save();
        }
    }
}
