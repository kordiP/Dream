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
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : Window
    {
        public UserView()
        {
            InitializeComponent();
        }
        public UserView(User user)
        {
            InitializeComponent();
            LoadUserData(user);
        }

        private void Close_Btn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void LoadUserData(User user)
        {
            Username_Lbl.Content = $"Welcome, {user.Username}!";

            if (user.Balance == null) Balance_Lbl.Content = "0.00$";
            else Balance_Lbl.Content = $"{user.Balance}$";

            oldUsername_Label.Content += user.Username;
            oldEmail_Label.Content += user.Email;
            oldFirstName_Label.Content += user.FirstName;
            oldLastName_Label.Content += user.LastName;
            oldAge_Label.Content += user.Age.ToString();
        }
    }
}
