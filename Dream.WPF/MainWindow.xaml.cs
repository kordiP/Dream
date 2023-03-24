using Castle.DynamicProxy;
using Dream.Controllers;
using Dream.Controllers.DeveloperControllers;
using Dream.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        private GameController gameController;
        private GenreController genreController;
        public MainWindow()
        {
            context = new DreamContext();
            gameController = new GameController(context);
            genreController = new GenreController(context);

            InitializeComponent();
            LoadData();
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

            MostLikedGame_Label.Content = gameController.GetMostLikedGame().Name;
            MostPopularGame_Label.Content = gameController.GetMostDownloadedGame().Name;
            MostPopularGenre_Label.Content = genreController.GetMostPopularGenre().Name;

        }

    }
}
