using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dream.Views
{
    public class UserSigningView
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public UserSigningView()
        {
            GetValues();
        }
        private void GetValues()
        {
            Console.Write("Username: ");
            this.Username = Console.ReadLine();
            Console.Write("Email: ");
            this.Email = Console.ReadLine();
            Console.Write("First name: ");
            this.FirstName = Console.ReadLine();
            Console.Write("Last name: ");
            this.LastName = Console.ReadLine();
            Console.Write("Age: ");
            this.Age = int.Parse(Console.ReadLine());
        }
    }
}
