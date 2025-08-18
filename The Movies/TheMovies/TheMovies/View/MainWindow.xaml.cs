using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TheMovies
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Uri("View/MainPageView.xaml", UriKind.Relative));

        }

        private void NavigateButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button { Tag: string path } && !string.IsNullOrWhiteSpace(path))
            {
                MainFrame.Navigate(new Uri(path, UriKind.Relative));
            }
        }
        private void BrowseBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = MainFrame.CanGoBack;
            e.Handled = true;
        }

        private void BrowseBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MainFrame.CanGoBack) MainFrame.GoBack();
        }

        private void BrowseForward_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = MainFrame.CanGoForward;
            e.Handled = true;
        }

        private void BrowseForward_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MainFrame.CanGoForward) MainFrame.GoForward();
        }
    }
}