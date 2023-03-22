using Data.Models;
using Dream.Controllers;
using Dream.Data.Models;
using Dream.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DreamTests
{
    /* <Summary>
    * This class tests all methods from LikeController
    * except a few which simply validate or/and rely on
    * repository to initiate a CRUD operation
    * <Summary/> */

    [TestFixture]
    public class LikeControllerTests
    {
        private IQueryable<Like> likeData;
        private Mock<DbSet<Like>> likeMockSet;
        private Mock<DreamContext> mockContext;
        private LikeRepository likeRepository;
        private LikeController service;

        [SetUp]
        public void SetUp()
        {
            likeData = new List<Like>
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
                    GameId = 3
                },
            }.AsQueryable();

            likeMockSet = new Mock<DbSet<Like>>();
            likeMockSet.As<IQueryable<Like>>().Setup(m => m.Provider).Returns(likeData.Provider);
            likeMockSet.As<IQueryable<Like>>().Setup(m => m.Expression).Returns(likeData.Expression);
            likeMockSet.As<IQueryable<Like>>().Setup(m => m.ElementType).Returns(likeData.ElementType);
            likeMockSet.As<IQueryable<Like>>().Setup(m => m.GetEnumerator()).Returns(() => likeData.GetEnumerator());


            mockContext = new Mock<DreamContext>();
            mockContext.Setup(m => m.Likes).Returns(likeMockSet.Object);

            likeRepository = new LikeRepository(mockContext.Object);

            service = new LikeController(mockContext.Object);

            likeData.ToList().ForEach(p => likeRepository.Add(p));
            likeRepository.Save();
        }

        [Test]
        public void GetUserLikes_returns_correct_likes()
        {
            //Arrange
            int userId = 1;
            List<Like> expectedLikes = likeData.Where(x => x.UserId == userId).ToList();
            //Assert
            Assert.AreEqual(expectedLikes, service.GetUserLikes(userId),
                $"GetUserLikes returned incorrect likes");

        }

        [Test]
        public void GetUserLikesCount_returns_correct_number()
        {
            //Arrange
            int expectedCount = 2;
            int userId = 1;
            //Assert
            Assert.That(expectedCount == service.GetUserLikesCount(userId),
                $"GetUserLikesCount returned {service.GetUserLikesCount(userId)} instead of {expectedCount}");
        }

        [Test]
        public void GetDeveloperLikesCount_returns_correct_number()
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
            gameData.ToList().ForEach(p => p.Likes = likeData.Where(x => x.GameId == p.GameId).ToList());
            gameData.ToList().ForEach(p => gameRepository.Add(p));
            gameRepository.Save();

            int devId = 1;
            int expectedLikesCount = 2;
            //Assert
            Assert.That(expectedLikesCount == service.GetDeveloperLikesCount(devId),
                $"GetDeveloperLikesCount returned {service.GetDeveloperLikesCount(devId)} instead of {expectedLikesCount}");
        }
    }
}
