using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Views
{
    public class DeveloperSigningView
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DeveloperSigningView()
        {
            GetValues();
        }
        private void GetValues()
        {
            Console.Write("Email: ");
            this.Email = Console.ReadLine();
            Console.Write("First name: ");
            this.FirstName = Console.ReadLine();
            Console.Write("Last name: ");
            this.LastName = Console.ReadLine();
        }
    }
}
