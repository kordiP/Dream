using Dream.Controllers;
using Dream.Data.Models;
using Dream.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DreamTests
{
                    /* --- Summary --- */
    /* --- This class tests all methods from GenreController --- */
    /* --- except a few which simply validate or/and rely on --- */
    /* --- repository to initiate a CRUD operation --- */

    [TestFixture]
    public class GenreControllerTests
    {
        private IQueryable<Genre> genreData;
        private Mock<DbSet<Genre>> genreMockSet;
        private Mock<DreamContext> mockContext;
        private GenreRepository genreRepository;
        private GenreController service;

        [SetUp]
        public void SetUp()
        {
            genreData = new List<Genre>
            {
                new Genre()
                {
                    GenreId = 1,
                    Name= "Test",
                },
                new Genre()
                {
                    GenreId = 2,
                    Name= "Test1",
                },
            }.AsQueryable();

            genreMockSet = new Mock<DbSet<Genre>>();
            genreMockSet.As<IQueryable<Genre>>().Setup(m => m.Provider).Returns(genreData.Provider);
            genreMockSet.As<IQueryable<Genre>>().Setup(m => m.Expression).Returns(genreData.Expression);
            genreMockSet.As<IQueryable<Genre>>().Setup(m => m.ElementType).Returns(genreData.ElementType);
            genreMockSet.As<IQueryable<Genre>>().Setup(m => m.GetEnumerator()).Returns(() => genreData.GetEnumerator());


            mockContext = new Mock<DreamContext>();
            mockContext.Setup(m => m.Genres).Returns(genreMockSet.Object);

            genreRepository = new GenreRepository(mockContext.Object);

            service = new GenreController(mockContext.Object);

            genreData.ToList().ForEach(p => genreRepository.Add(p));
            genreRepository.Save();
        }

        [Test]
        public void GetGenreByName_returns_correct_name()
        {
            //Arrange
            Genre expectedGenre = genreData.ToArray()[0];
            string expectedGenreName = genreData.ToArray()[0].Name;

            //Assert
            Assert.AreEqual(expectedGenre, service.GetGenreByName(expectedGenreName),
                $"GetGenreByName returned {service.GetGenreByName(expectedGenreName)} instead of {expectedGenreName}");
        }

        [Test]
        public void GetMostPopularGenre_returns_correct_genre()
        {
            var gameData = new List<Game>
            {
                new Game()
                {
                    GameId = 1
                },
                new Game()
                {
                    GameId = 2
                },
                new Game()
                {
                    GameId = 3
                },
            }.AsQueryable();

            var expectedGenre = genreData.ToArray()[0];

            expectedGenre.Games = gameData.ToList();
            genreRepository.Save();

            Assert.AreEqual(expectedGenre, service.GetMostPopularGenre(),
                $"GetGenreByName returned {service.GetMostPopularGenre().Name} instead of {expectedGenre.Name}");
        }
    }
}
