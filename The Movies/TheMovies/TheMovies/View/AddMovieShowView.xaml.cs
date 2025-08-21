using System.Windows.Controls;
using TheMovies.ViewModels;

namespace TheMovies.View
{
    /// <summary>
    /// Interaction logic for AddMovieShowView.xaml
    /// </summary>
    public partial class AddMovieShowView : Page
    {
        public AddMovieShowView()
        {
            InitializeComponent();
            DataContext = new AddMovieShowViewModel();
        }
    }
}
