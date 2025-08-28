using System.Windows.Controls;
using TheMovies.ViewModels;

namespace TheMovies.View
{
    /// <summary>
    /// Interaction logic for AddBookingView.xaml
    /// </summary>
    public partial class AddBookingView : Page
    {
        public AddBookingView()
        {
            InitializeComponent();
            DataContext = new AddBookingViewModel();
        }

    }
}
