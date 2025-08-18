using System.Windows.Controls;
using TheMovies.ViewModels;

namespace TheMovies.View
{
    /// <summary>
    /// Interaction logic for AddMovieView.xaml
    /// </summary>
    public partial class AddMovieView : Page
    {
        public AddMovieView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
