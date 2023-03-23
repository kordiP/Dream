using Dream.Controllers;
using Dream.Controllers.DeveloperControllers;
using Dream.Data.Models;
using Dream.WPF.Controllers.SigningControllers;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Dream.WPF
{
    /// <summary>
    /// Interaction logic for DeveloperView.xaml
    /// </summary>
    public partial class DeveloperView : Window
    {
        private AccountController accountController;
        private DreamContext context;
        private Developer loggedDeveloper;
        private GameController gameController;
        public string Name { get; set; }
        public string GenreName { get; set; }
        public decimal Price { get; set; }
        public double RequiredMemory { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> DeveloperEmails { get; internal set; }

        public DeveloperView()
        {
            InitializeComponent();
        }
        public DeveloperView(Developer developer)
        {
            InitializeComponent();

            context = new DreamContext();
            accountController = new AccountController(context);
            gameController = new GameController(context, this);

            this.loggedDeveloper = developer;
            LoadDeveloperData();
        }
        private void ReadGameData()
        {
            Name = GameName_Textbox.Text;
            GenreName = GameName_Textbox.Text;
            Price = decimal.Parse(GamePrice_Textbox.Text);
            RequiredMemory = double.Parse(GameSize_Textbox.Text);
            Description = GameDescription_Textbox.Text;
            DeveloperEmails = GameCoDevs_Textbox.Text.Split(' ', ',', ';');
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
            this.Close();
            MainWindow main = new MainWindow();
            main.Show();

            context.Entry(loggedDeveloper).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            accountController.DeleteDeveloper(loggedDeveloper);
        }

        private void LogOut_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow main = new MainWindow();
            main.Show();
        }

        private void CreateGame_Btn_Click(object sender, RoutedEventArgs e)
        {
            ReadGameData();
            gameController.AddGame(loggedDeveloper);
            
        }

        private void UpdateProfile_Btn_Click(object sender, RoutedEventArgs e)
        {
        }

        internal void InvalidGameName()
        {
            MessageBox.Show("Invalid game name. Please try with a different one.", "Invalid game name", MessageBoxButton.OK);
        }

        internal void InvalidGenreName()
        {
            MessageBox.Show("Invalid genre name. Please try with a different one.", "Invalid genre name", MessageBoxButton.OK);

        }
    }
}
