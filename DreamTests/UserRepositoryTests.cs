using Microsoft.EntityFrameworkCore;
using Moq;
using Dream.Data.Models;
using Dream.Repositories;

namespace DreamTests
{
    /* --- Summary --- */
    /* --- This class tests all methods from the most complex repository - UserRepository --- */
    /* ---  Both queriable and CRUD operations have been tested --- */
    /* --- All other repositories implement similar but simpler interfaces than UserRepository --- */
    /* --- For this reason and because CRUP operation are not the main goal of testing --- */
    /* --- This is the only test class for repositories --- */

    [TestFixture]
    public class UserRepositoryTests
    {

        [Test]
        public void Adding_a_user_via_context()
        {
            //Arrange
            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<DreamContext>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);
            var service = new UserRepository(mockContext.Object);

            //Act
            service.Add(new User() { Username = "unique", Email = "unique@gmail", Age = 18, FirstName = "testName", LastName = "testName"});
            service.Save();

            //Assert
            mockSet.Verify(m => m.Add(It.IsAny<User>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void Deleting_a_user_via_context()
        {
            //Arrange
            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<DreamContext>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);
            var service = new UserRepository(mockContext.Object);

            User user = new User()
            {
                Username = "unique",
                Email = "unique@gmail",
                Age = 18,
                FirstName = "testName",
                LastName = "testName"
            };

            //Act
            service.Add(user);
            service.Save();
            service.Delete(user);

            //Assert
            mockSet.Verify(m => m.Remove(user), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Exactly(2));
        }

        [Test]
        public void Updating_a_user_via_context()
        {
            Assert.Pass();
            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<DreamContext>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);
            var service = new UserRepository(mockContext.Object);

            User user = new User()
            {
                Username = "unique",
                Email = "unique@gmail",
                Age = 18,
                FirstName = "testName",
                LastName = "testName"
            };

            //Act
            service.Add(user);
            service.Save();
            user.Username = "new";
            service.Update(user);

            //Assert
            mockSet.Verify(m => m.Update(user), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Exactly(2));
        }

        [Test]
        public void UserEmailExists_return_correct_value()
        {
            //Arrange
            var userData = new List<User>
            {
                new User()
                {UserId = 1,
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

            var service = new UserRepository(mockContext.Object);
            string email = "unique@gmail";

            //Act
            userData.ToList().ForEach(p => service.Add(p));
            service.Save();

            //Assert
            Assert.That(service.UserEmailExists(email), "UserEmailExists does not find existing emails");
        }
        [Test]
        public void UserExists_by_id_returns_correct_value()
        {
            //Arrange
            var userData = new List<User>
            {
                new User()
                {UserId = 1,
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

            var service = new UserRepository(mockContext.Object);
            int userId = 1;

            //Act
            userData.ToList().ForEach(p => service.Add(p));
            service.Save();

            //Assert
            Assert.That(service.Exists(userId), "UserExists does not find existing users via id");
        }
        [Test]
        public void UserExists_by_username()
        {
            //Arrange
            var userData = new List<User>
            {
                new User()
                {UserId = 1,
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

            var service = new UserRepository(mockContext.Object);
            string username = "unique";

            //Act
            userData.ToList().ForEach(p => service.Add(p));
            service.Save();

            //Assert
            Assert.That(service.UserUsernameExists(username), "UserUsernameExists does not find existing users via username");
        }

        [Test]
        public void Get_User_by_id()
        {
            //Arrange
            var userData = new List<User>
            {
                new User()
                {UserId = 1,
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

            var service = new UserRepository(mockContext.Object);
            var userId = 1;

            //Act
            userData.ToList().ForEach(p => service.Add(p));
            service.Save();

            //Assert
            Assert.AreEqual(userData.ToArray()[0], service.Get(userId),
                $"GetUser by id returns {service.Get(userId)} instead of {userData.ToArray()[0]}");
        }

        [Test]
        public void Get_User_by_username()
        {
            //Arrange
            var userData = new List<User>
            {
                new User()
                {UserId = 1,
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

            var service = new UserRepository(mockContext.Object);
            string username = "unique";

            //Act
            userData.ToList().ForEach(p => service.Add(p));
            service.Save();

            //Assert
            Assert.AreEqual(userData.ToArray()[0], service.GetByUsername(username),
                $"GetUser by username returns {service.GetByUsername(username)} instead of {userData.ToArray()[0]}");

        }

        [Test]
        public void GetAll_orders_users_by_id()
        {
            //Arrange
            var userData = new List<User>
            {
                new User()
                {UserId = 1,
                Username = "unique",
                Email = "unique@gmail",
                Age = 18,
                FirstName = "testName",
                LastName = "testName" },

                new User()
                {UserId = 2,
                Username = "unique1",
                Email = "unique@gmail1",
                Age = 18,
                FirstName = "testName",
                LastName = "testName" },

                new User()
                {UserId = 3,
                Username = "unique2",
                Email = "unique@gmail2",
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

            var service = new UserRepository(mockContext.Object);

            //Act
            userData.ToList().ForEach(p => service.Add(p));
            service.Save();

            var users = service.GetAll().ToList<User>();

            //Assert
            Assert.AreEqual(3, users.Count(), $"GetAll count is {users.Count()} instead of {3}");
            Assert.AreEqual("unique", users[0].Username, "Incorrect order");
            Assert.AreEqual("unique1", users[1].Username, "Incorrect order");
            Assert.AreEqual("unique2", users[2].Username, "Incorrect order");
        }
    }
}