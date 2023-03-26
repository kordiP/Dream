using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views;

namespace Dream.Controllers
{
                /* --- Summary --- */
    /* --- This controller is responsible for --- */
          /* --- genre CRUD operations --- */

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
            /* --- Getting values --- */
            view = new AddingGenreView(name);

            /* --- Creating the genre --- */
            Genre genre = new Genre()
            {
                Name = view.Name,
                AgeRequirements = view.AgeRequirements < 0 ? 0 : view.AgeRequirements
            };
            genreRepository.Add(genre);

            /* --- Saving the changes --- */
            genreRepository.Save();

            return genre;
        }

        public Genre GetGenreByName(string genreName)
        {
            return genreRepository.GetAll().FirstOrDefault(x => x.Name == genreName);
        }

        public Genre GetMostPopularGenre()
        {
            return genreRepository.GetAll()
                .OrderByDescending(x => x.Games.Count)
                .FirstOrDefault();
        }
    }
}
