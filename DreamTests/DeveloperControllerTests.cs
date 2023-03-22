using Data.Models;
using Dream.Controllers.DeveloperControllers;
using Dream.Data.Models;
using Dream.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DreamTests
{
    /* <Summary>
    * This class tests all methods from DeveloperController
    * except a few which simply validate or/and rely on
    * repository to initiate a CRUD operation
    * <Summary/> */

    [TestFixture]
    public class DeveloperControllerTests
    {
        private IQueryable<Developer> developerData;
        private Mock<DbSet<Developer>> devMockSet;
        private Mock<DreamContext> mockContext;
        private DeveloperRepository devRepository;
        private DeveloperController service;

        [SetUp]
        public void SetUp()
        {
            developerData = new List<Developer>
            {
                new Developer()
                {
                    DeveloperId = 1,
                    Email = "unique@gmail",
                    FirstName = "testName",
                    LastName = "testName",
                },
                new Developer()
                {
                    DeveloperId = 2,
                    Email = "unique@gmail1",
                    FirstName = "testName1",
                    LastName = "testName1",
                }
            }.AsQueryable();

            devMockSet = new Mock<DbSet<Developer>>();
            devMockSet.As<IQueryable<Developer>>().Setup(m => m.Provider).Returns(developerData.Provider);
            devMockSet.As<IQueryable<Developer>>().Setup(m => m.Expression).Returns(developerData.Expression);
            devMockSet.As<IQueryable<Developer>>().Setup(m => m.ElementType).Returns(developerData.ElementType);
            devMockSet.As<IQueryable<Developer>>().Setup(m => m.GetEnumerator()).Returns(() => developerData.GetEnumerator());


            mockContext = new Mock<DreamContext>();
            mockContext.Setup(m => m.Developers).Returns(devMockSet.Object);

            devRepository = new DeveloperRepository(mockContext.Object);

            service = new DeveloperController(mockContext.Object);

            developerData.ToList().ForEach(p => devRepository.Add(p));
            devRepository.Save();
        }

        [Test]
        public void GetCoDevelopersOfGame_returns_correct_developers()
        {
            //Arrange
            Developer dev = developerData.ToArray()[0];
            Developer coDev1 = developerData.ToArray()[1];

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
                    Developer = dev
                },
                new GameDeveloper()
                {
                    GameId = 1,
                    Developer = coDev1
                },
            }.AsQueryable();

            gameData.ToList().ForEach(p => p.GameDevelopers = gameDevelopersData.ToList());
            dev.GameDevelopers = gameDevelopersData.ToList();
            coDev1.GameDevelopers = gameDevelopersData.ToList();

            var gameDevMockSet = new Mock<DbSet<GameDeveloper>>();
            gameDevMockSet.As<IQueryable<GameDeveloper>>().Setup(m => m.Provider).Returns(gameDevelopersData.Provider);
            gameDevMockSet.As<IQueryable<GameDeveloper>>().Setup(m => m.Expression).Returns(gameDevelopersData.Expression);
            gameDevMockSet.As<IQueryable<GameDeveloper>>().Setup(m => m.ElementType).Returns(gameDevelopersData.ElementType);
            gameDevMockSet.As<IQueryable<GameDeveloper>>().Setup(m => m.GetEnumerator()).Returns(() => gameDevelopersData.GetEnumerator());

            mockContext.Setup(m => m.GamesDevelopers).Returns(gameDevMockSet.Object);

            var gameDevRepository = new GameDeveloperRepository(mockContext.Object);

            gameDevelopersData.ToList().ForEach(p => gameDevRepository.Add(p));
            gameDevRepository.Save();

            //Assert
            Assert.That(service.GetCoDevelopersOfGame(1).Count == 2, "GetCoDeveloperOfGame returns incorrect codevelopers count");
            Assert.AreEqual(service.GetCoDevelopersOfGame(1), developerData, "GetCoDeveloperOfGame returns incorrect codevelopers");
        }

        [Test]
        public void GetDeveloper_returns_correct_dev_via_id()
        {
            //Arrange
            int DeveloperId = developerData.ToArray()[0].DeveloperId;
            //Assert
            Assert.That(service.GetDeveloper(DeveloperId) == developerData.ToArray()[0], "GetDeveloper does not return the correct developer via id");
        }

        [Test]
        public void GetDeveloper_returns_correct_dev_via_email()
        {
            //Arrange
            string email = developerData.ToArray()[0].Email;
            //Assert
            Assert.That(service.GetDeveloper(email) == developerData.ToArray()[0], "GetDeveloper does not return the correct developer via email");
        }

        [Test]
        public void IsDeveloperCreated_returns_true_when_dev_is_created_via_id()
        {
            //Arrange
            int id = developerData.ToArray()[0].DeveloperId;
            //Assert
            Assert.That(service.IsDeveloperCreated(id), "IsDeveloperCreated returns false when dev is created (via id)");
        }

        [Test]
        public void IsDeveloperCreated_returns_true_when_dev_is_created_via_email()
        {
            //Arrange
            string email = developerData.ToArray()[0].Email;
            //Assert
            Assert.That(service.IsDeveloperCreated(email), "IsDeveloperCreated returns false when dev is created (via email)");
        }

        [Test]
        public void GetDeveloperFullname_returns_the_correct_fullname()
        {
            //Arrange
            Developer dev = developerData.ToArray()[0];
            string fullname = dev.FirstName + " " + dev.LastName;
            //Assert
            Assert.That(fullname == service.GetDeveloperFullname(dev.DeveloperId), "GetDeveloperFullname does not return their fullname");
        }
    }
}
