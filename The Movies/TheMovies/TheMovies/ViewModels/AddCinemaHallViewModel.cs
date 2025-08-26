using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TheMovies.Commands;
using TheMovies.Data.FileRepositories;
using TheMovies.Models;

namespace TheMovies.ViewModels
{
    internal class AddCinemaHallViewModel : ViewModelBase
    {
        // Repositories
        private readonly CinemaHallFileRepository _hallRepo;
        private readonly CinemaFileRepository _cinemaRepo;

        private ObservableCollection<Cinema> _cinemas = new();
        public ObservableCollection<Cinema> Cinemas
        {
            get => _cinemas;
            private set { _cinemas = value; OnPropertyChanged(); }
        }

        private Cinema? _selectedCinema;
        public Cinema? SelectedCinema
        {
            get => _selectedCinema;
            set
            {
                if (_selectedCinema != value)
                {
                    _selectedCinema = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CinemaInitials));

                    System.Windows.Input.CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        private string _hallNumber = string.Empty;
        public string HallNumber
        {
            get => _hallNumber;
            set
            {
                if (_hallNumber != value)
                {
                    _hallNumber = value;
                    OnPropertyChanged();
                    System.Windows.Input.CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        private int _cleaningTime = 15;
        public int CleaningTime
        {
            get => _cleaningTime;
            set
            {
                if (_cleaningTime != value)
                {
                    _cleaningTime = value;
                    OnPropertyChanged();
                    System.Windows.Input.CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        private string _statusMessage = string.Empty;
        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        public string CinemaInitials => SelectedCinema?.CinemaInitials ?? string.Empty;

        // Commands
        public ICommand SaveHallCommand { get; }
        public ICommand CancelCommand { get; }

        public AddCinemaHallViewModel()
        {
            _hallRepo = new CinemaHallFileRepository();
            _cinemaRepo = new CinemaFileRepository();

            Cinemas = new ObservableCollection<Cinema>(_cinemaRepo.GetAll());

            SaveHallCommand = new RelayCommand(_ => SaveHall(), _ => CanSaveHall());
            CancelCommand = new RelayCommand(_ => ClearForm());
        }

        private bool CanSaveHall()
        {
            // Requirements: selected cinema, HallNumber set, CleaningTime > 0
            return SelectedCinema != null
                   && !string.IsNullOrWhiteSpace(HallNumber)
                   && CleaningTime > 0;
        }

        private void SaveHall()
        {
            // Safety guard for compiler flow analysis (predicate already enforces this)
            if (SelectedCinema is null) return;

            var newId = _hallRepo.Items.Count + 1;

            var hall = new CinemaHall
            {
                Id = newId,
                HallNumber = HallNumber,
                CinemaInitials = SelectedCinema.CinemaInitials,
                CleaningTime = CleaningTime
            };

            _hallRepo.Add(hall);
            _hallRepo.SaveAll();

            StatusMessage = $"Hall '{hall.HallNumber}' ({hall.HallId}) tilføjet til {SelectedCinema.CinemaName}.";
            MessageBox.Show(StatusMessage);

            if (SelectedCinema.Halls != null)
            {
                SelectedCinema.Halls.Add(hall);
                _cinemaRepo.Update(SelectedCinema);
                _cinemaRepo.SaveAll();
            }

            ClearForm();
        }

        private void ClearForm()
        {
            HallNumber = string.Empty;
            CleaningTime = 15;
            // Keep SelectedCinema to allow adding multiple halls to the same cinema
        }

    }
}
