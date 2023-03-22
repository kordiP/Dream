using Data.Models;
using Dream.Controllers;
using Dream.Data.Models;
using Dream.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;

namespace DreamTests
{
    /* <Summary>
    * This class tests all methods from GameController
    * except a few which simply validate or/and rely on
    * repository to initiate a CRUD operation
    * <Summary/> */

    [TestFixture]
    public class GameControllerTests
    {
        private IQueryable<Game> gameData;
        private Mock<DbSet<Game>> gameMockSet;
        private Mock<DreamContext> mockContext;
        private GameRepository gameRepository;
        private GameController service;

        [SetUp]
        public void SetUp()
        {
            gameData = new List<Game>
            {
                new Game()
                { 
                    GameId = 1,
                },
                new Game()
                { 
                    GameId = 2,
                },
                new Game()
                { 
                    GameId = 3,
                },
            }.AsQueryable();

            gameMockSet = new Mock<DbSet<Game>>();
            gameMockSet.As<IQueryable<Game>>().Setup(m => m.Provider).Returns(gameData.Provider);
            gameMockSet.As<IQueryable<Game>>().Setup(m => m.Expression).Returns(gameData.Expression);
            gameMockSet.As<IQueryable<Game>>().Setup(m => m.ElementType).Returns(gameData.ElementType);
            gameMockSet.As<IQueryable<Game>>().Setup(m => m.GetEnumerator()).Returns(() => gameData.GetEnumerator());


            mockContext = new Mock<DreamContext>();
            mockContext.Setup(m => m.Games).Returns(gameMockSet.Object);

            gameRepository = new GameRepository(mockContext.Object);

            service = new GameController(mockContext.Object);

            gameData.ToList().ForEach(p => gameRepository.Add(p));
            gameRepository.Save();
        }

        [Test]
        public void GetMostLikedGame_returns_correct_game()
        {
            //Arrange
            var likeData = new List<Like>
            {
                new Like()
                {
                    UserId = 1,
                    GameId = 1
                },
                new Like()
                {
                    UserId = 1,
                    GameId = 2
                },
                new Like()
                {
                    UserId = 2,
                    GameId = 1
                },
            }.AsQueryable();

            var expectedGame = gameData.ToArray()[0];
            expectedGame.Likes = likeData.ToList();
            gameRepository.Save();

            //Assert
            Assert.AreEqual(expectedGame, service.GetMostLikedGame(),
                $"GetMostLikedGame returned {service.GetMostLikedGame().Name} instead of {expectedGame.Name}");
        }

        [Test]
        public void GetMostDownloadedGame_returns_correct_game()
        {
            //Arrange
            var downloadData = new List<Download>
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
                    GameId = 1
                },
            }.AsQueryable();

            var expectedGame = gameData.ToArray()[0];
            expectedGame.Downloads = downloadData.ToList();
            gameRepository.Save();

