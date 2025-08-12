using System.Windows;
using MemoryGameByJohnny.ViewModels;

namespace MemoryGameByJohnny
{
    /// <summary>
    /// Main application window.
    /// 
    /// Responsibilities:
    /// - Initializes the WPF window and assigns a <see cref="GameViewModel"/> as the DataContext.
    /// - Hosts the XAML-defined UI (see MainWindow.xaml) which binds to the ViewModel.
    ///
    /// Notes:
    /// - Directly setting <see cref="FrameworkElement.DataContext"/> here is simple for small apps.
    ///   For larger apps, consider DI or setting the ViewModel via App resources or a ViewModelLocator.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Constructs the window and wires up the initial ViewModel.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // Bind the View to the ViewModel (MVVM).
            DataContext = new GameViewModel();
        }

        private void HighScores_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is GameViewModel vm)
            {
                vm.RefreshHighScores(); // hent Top 10 fra repo
                var win = new HighscoresWindow
                {
                    Owner = this,
                    DataContext = vm
                };
                win.ShowDialog();
            }
        }
    }
}
