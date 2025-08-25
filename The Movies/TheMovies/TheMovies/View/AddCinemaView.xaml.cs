using System.Windows.Controls;
using TheMovies.ViewModels;

namespace TheMovies.View
{
    /// <summary>
    /// Interaction logic for AddCinemaView.xaml
    /// </summary>
    public partial class AddCinemaView : Page
    {
        public AddCinemaView()
        {
            InitializeComponent();
            DataContext = new AddCinemaViewModel();
        }
    }
}