            //Assert
            Assert.AreEqual(expectedGame, service.GetMostDownloadedGame(),
                $"GetMostLikedGame returned {service.GetMostDownloadedGame().Name} instead of {expectedGame.Name}");
        }

        [Test]
        public void GetDeveloperGameCount_returns_correct_number()
        {
            //Arrange
            var gameDevelopersData = new List<GameDeveloper>()
            {
                new GameDeveloper()
                {
                    Game = gameData.ToArray()[0],
                    DeveloperId = 1
                },
                new GameDeveloper()
                {
                    Game = gameData.ToArray()[1],
                    DeveloperId = 1
                },
                new GameDeveloper()
                {
                    Game = gameData.ToArray()[0],
                    DeveloperId = 2
                },
            }.AsQueryable();

            var gameDevMockSet = new Mock<DbSet<GameDeveloper>>();
            gameDevMockSet.As<IQueryable<GameDeveloper>>().Setup(m => m.Provider).Returns(gameDevelopersData.Provider);
            gameDevMockSet.As<IQueryable<GameDeveloper>>().Setup(m => m.Expression).Returns(gameDevelopersData.Expression);
            gameDevMockSet.As<IQueryable<GameDeveloper>>().Setup(m => m.ElementType).Returns(gameDevelopersData.ElementType);
            gameDevMockSet.As<IQueryable<GameDeveloper>>().Setup(m => m.GetEnumerator()).Returns(() => gameDevelopersData.GetEnumerator());

            mockContext.Setup(m => m.GamesDevelopers).Returns(gameDevMockSet.Object);

            var gameDevRepository = new GameDeveloperRepository(mockContext.Object);

            gameDevelopersData.ToList().ForEach(p => gameDevRepository.Add(p));

            var devId = 1;
            gameData.ToList().ForEach(p => p.GameDevelopers = gameDevelopersData.Where(x => x.DeveloperId == devId).ToList());
            gameRepository.Save();

            var expectedCount = 2;

            //Assert
            Assert.AreEqual(2, service.GetDeveloperGameCount(devId), "GetDeveloperGameCount returns incorrect number");

        }

        [Test]
        public void GetGamesOfDeveloper_returns_correct_games()
        {
            //Arrange
            var gameDevelopersData = new List<GameDeveloper>()
            {
                new GameDeveloper()
                {
                    Game = gameData.ToArray()[0],
                    DeveloperId = 1
                },
                new GameDeveloper()
                {
                    Game = gameData.ToArray()[1],
                    DeveloperId = 1
                },
                new GameDeveloper()
                {
                    Game = gameData.ToArray()[0],
                    DeveloperId = 2
                },
            }.AsQueryable();

            var gameDevMockSet = new Mock<DbSet<GameDeveloper>>();
            gameDevMockSet.As<IQueryable<GameDeveloper>>().Setup(m => m.Provider).Returns(gameDevelopersData.Provider);
            gameDevMockSet.As<IQueryable<GameDeveloper>>().Setup(m => m.Expression).Returns(gameDevelopersData.Expression);
            gameDevMockSet.As<IQueryable<GameDeveloper>>().Setup(m => m.ElementType).Returns(gameDevelopersData.ElementType);
            gameDevMockSet.As<IQueryable<GameDeveloper>>().Setup(m => m.GetEnumerator()).Returns(() => gameDevelopersData.GetEnumerator());

            mockContext.Setup(m => m.GamesDevelopers).Returns(gameDevMockSet.Object);

            var gameDevRepository = new GameDeveloperRepository(mockContext.Object);

            gameDevelopersData.ToList().ForEach(p => gameDevRepository.Add(p));

            var devId = 1;
            gameData.ToList().ForEach(p => p.GameDevelopers = gameDevelopersData.Where(x => x.DeveloperId == devId).ToList());
            gameRepository.Save();

            List<Game> gamesOfDev = new List<Game>()
            {
                gameData.ToList()[0],
                gameData.ToList()[1],
            };

            //Assert
            Assert.AreEqual(gamesOfDev, service.GetGamesOfDeveloper(devId), "GetGamesOfDeveloper returns incorrect games");
        }

        [Test]
        public void GetBestGames_returns_correct_games()
        {
            //Arrange
            var likeData = new List<Like>
            {
                new Like()
                {
                    UserId = 1,
                    GameId = 1
                },
                new Like()
                {
                    UserId = 1,
                    GameId = 2
                },
                new Like()
                {
                    UserId = 2,
                    GameId = 1
                },
            }.AsQueryable();

            var downloadData = new List<Download>
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
                    GameId = 1
                },
            }.AsQueryable();

            gameData.ToList().ForEach(p => p.Likes = likeData.Where(x => x.GameId == p.GameId).ToList());
            gameData.ToList().ForEach(p => p.Downloads = downloadData.Where(x => x.GameId == p.GameId).ToList());
            var expectedGames = gameData.OrderByDescending(x => x.Likes.Count()).ThenByDescending(x => x.Downloads.Count());
            gameRepository.Save();

            //Assert
            Assert.AreEqual(expectedGames, service.GetBestGames(), "GetBestGames returns incorrect games");
        }
    }
}
