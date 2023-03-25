using Dream.Controllers;
using Dream.Data.Models;
using Dream.WPF.Controllers;
using System.Collections.Generic;
using System.Data;
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
        private DreamContext context;

        private Developer loggedDeveloper;

        private AccountController accountController;
        private GameController gameController;
        private LikeController likeController;
        private DownloadController downloadController;

        public string DevEmail { get; set; }
        public string DevFirstName { get; set; }
        public string DevLastName { get; set; }

        public string GameName { get; set; }
        public string GenreName { get; set; }
        public int AgeRequirements { get; set; }
        public decimal Price { get; set; }
        public double RequiredMemory { get; set; }
        public string Description { get; set; }

        public IEnumerable<string> DeveloperEmails { get; set; }

        public DeveloperView()
        {
            InitializeComponent();
        }
        public DeveloperView(Developer developer)
        {
            InitializeComponent();

            context = new DreamContext();

            accountController = new AccountController(context, this);
            gameController = new GameController(context, this);
            likeController = new LikeController(context);
            downloadController = new DownloadController(context);

            this.loggedDeveloper = developer;
            LoadData();
        }

        /* Button methods */
        private void LogOut_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow main = new MainWindow();
            main.Show();
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

            accountController.DeleteDeveloper(loggedDeveloper);
        }
        private void UpdateProfile_Btn_Click(object sender, RoutedEventArgs e)
        {
            ReadNewCredentials();
            accountController.UpdateDeveloper(loggedDeveloper);
            LoadData();
            newEmail_Textbox.Clear();
            newFirstName_Textbox.Clear();
            newLastName_Textbox.Clear();
        }

        private void CreateGame_Btn_Click(object sender, RoutedEventArgs e)
        {
            ReadGameData();
            gameController.AddGame(loggedDeveloper);

        }



        /* Methods for loading labels/grids */
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
        private void LoadDeveloperData()
        {
            Name_Lbl.Content = $"Welcome, {loggedDeveloper.FirstName}!";
            oldEmail_Label.Content = $"Current email: {loggedDeveloper.Email}";
            oldFirstName_Label.Content = $"Current first name: {loggedDeveloper.FirstName}";
            oldLastName_Label.Content = $"Current last name: {loggedDeveloper.LastName}";
            GamesCreated_Label.Content = gameController.GetDeveloperGameCount(loggedDeveloper.DeveloperId);
            TotalDownloads_Label.Content = downloadController.GetDeveloperDownloadsCount(loggedDeveloper.DeveloperId);
            TotalLikes_Label.Content = likeController.GetDeveloperLikesCount(loggedDeveloper.DeveloperId);
        }

        private void ReadGameData()
        {
            GameName = GameName_Textbox.Text;
            GenreName = GameGenre_Textbox.Text;

            if (decimal.TryParse(GamePrice_Textbox.Text, out decimal num))
            {
                if (decimal.Parse(GamePrice_Textbox.Text) > 0)
                    Price = decimal.Parse(GamePrice_Textbox.Text);
            }
            else
            {
                Price = 0;
            }

            if (double.TryParse(GameSize_Textbox.Text, out double num1))
            {
                if (double.Parse(GameSize_Textbox.Text) > 0)
                    RequiredMemory = double.Parse(GameSize_Textbox.Text);
            }
            else
            {
                RequiredMemory = 0;
            }

            Description = GameDescription_Textbox.Text;
            DeveloperEmails = GameCoDevs_Textbox.Text.Split(' ', ',', ';');
        }
        private void ReadNewCredentials()
        {
            DevEmail = newEmail_Textbox.Text;
            DevFirstName = newFirstName_Textbox.Text;
            DevLastName = newLastName_Textbox.Text;
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
            WrongCredentials_Label.Content = $"Succesfully created {GameName}.";

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

        public void InvalidEmail()
        {
            Message_Label.Foreground = SetBrushColor("#FF961010");
            Message_Label.Content = "This email is invalid or already in use. Please try another one!";
        }
        public void InvalidName()
        {
            Message_Label.Foreground = SetBrushColor("#FF961010");
            Message_Label.Content = "This first/last name is invalid or already in use. Please try another one!";
        }

        public void SuccessfulUpdate()
        {
            Message_Label.Foreground = SetBrushColor("#FF34AB14");
            Message_Label.Content = "Successfully updated profile!";
        }
    }
}
