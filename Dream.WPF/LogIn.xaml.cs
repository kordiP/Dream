using Dream.Data.Models;
using Dream.WPF.Controllers.SigningControllers;
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
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        private AccountController accountController;
        private DreamContext context;

        public string User_Username { get; set; }
        public string Dev_Email { get; set; }

        public LogIn()
        {
            InitializeComponent();
            context = new DreamContext();
            accountController = new AccountController(context, this);
        }
        private void Close_Btn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void UserLogIn_Btn_Click(object sender, RoutedEventArgs e)
        {
            ReadUserData();
            var loggedUser = accountController.LogUser();

            this.Close();
            UserView userView = new UserView(loggedUser); 
            userView.Show();
        }

        private void DeveloperLogIn_Btn_Click(object sender, RoutedEventArgs e)
        {
            ReadDeveloperData();
            var loggedDev = accountController.LogDeveloper();

            this.Close();
            DeveloperView developerView = new DeveloperView(loggedDev);
            developerView.Show();
        }
        private void ReadUserData()
        {
            User_Username = UsernameInput.Text;
        }
        private void ReadDeveloperData()
        {
            Dev_Email = EmailInput_Dev.Text;
        }
        public void InvalidEmail()
        {
            MessageBox.Show("Email not found. Please try with a different one.", "Invalid Email", MessageBoxButton.OK);
        }

        public void InvalidUsername()
        {
            MessageBox.Show("Username not found. Please try with a different one.", "Invalid Username", MessageBoxButton.OK);
        }

        private void SignUp_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            SignUp signUp = new SignUp();
            signUp.Show();

        }
    }
}
