using Dream.Data.Models;
using System;
using System.Windows;

namespace Dream.WPF
{
    /// <summary>
    /// Interaction logic for DeveloperView.xaml
    /// </summary>
    public partial class DeveloperView : Window
    {
        public DeveloperView()
        {
            InitializeComponent();
        }
        public DeveloperView(Developer developer)
        {
            InitializeComponent();
            LoadDeveloperData(developer);
        }

        private void LoadDeveloperData(Developer developer)
        {
            throw new NotImplementedException();
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
            this.Hide();
            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}
