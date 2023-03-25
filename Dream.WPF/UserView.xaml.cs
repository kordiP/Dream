using Dream.Controllers;
using Dream.Data.Models;
using Dream.WPF.Controllers;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dream.WPF
{
    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : Window
    {
        public decimal DepositAmount { get; set; }
        public int GameNumber { get; set; }

        public string UserUsername { get; set; }
        public string UserEmail { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public int UserAge { get; set; }


        private AccountController accountController;
        private DreamContext context;
        private User loggedUser;
        private GameController gameController;
        private LikeController likeController;
        private DownloadController downloadController;
        private GenreController genreController;
        private UserDepositController userDepositController;
        public UserView()
        {
            InitializeComponent();
        }
        public UserView(User user)
        {
            InitializeComponent();

            context = new DreamContext();
            accountController = new AccountController(context, this);
            gameController = new GameController(context);
            likeController = new LikeController(context, this);
            downloadController = new DownloadController(context, this);
            genreController = new GenreController(context);
            userDepositController = new UserDepositController(context, this);

            this.loggedUser = user;
            LoadData();
        }

        /* Button methods */
        private void LogOut_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
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

            accountController.DeleteUser(loggedUser);
            LoadData();
        }
        private void UpdateProfile_Btn_Click(object sender, RoutedEventArgs e)
        {
            ReadNewCredentials();
            accountController.UpdateUser(loggedUser);
            LoadData();

            newEmail_Textbox.Clear();
            newFirstName_Textbox.Clear();
            newLastName_Textbox.Clear();
            newAge_Textbox.Clear();
            newUsername_Textbox.Clear();

        }

        private void DepositMoney_Btn_Click(object sender, RoutedEventArgs e)
        {
            DepositAmount = decimal.Parse(DepositMoney_Textbox.Text);
            userDepositController.Deposit(loggedUser);
        }

        private void Like_Btn_Click(object sender, RoutedEventArgs e)
        {
            GameNumber = AllGamesDataGrid.SelectedIndex;
            likeController.AddLike(loggedUser);
            LoadData();
        }
        private void Download_Btn_Click(object sender, RoutedEventArgs e)
        {
            GameNumber = AllGamesDataGrid.SelectedIndex;
            downloadController.AddDownload(loggedUser);
            LoadData();
        }

       
        
        /* Methods for loading text in labels/datagrids */
        private void LoadUserData()
        {
            Username_Lbl.Content = $"Welcome, {loggedUser.Username}!";

            if (loggedUser.Balance == null) Balance_Lbl.Content = "0.00$";
            else Balance_Lbl.Content = $"{loggedUser.Balance}$";

            oldUsername_Label.Content = $"Current username: {loggedUser.Username}";
            oldEmail_Label.Content = $"Current email: {loggedUser.Email}";
            oldFirstName_Label.Content = $"Current first name: {loggedUser.FirstName}";
            oldLastName_Label.Content = $"Current last name: {loggedUser.LastName}";
            oldAge_Label.Content = $"Current age: {loggedUser.Age}";

            DownloadedGames_Label.Content = downloadController.GetUserDownloadsCount(loggedUser.UserId);
            LikedGames_Label.Content = likeController.GetUserLikesCount(loggedUser.UserId);
        }
        private void LoadData()
        {
            LoadUserData();
            LoadUserGrids();
            /* create a data table with columns */
            DataTable table = new DataTable();

            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Price", typeof(string));
            table.Columns.Add("Required Memory", typeof(string));
            table.Columns.Add("Likes", typeof(string));
            table.Columns.Add("Downloads", typeof(string));
            table.Columns.Add("Genre", typeof(string));
            table.Columns.Add("Description", typeof(string));

            /* add all games */
            foreach (var item in gameController.BrowseGames())
            {
                table.Rows.Add(item.Split("░"));
            }

            /* design of the datagrid */
            AllGamesDataGrid.HeadersVisibility = DataGridHeadersVisibility.Column;
            AllGamesDataGrid.ColumnHeaderHeight = 70;
            AllGamesDataGrid.RowHeight = 40;
            AllGamesDataGrid.ColumnWidth = 181;
            AllGamesDataGrid.DataContext = table;

            MostLikedGame_Label.Content = gameController.GetMostLikedGame().Name;
            MostPopularGame_Label.Content = gameController.GetMostDownloadedGame().Name;
            MostPopularGenre_Label.Content = genreController.GetMostPopularGenre().Name;

        }
        private void LoadUserGrids()
        { 
            /* Same logic as LoadData() */
            DataTable tableDownloads = new DataTable();

            tableDownloads.Columns.Add("Name", typeof(string));
            tableDownloads.Columns.Add("Price", typeof(string));
            tableDownloads.Columns.Add("Required Memory", typeof(string));
            tableDownloads.Columns.Add("Genre", typeof(string));

            foreach (var item in gameController.BrowseDownloadedGames(loggedUser))
            {
                tableDownloads.Rows.Add(item.Split("░"));
            }

            DownloadedGamesGrid.HeadersVisibility = DataGridHeadersVisibility.Column;
            DownloadedGamesGrid.ColumnHeaderHeight = 70;
            DownloadedGamesGrid.RowHeight = 40;
            DownloadedGamesGrid.ColumnWidth = 181;
            DownloadedGamesGrid.DataContext = tableDownloads;


            DataTable tableLikes = new DataTable();

            tableLikes.Columns.Add("Name", typeof(string));
            tableLikes.Columns.Add("Price", typeof(string));
            tableLikes.Columns.Add("Required Memory", typeof(string));
            tableLikes.Columns.Add("Genre", typeof(string));

            foreach (var item in gameController.BrowseLikedGames(loggedUser))
            {
                tableLikes.Rows.Add(item.Split("░"));
            }

            LikedGamesGrid.HeadersVisibility = DataGridHeadersVisibility.Column;
            LikedGamesGrid.ColumnHeaderHeight = 70;
            LikedGamesGrid.RowHeight = 40;
            LikedGamesGrid.ColumnWidth = 181;
            LikedGamesGrid.DataContext = tableLikes;
        }

        private Brush SetBrushColor(string color)
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString(color);

            return brush;
        }

        private void AllGamesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /* Check if a row (game) is selected by user */
            if (AllGamesDataGrid.SelectedIndex != -1)
            {
                Download_Btn.Visibility = Visibility.Visible;
                Like_Btn.Visibility = Visibility.Visible;
            }
            else
            {
                Download_Btn.Visibility = Visibility.Hidden;
                Like_Btn.Visibility = Visibility.Hidden;
            }
        }

        private void ReadNewCredentials()
        {
            UserUsername = newUsername_Textbox.Text;
            UserEmail = newEmail_Textbox.Text;
            UserFirstName = newFirstName_Textbox.Text;
            UserLastName = newLastName_Textbox.Text;
            UserAge = int.Parse(newAge_Textbox.Text);
        }

        public void InvalidDeposit()
        {
            InvalidDeposit_Label.Foreground = SetBrushColor("#FF961010");
            InvalidDeposit_Label.Content = "Invalid deposit amount.";
        }
        public void SuccessfulDeposit(decimal balance)
        {
            InvalidDeposit_Label.Foreground = SetBrushColor("#FF34AB14");
            InvalidDeposit_Label.Content = $"Succesfully added {DepositMoney_Textbox.Text}$.";

            Balance_Lbl.Content = $"{balance}$";
            DepositMoney_Textbox.Text = string.Empty;
        }

        public void LikedGame(string gameName)
        {
            Message_Label.Foreground = SetBrushColor("#FF34AB14");
            Message_Label.Content = $"You have successfully liked {gameName}";
        }
        public void DislikedGame(string gameName)
        {
            Message_Label.Foreground = SetBrushColor("#FF34AB14");
            Message_Label.Content = $"You have successfully disliked {gameName}";
        }

        public void InvalidGame()
        {
            Message_Label.Foreground = SetBrushColor("#FF961010");
            Message_Label.Content = "You have used an invalid number!";
        }
        public void InvalidAge()
        {
            Message_Label.Foreground = SetBrushColor("#FF961010");
            Message_Label.Content = $"You are too young to play this game";
        }
        public void InvalidBalance()
        {
            Message_Label.Foreground = SetBrushColor("#FF961010");
            Message_Label.Content = $"You don't have enough money to play this game";
        }

        public void DownloadedGame(string name)
        {
            Message_Label.Foreground = SetBrushColor("#FF34AB14");
            Message_Label.Content = $"You have successfully downloaded {name}";
        }
        public void RemovedGame(string name)
        {
            Message_Label.Foreground = SetBrushColor("#FF34AB14");
            Message_Label.Content = $"You have successfully removed {name} from library";
        }

        public void SuccessfulUpdate()
        {
            Message_Label_Profile.Foreground = SetBrushColor("#FF34AB14");
            Message_Label_Profile.Content = $"You have successfully updated you profile.";
        }
        public void InvalidUsername()
        {
            Message_Label_Profile.Foreground = SetBrushColor("#FF961010");
            Message_Label_Profile.Content = $"Username is invalid or already exists.";
        }
        public void InvalidEmail()
        {
            Message_Label_Profile.Foreground = SetBrushColor("#FF961010");
            Message_Label_Profile.Content = $"Email is invalid or already exists.";
        }
        public void InvalidName()
        {
            Message_Label_Profile.Foreground = SetBrushColor("#FF961010");
            Message_Label_Profile.Content = $"First/Last name is invalid or already exists.";
        }
    }
}
