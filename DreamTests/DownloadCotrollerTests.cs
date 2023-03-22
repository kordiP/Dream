using Data.Models;
using Dream.Controllers;
using Dream.Data.Models;
using Dream.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DreamTests
{
    /* <Summary>
    * This class tests all methods from DownloadCotroller
    * except a few which simply validate or/and rely on
    * repository to initiate a CRUD operation
    * <Summary/> */

    [TestFixture]
    public class DownloadCotrollerTests
    {
        private IQueryable<Download> downloadData;
        private Mock<DbSet<Download>> downloadMockSet;
        private Mock<DreamContext> mockContext;
        private DownloadRepository downloadRepository;
        private DownloadController service;

        [SetUp]
        public void SetUp()
        {
            downloadData = new List<Download>
            {
                new Download()
                {
                    UserId = 1,
                    GameId = 1
                },
                new Download()
                {
                    UserId = 1,
                    GameId = 2
                },
                new Download()
                {
                    UserId = 2,
                    GameId = 3
                },
            }.AsQueryable();

            downloadMockSet = new Mock<DbSet<Download>>();
            downloadMockSet.As<IQueryable<Download>>().Setup(m => m.Provider).Returns(downloadData.Provider);
            downloadMockSet.As<IQueryable<Download>>().Setup(m => m.Expression).Returns(downloadData.Expression);
            downloadMockSet.As<IQueryable<Download>>().Setup(m => m.ElementType).Returns(downloadData.ElementType);
            downloadMockSet.As<IQueryable<Download>>().Setup(m => m.GetEnumerator()).Returns(() => downloadData.GetEnumerator());


            mockContext = new Mock<DreamContext>();
            mockContext.Setup(m => m.Downloads).Returns(downloadMockSet.Object);

            downloadRepository = new DownloadRepository(mockContext.Object);

            service = new DownloadController(mockContext.Object);

            downloadData.ToList().ForEach(p => downloadRepository.Add(p));
            downloadRepository.Save();
        }

        [Test]
        public void GetUserDownloads_returns_correct_downloads()
        {
            //Arrange
            int userId = 1;
            List<Download> expectedDownloads = downloadData.Where(x => x.UserId == userId).ToList();
            //Assert
            Assert.AreEqual(expectedDownloads, service.GetUserDownloads(userId),
                $"GetUserDownloads returned incorrect downloads");

        }

        [Test]
        public void GetUserDownloadsCount_returns_correct_number()
        {
            //Arrange
            int expectedCount = 2;
            int userId = 1;
            //Assert
            Assert.That(expectedCount == service.GetUserDownloadsCount(userId),
                $"GetUserDownloadsCount returned {service.GetUserDownloadsCount(userId)} instead of {expectedCount}");
        }

        [Test]
        public void GetDeveloperDownloadsCount_returns_correct_number()
        {
            //Arrange
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

            var gameDevelopersData = new List<GameDeveloper>()
            {
                new GameDeveloper()
                {
                    GameId = 1,
                    DeveloperId = 1
                },
                new GameDeveloper()
                {
                    GameId = 2,
                    DeveloperId = 1
                },
                new GameDeveloper()
                {
                    GameId = 1,
                    DeveloperId = 2
                },
            }.AsQueryable();

            var gameSet = new Mock<DbSet<Game>>();
            gameSet.As<IQueryable<Game>>().Setup(m => m.Provider).Returns(gameData.Provider);
            gameSet.As<IQueryable<Game>>().Setup(m => m.Expression).Returns(gameData.Expression);
            gameSet.As<IQueryable<Game>>().Setup(m => m.ElementType).Returns(gameData.ElementType);
            gameSet.As<IQueryable<Game>>().Setup(m => m.GetEnumerator()).Returns(() => gameData.GetEnumerator());

            mockContext.Setup(m => m.Games).Returns(gameSet.Object);

            var gameRepository = new GameRepository(mockContext.Object);

            gameData.ToList().ForEach(p => p.GameDevelopers = gameDevelopersData.Where(x => x.GameId == p.GameId).ToList());
            gameData.ToList().ForEach(p => p.Downloads = downloadData.Where(x => x.GameId == p.GameId).ToList());
            gameData.ToList().ForEach(p => gameRepository.Add(p));
            gameRepository.Save();

            int devId = 1;
            int expectedDownloadsCount = 2;
            //Assert
            Assert.That(expectedDownloadsCount == service.GetDeveloperDownloadsCount(devId),
                $"GetDeveloperDownloadsCount returned {service.GetDeveloperDownloadsCount(devId)} instead of {expectedDownloadsCount}");
        }
    }
}
