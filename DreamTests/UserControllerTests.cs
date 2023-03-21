using Dream.Controllers.UserControllers;
using Dream.Data.Models;
using Dream.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DreamTests
{
    /* <Summary>
    * This class tests all methods from UserController
    * except a few which simply validate or/and rely on
    * repository to initiate a CRUD operation
    * <Summary/> */

    [TestFixture]
    public class UserControllerTests
    {
        private IQueryable<User> userData;
        private Mock<DbSet<User>> mockSet;
        private Mock<DreamContext> mockContext;
        private UserRepository repository;
        private UserController service;

        [SetUp]
        public void SetUp()
        {
            //Arrange
            userData = new List<User>
            {
                new User(){
                UserId = 1,
                Username = "unique",
                Email = "unique@gmail",
                Age = 18,
                FirstName = "testName",
                LastName = "testName" },
            }.AsQueryable();

            mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userData.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userData.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userData.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => userData.GetEnumerator());

            mockContext = new Mock<DreamContext>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            repository = new UserRepository(mockContext.Object);
            service = new UserController(mockContext.Object);

            //Act
            userData.ToList().ForEach(p => repository.Add(p));
            repository.Save();
        }

        [Test]
        public void GetUserUsername_returns_correct_username()
        {
            //Arragge
            string username = userData.ToArray()[0].Username;
            //Assert
            Assert.That(username == service.GetUserUsername(1), "GetUserUsername does not return correct username");
        }

        [Test]
        public void GetUserBalance_returns_correct_balance_if_null()
        {
            //Arrange
            int UserId = userData.ToArray()[0].UserId;

            //Assert
            Assert.That(service.GetUserBalance(UserId) == 0, "GetUserBalance does not return correct balance when it is null");
        }

        [Test]
        public void GetUserBalance_returns_correct_balance_if_not_null()
        {
            //Arrange
            int UserId = userData.ToArray()[0].UserId;
            decimal expectedBalance = 10;
            userData.ToArray()[0].Balance = expectedBalance;
            //Assert
            Assert.That(service.GetUserBalance(UserId) == expectedBalance, "GetUserBalance does not return correct balance when it is not null");
        }

        [Test]
        public void GetUser_returns_correct_user_via_id()
        {
            //Arrange
            int UserId = userData.ToArray()[0].UserId;
            //Assert
            Assert.That(service.GetUser(UserId) == userData.ToArray()[0], "GetUser does not return correct user via id");
        }

        [Test]
        public void GetUser_returns_correct_user_via_username()
        {
            //Arrange
            string username = userData.ToArray()[0].Username;
            //Assert
            Assert.That(service.GetUser(username) == userData.ToArray()[0], "GetUser does not return correct user via username");
        }

        [Test]
        public void IsUserEmailCreated_returns_true_when_user_email_is_created()
        {
            //Arrange
            string email = userData.ToArray()[0].Email;
            //Assert
            Assert.That(service.IsUserEmailCreated(email) == true, "IsUserEmailCreated does not return true when user email is created");
        }

        [Test]
        public void IsUserUsernameCreated_returns_true_when_user_username_is_created()
        {
            //Arrange
            string username = userData.ToArray()[0].Username;
            //Assert
            Assert.That(service.IsUsernameCreated(username) == true, "IsUserUsernameCreated does not return true when user username is created");
        }
    }
}
