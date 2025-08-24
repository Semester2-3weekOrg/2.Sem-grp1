using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TheMovies.Commands;
using TheMovies.Models;

namespace TheMovies.ViewModels
{
    internal class CinemaViewModel
    {
        public required string CinemaName { get; set; }
        public required string CinemaInitials { get; set; }
        public List<CinemaHall> Halls { get; set; } = new();

    }

    internal class AddCinemaViewModel : INotifyPropertyChanged
    {
        private readonly CinemaFileRepository _cinemaRepo;
        #region Lister til visning
        public ObservableCollection<CinemaViewModel> Cinemas => _cinemaRepo.Items;

        #endregion

        #region Inputfelter
        private string _cinemaName = string.Empty;
        public string CinemaName

        {
            get => _cinemaName;
            set { _cinemaName = value; OnPropertyChanged(); }
        }

        private string _cinemaInitials = string.Empty;
        public string CinemaInitials
        {
            get => _cinemaInitials;
            set { _cinemaInitials = value; OnPropertyChanged(); }

        }

        private string _statusMessage = string.Empty;
        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }

        }
        #endregion

        #region Commands
        public ICommand AddCinemaCommand { get; }
        public ICommand SaveAllCommand { get; }
        #endregion

        public AddCinemaViewModel()

        {
            _cinemaRepo = new CinemaFileRepository();

            AddCinemaCommand = new RelayCommand(_ => AddCinema());
            SaveAllCommand = new RelayCommand(_ => _cinemaRepo.SaveAll());
        }

        private void AddCinema()
        {
            if (string.IsNullOrWhiteSpace(CinemaName) || string.IsNullOrWhiteSpace(CinemaInitials))
            {
                StatusMessage = "Udfyld både navn og initialer.";
                return;
            }

            var newCinema = new CinemaViewModel
            {
                Id = _movieRepo.Items.Count + 1,
                CinemaName = CinemaName,
                CinemaInitials = CinemaInitials
            };

            Cinemas.Add(newCinema);
            StatusMessage = $"Biograf '{CinemaName}' tilføjet.";

            //Nulstil felter
            CinemaName = string.Empty;
            CinemaInitials = string.Empty;

        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string PropertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        #endregion


        public class CinemaFileRepository
        {
            public void SaveAll()
            {
                //TO-DO: gem biografer til fil 

            }

        }
    }
}