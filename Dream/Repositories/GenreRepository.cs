using Dream.Data.Models;

namespace Dream.Repositories
{
    public class GenreRepository
    {
        private DreamContext context;
        public GenreRepository()
        {
            this.context = new DreamContext();
        }
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
