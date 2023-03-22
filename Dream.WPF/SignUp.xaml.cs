using Dream.Controllers.DeveloperControllers;
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
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        private AccountController accountController;
        private DreamContext context;
        public string User_Username { get; set; }
        public string User_Email { get; set; }
        public string User_FirstName { get; set; }
        public string User_LastName { get; set; }
        public int User_Age { get; set; }

        public string Dev_Email { get; set; }
        public string Dev_FirstName { get; set; }
        public string Dev_LastName { get; set; }
        public SignUp()
        {
            InitializeComponent();
            context = new DreamContext();
            accountController = new AccountController(context, this);
        }

        private void CreateUserProfile_Btn_Click(object sender, RoutedEventArgs e)
        {
            ReadUserData();
            accountController.AddUser();

            this.Close();
            UserView userView = new UserView();
            userView.Show();
        }

        private void LogIn_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            LogIn logIn = new LogIn();
            logIn.Show();
        }

        private void CreateDeveloperProfile_Btn_Click(object sender, RoutedEventArgs e)
        {
            ReadDeveloperData();
            accountController.AddDeveloper();

            this.Close();
            DeveloperView developerView = new DeveloperView();
            developerView.Show();
        }

        private void Close_Btn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void ReadUserData()
        {
            User_Username = Username_Textbox_User.Text;
            User_Email = EmaiI_Textbox_User.Text;
            User_FirstName = FirstName_Textbox_User.Text;
            User_LastName = LastName_Textbox_User.Text;
            User_Age = int.Parse(Age_Textbox_User.Text);
        }
        private void ReadDeveloperData()
        {
            Dev_Email = Email_Textbox_Dev.Text;
            Dev_FirstName = FirstName_Textbox_Dev.Text;
            Dev_LastName = LastName_Textbox_Dev.Text;
        }

        public void InvalidEmail()
        {
            MessageBox.Show("Invalid email. Please try with a different one.", "Invalid Email", MessageBoxButton.OK);
        }

        public void InvalidUsername()
        {
            MessageBox.Show("Invalid username. Please try with a different one.", "Invalid Username", MessageBoxButton.OK);
        }

        public void InvalidName()
        {
            MessageBox.Show("Invalid name. Please try with a different one.", "Invalid Name", MessageBoxButton.OK);
        }
    }
}
