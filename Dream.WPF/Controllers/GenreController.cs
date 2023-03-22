using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views;
using System.Linq;

namespace Dream.Controllers
{
    public class GenreController
    {
        private AddingGenreView view;
        private GenreRepository genreRepository;

        private DreamContext context;
        public GenreController(DreamContext context)
        {
            this.context = context;
            this.genreRepository = new GenreRepository(context);
        }

        public Genre AddGenre(string name)
        {
            view = new AddingGenreView(name);

            Genre genre = new Genre()
            {
                Name = view.Name,
                AgeRequirements = view.AgeRequirements < 0 ? 0 : view.AgeRequirements
            };
            genreRepository.Add(genre);
            genreRepository.Save();

            return genre;
        }

        public Genre GetGenreByName(string genreName)
        {
            Genre genre = genreRepository.GetAll().FirstOrDefault(x => x.Name == genreName);
            if (genre is null)
            {
                return AddGenre(genreName);
            }
            else
            {
                return genre;
            }
        }

        public Genre GetMostPopularGenre()
        {
            return genreRepository.GetAll()
                .OrderByDescending(x => x.Games.Count)
                .First();
        }
    }
}
