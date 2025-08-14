using System;
using System.Collections.Generic;
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
using MemoryGame.ViewModel;

namespace MemoryGame.View
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private Frame _frame;
        private GameViewModel _viewModel;

        public MainPage(Frame frame)
        {
            InitializeComponent();
            _frame = frame;
            _viewModel = new GameViewModel();
            DataContext = _viewModel;
        }
    }
}
