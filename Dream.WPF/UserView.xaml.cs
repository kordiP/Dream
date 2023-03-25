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
            accountController = new AccountController(context);
            gameController = new GameController(context);
            likeController = new LikeController(context, this);
            downloadController = new DownloadController(context, this);
            genreController = new GenreController(context);
            userDepositController = new UserDepositController(context, this);

            this.loggedUser = user;
            LoadData();
        }

        private void Close_Btn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void LoadUserData()
        {
            Username_Lbl.Content = $"Welcome, {loggedUser.Username}!";

            if (loggedUser.Balance == null) Balance_Lbl.Content = "0.00$";
            else Balance_Lbl.Content = $"{loggedUser.Balance}$";

            oldUsername_Label.Content += loggedUser.Username;
            oldEmail_Label.Content += loggedUser.Email;
            oldFirstName_Label.Content += loggedUser.FirstName;
            oldLastName_Label.Content += loggedUser.LastName;
            oldAge_Label.Content += loggedUser.Age.ToString();

            DownloadedGames_Label.Content = downloadController.GetUserDownloadsCount(loggedUser.UserId);
            LikedGames_Label.Content = likeController.GetUserLikesCount(loggedUser.UserId);
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

        }

        private void LogOut_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow main = new MainWindow();
            main.Show();
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

        private void DepositMoney_Btn_Click(object sender, RoutedEventArgs e)
        {
            DepositAmount = decimal.Parse(DepositMoney_Textbox.Text);
            userDepositController.Deposit(loggedUser);
        }

        internal void InvalidDeposit()
        {
            InvalidDeposit_Label.Foreground = SetBrushColor("#FF961010");
            InvalidDeposit_Label.Content = "Invalid deposit amount.";
        }

        internal void SuccessfulDeposit(decimal balance)
        {
            InvalidDeposit_Label.Foreground = SetBrushColor("#FF34AB14");
            InvalidDeposit_Label.Content = $"Succesfully added {DepositMoney_Textbox.Text}$.";

            Balance_Lbl.Content = $"{balance}$";
            DepositMoney_Textbox.Text = string.Empty;
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

        private void AllGamesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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

        private void AllGamesDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGridTextColumn dataGridTextColumn = e.Column as DataGridTextColumn;
            if (dataGridTextColumn != null)
            {
                dataGridTextColumn.Binding.StringFormat = "{0:p}";
            }
        }
    }
}
