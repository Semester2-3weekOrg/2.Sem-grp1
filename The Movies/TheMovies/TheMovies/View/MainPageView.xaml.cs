using System.Windows;
using System.Windows.Controls;
using TheMovies.ViewModels;

namespace TheMovies.View
{
    public partial class MainPageView : Page
    {

        private Frame _frame;
        private MainViewModel _mainViewModel;

        public MainPageView(Frame frame)
        {
            InitializeComponent();
            _frame = frame;
            _mainViewModel = new MainViewModel();
            DataContext = _mainViewModel;
        }

        private void addMovie_Click(object sender, RoutedEventArgs e)
        {

        }

        private void removeMovie_Click(object sender, RoutedEventArgs e)
        {

        }

        private void placeholder_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
