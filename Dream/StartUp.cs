using Dream.Controllers;
using Dream.Data.Models;

namespace Dream
{
    public class StartUp
    {
        static void Main()
        {
            /* --- Creating the context instance ---*/
            DreamContext context = new DreamContext();
            context.Database.EnsureCreated();

            /* --- Starting the app with the console interface --- */
            IndexController indexController = new IndexController(context);
        }
    }
}