using Dream.Controllers;
using Dream.Data.Models;
using Dream.Repositories;
using Dream.Views;

namespace Dream
{
    public class StartUp
    {
        static void Main()
        {
            DreamContext context = new DreamContext();
            context.Database.EnsureCreated();

            UserRepository userRepository = new UserRepository(context);

            UserController userController = new UserController(userRepository);
            IndexController indexController = new IndexController(userController);

        }
    }
}