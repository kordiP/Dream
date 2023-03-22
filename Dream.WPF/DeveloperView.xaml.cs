using Dream.Controllers;
using Dream.Controllers.DeveloperControllers;
using Dream.Data.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Windows;

namespace Dream.WPF
{
    /// <summary>
    /// Interaction logic for DeveloperView.xaml
    /// </summary>
    public partial class DeveloperView : Window
    {
        Developer loggedDeveloper;
        DeveloperController devController;
        GameController gameController;
        public DeveloperView()
        {
            InitializeComponent();
        }
        public DeveloperView(Developer developer)
        {
            InitializeComponent();

            this.loggedDeveloper = developer;
            LoadDeveloperData();
        }

        private void LoadDeveloperData()
        {
            Name_Lbl.Content = $"Welcome, {loggedDeveloper.FirstName}!";
            oldEmail_Label.Content += loggedDeveloper.Email;
            oldFirstName_Label.Content += loggedDeveloper.FirstName;
            oldLastName_Label.Content += loggedDeveloper.LastName;
        }
        private void Close_Btn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void DeleteProfile_Btn_Click(object sender, RoutedEventArgs e)
        {
        }

        private void LogOut_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow main = new MainWindow();
            main.Show();
        }

        private void CreateGame_Btn_Click(object sender, RoutedEventArgs e)
        {
        }

        private void UpdateProfile_Btn_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
