using Dream.Data.Models;
using Dream.WPF.Controllers;
using System.Windows;

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
            accountController.LogUser();
        }

        private void DeveloperLogIn_Btn_Click(object sender, RoutedEventArgs e)
        {
            ReadDeveloperData();
            accountController.LogDeveloper();
        }
        public void LogDevIn(Developer loggedDev)
        {
            this.Close();
            DeveloperView developerView = new DeveloperView(loggedDev);
            developerView.Show();
        }
        public void LogUserIn(User loggedUser)
        {
            this.Close();
            UserView userView = new UserView(loggedUser);
            userView.Show();
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
            WrongCredentials_Label.Content = "No account with that email was found.";
        }

        public void InvalidUsername()
        {
            WrongCredentials_Label.Content = "No account with that username was found.";
        }

        private void SignUp_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            SignUp signUp = new SignUp();
            signUp.Show();
        }
    }
}
