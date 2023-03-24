using Dream.Controllers;
using Dream.Controllers.DeveloperControllers;
using Dream.Data.Models;
using Dream.WPF.Controllers;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        private LikeController likeController;
        private DownloadController downloadController;
        public string Name { get; set; }
        public string GenreName { get; set; }
        public int AgeRequirements { get; set; }
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
            likeController = new LikeController(context);
            downloadController = new DownloadController(context);

            this.loggedDeveloper = developer;
            LoadData();
        }
        private void ReadGameData()
        {
            Name = GameName_Textbox.Text;
            GenreName = GameGenre_Textbox.Text;
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
            GamesCreated_Label.Content = gameController.GetDeveloperGameCount(loggedDeveloper.DeveloperId);
            TotalDownloads_Label.Content = downloadController.GetDeveloperDownloadsCount(loggedDeveloper.DeveloperId);
            TotalLikes_Label.Content = likeController.GetDeveloperLikesCount(loggedDeveloper.DeveloperId);
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
        public void ShowGenreInput()
        {
            WrongCredentials_Label.Foreground = SetBrushColor("#FFE29F0B");
            WrongCredentials_Label.Content = "Genre wasn't found. Enter age requirement to create it.";
            ARL.Visibility = Visibility.Visible;
            GenreAgeRequirement_Textbox.Visibility = Visibility.Visible;
        }
        public void SuccesfullyCreatedGame()
        {
            WrongCredentials_Label.Foreground = SetBrushColor("#FF34AB14");
            WrongCredentials_Label.Content = $"Succesfully created {Name}.";

            GameName_Textbox.Text = string.Empty;
            GamePrice_Textbox.Text = string.Empty;
            GameGenre_Textbox.Text = string.Empty;
            GameSize_Textbox.Text = string.Empty;
            GameDescription_Textbox.Text = string.Empty;
            GameCoDevs_Textbox.Text = string.Empty;
            ARL.Visibility = Visibility.Hidden;
            GenreAgeRequirement_Textbox.Text = string.Empty;
            GenreAgeRequirement_Textbox.Visibility = Visibility.Hidden;

            LoadData();
        }
        private Brush SetBrushColor(string color)
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString(color);

            return brush;
        }
        private void UpdateProfile_Btn_Click(object sender, RoutedEventArgs e)
        {

        }

        internal void InvalidGameName()
        {
            WrongCredentials_Label.Foreground = SetBrushColor("#FF961010");
            WrongCredentials_Label.Content = "This game name is invalid or already exists.";
        }

        internal void InvalidGenreName()
        {
            WrongCredentials_Label.Foreground = SetBrushColor("#FF961010");
            WrongCredentials_Label.Content = "This genre name is invalid.";
        }
        private void LoadData()
        {
            /* make all labels display correct names */
            LoadDeveloperData();

            /* create a datatable with columns */
            DataTable table = new DataTable();

            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Price", typeof(string));
            table.Columns.Add("Creators", typeof(string));
            table.Columns.Add("Likes", typeof(string));
            table.Columns.Add("Downloads", typeof(string));
            table.Columns.Add("Genre", typeof(string));
            table.Columns.Add("Description", typeof(string));

            /* add all games */
            foreach (var item in accountController.BrowseGamesAsDeveloper(loggedDeveloper))
            {
                table.Rows.Add(item.Split("░"));
            }

            /* design of the datagrid */
            AllGamesDataGrid.HeadersVisibility = DataGridHeadersVisibility.Column;
            AllGamesDataGrid.ColumnHeaderHeight = 70;
            AllGamesDataGrid.RowHeight = 40;
            AllGamesDataGrid.ColumnWidth = 181;

            /* use datatable as a context for datagrid */
            AllGamesDataGrid.DataContext = table;

        }
    }
}
