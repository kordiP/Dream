using Dream.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Dream.WPF
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        private User user = new User();
        public SignUp()
        {
            InitializeComponent();
        }

        private void CreateUserProfile_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ReadData();
            // log them in
            UserView userView = new UserView(user); // put user in the brackets.
            userView.Show();
        }

        private void LogIn_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LogIn logIn = new LogIn();
            logIn.Show();
        }

        private void CreateDeveloperProfile_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            DeveloperView developerView = new DeveloperView();
            developerView.Show();
        }

        private void Close_Btn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void ReadData()
        {
            string username = UsernameInput.Text;
            string email = EmailInput.Text;
            string firstName = FirstNameInput.Text;
            string lastName = LastNameInput.Text;
            int age = int.Parse(AgeInput.Text);

            /* End app will not look like that. */

            user.Username = username;
            user.Email = email;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Age = age;

        }
    }
}
