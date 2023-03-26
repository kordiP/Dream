using Dream.Data.Models;
using Dream.Repositories;
using Dream.WPF;
using System.Linq;

namespace Dream.Controllers
{
    public class GenreController
    {
        private DreamContext context;

        private DeveloperView view;

        private GenreRepository genreRepository;
        public GenreController(DreamContext context)
        {
            this.context = context;
            this.genreRepository = new GenreRepository(context);
        }
        public GenreController(DreamContext context, DeveloperView view)
        {
            this.context = context;
            this.genreRepository = new GenreRepository(context);

            this.view = view;
        }

        public Genre AddGenre()
        {
            /* Adding new genre */
            Genre genre = new Genre()
            {
                Name = view.GenreName,
                AgeRequirements = view.AgeRequirements < 0 ? 0 : view.AgeRequirements
            };
            genreRepository.Add(genre);
            genreRepository.Save();

            return genre;
        }

        public Genre GetGenreByName(string genreName)
        {
            /* Search for a given genre */
            Genre genre = genreRepository.GetAll().FirstOrDefault(x => x.Name == genreName);
            if (genre is null)
            {
                return null;
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
                .FirstOrDefault();
        }
    }
}
