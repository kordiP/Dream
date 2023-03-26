using Dream.Controllers;
using Dream.Data.Models;
using System.Data;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;

namespace Dream.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DreamContext context;

        private GameController gameController;
        private GenreController genreController;
        public MainWindow()
        {
            

            context = new DreamContext();
            context.Database.EnsureCreated();
            gameController = new GameController(context);
            genreController = new GenreController(context);

            InitializeComponent();
            LoadData();
        }

        /* Button methods */
        private void LogIn_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LogIn log = new LogIn();
            log.Show();
        }
        private void SignUp_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            SignUp sign = new SignUp();
            sign.Show();
        }

        private void Close_Btn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }


        /* Method for loading data into the grid */
        private void LoadData()
        {
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

            if (gameController.GetMostLikedGame() is null)
            {
                MostLikedGame_Label.Content = string.Empty;
            }
            else
            {
                MostLikedGame_Label.Content = gameController.GetMostLikedGame().Name;
            }
            if (gameController.GetMostDownloadedGame() is null)
            {
                MostPopularGame_Label.Content = string.Empty;
            }
            else
            {
                MostPopularGame_Label.Content = gameController.GetMostDownloadedGame().Name;
            }
            if (genreController.GetMostPopularGenre() is null)
            {
                MostPopularGenre_Label.Content = string.Empty;
            }
            else
            {
                MostPopularGenre_Label.Content = genreController.GetMostPopularGenre().Name;
            }

        }

    }
}
