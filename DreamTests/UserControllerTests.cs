using Dream.Controllers.UserControllers;
using Dream.Data.Models;
using Dream.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DreamTests
{
    [TestFixture]
    public class UserControllerTests
    {
        [Test]
        public void GetUserUsername_returns_correct_username()
        {
            //Arrange
            var userData = new List<User>
            {
                new User(){
                UserId = 1,
                Username = "unique",
                Email = "unique@gmail",
                Age = 18,
                FirstName = "testName",
                LastName = "testName" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userData.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userData.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userData.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(() => userData.GetEnumerator());

            var mockContext = new Mock<DreamContext>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);

            var repository = new UserRepository(mockContext.Object);
            var service = new UserController(repository);

            //Act
            userData.ToList().ForEach(p => repository.Add(p));
            repository.Save();

            //Assert
            Assert.AreEqual("unique", service.GetUserUsername(userData.ToArray()[0].UserId), "GetUserUsername does not return correct username");

        }
    }
}
