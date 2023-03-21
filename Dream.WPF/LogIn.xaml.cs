﻿using System;
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
        public LogIn()
        {
            InitializeComponent();
        }

        private void LogIn_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            SignUp signUp = new SignUp();
            signUp.Show();
        }
        private void Close_Btn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void UserLogIn_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            UserView userView = new UserView();
            userView.Show();
        }

        private void DeveloperLogIn_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            DeveloperView developerView = new DeveloperView();
            developerView.Show();
        }
        private void ReadData()
        {
            // EmailInput_Dev --> email for the dev
            // UsernameInput --> username for the user
        }

    }
}
