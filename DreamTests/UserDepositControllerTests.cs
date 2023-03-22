using Dream.Controllers.UserControllers;
using Dream.Data.Models;
using Dream.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DreamTests
{
    /* <Summary>
    * This class tests all methods from UserDepositsController
    * except a few which simply validate or/and rely on
    * repository to initiate a CRUD operation
    * <Summary/> */

    [TestFixture]
    public class UserDepositControllerTests
    {
        private IQueryable<User> userData;
        private Mock<DbSet<User>> userMockSet;
        private Mock<DreamContext> mockContext;
        private UserRepository userRepository;
        private UserDepositController service;

        [SetUp]
        public void SetUp()
        {
            userData = new List<User>
            {
                new User(){
                UserId = 1,
                Username = "unique",
                Email = "unique@gmail",
                Age = 18,
                FirstName = "testName",
                LastName = "testName",
                Balance = 100},
            }.AsQueryable();

            userMockSet = new Mock<DbSet<User>>();
            userMockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userData.Provider);
            userMockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userData.Expression);
            userMockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userData.ElementType);
            userMockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => userData.GetEnumerator());

            mockContext = new Mock<DreamContext>();
            mockContext.Setup(m => m.Users).Returns(userMockSet.Object);

            userRepository = new UserRepository(mockContext.Object);
            service = new UserDepositController(mockContext.Object);

            userData.ToList().ForEach(p => userRepository.Add(p));
            userRepository.Save();
        }

        [Test]
        public void Purchase_updates_user_balance_correctly()
        {
            //Arrange
            decimal initialBalance = (decimal)userData.ToArray()[0].Balance;
            decimal gamePrice = 10;

            //Act
            int buyerId = service.Purchase(gamePrice, userData.ToArray()[0]);

            //Assert
            Assert.That(initialBalance - gamePrice == userRepository.Get(buyerId).Balance, "Purchasing a game does not update user balance correctly");
        }

        [Test]
        public void IsDepositValid_returns_true_when_deposit_is_valid()
        {
            //Arrange
            decimal deposit = (decimal)99.99;

            //Act
            bool result = service.IsDepositValid(deposit);

            //Assert
            Assert.That(result, "IsDepositValid returns false when deposit is valid");
        }
    }
}
