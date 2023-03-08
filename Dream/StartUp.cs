using Dream.Controllers;
using Dream.Controllers.UserControllers;
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

            IndexController indexController = new IndexController();
        }
    }
}