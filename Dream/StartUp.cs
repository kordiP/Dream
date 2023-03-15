using Dream.Controllers;
using Dream.Data.Models;

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