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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dream.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DreamContext context;

        public MainWindow()
        {
            InitializeComponent();
            context = new DreamContext();
        }

        private void LogIn_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LogIn log = new LogIn();
            log.Show();
        }

        private void Close_Btn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void SignUp_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            SignUp sign = new SignUp();
            sign.Show();
        }
    }
}
