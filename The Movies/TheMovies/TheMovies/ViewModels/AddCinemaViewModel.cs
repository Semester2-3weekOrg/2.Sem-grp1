using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TheMovies.Commands;
using TheMovies.Data.FileRepositories;
using TheMovies.Models;

namespace TheMovies.ViewModels
{

    internal class AddCinemaViewModel : ViewModelBase
    {
        private readonly CinemaFileRepository _cinemaRepo;

        public ObservableCollection<Cinema> Cinemas => _cinemaRepo.Items;

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

        private ObservableCollection<CinemaHall> _cinemaHalls = new();
        public ObservableCollection<CinemaHall> CinemaHalls
        {
            get => _cinemaHalls;
            set { _cinemaHalls = value; OnPropertyChanged(); }
        }

        private CinemaHall _selectedCinemaHall;
        public CinemaHall SelectedCinemaHall
        {
            get => _selectedCinemaHall;
            set { _selectedCinemaHall = value; OnPropertyChanged(); }
        }

        public ICommand AddCinemaCommand { get; }
        public ICommand SaveAllCommand { get; }
        public ICommand AddHallCommand { get; }
        public ICommand RemoveHallCommand { get; }

        public AddCinemaViewModel()
        {
            _cinemaRepo = new CinemaFileRepository();
            AddCinemaCommand = new RelayCommand(_ => AddCinema());
            SaveAllCommand = new RelayCommand(_ => _cinemaRepo.SaveAll());
            AddHallCommand = new RelayCommand(_ => AddHall(), _ => !string.IsNullOrWhiteSpace(CinemaInitials));
            RemoveHallCommand = new RelayCommand(_ => RemoveHall(), _ => SelectedCinemaHall != null);
        }

        private void AddCinema()
        {
            if (!string.IsNullOrWhiteSpace(CinemaName) && !string.IsNullOrWhiteSpace(CinemaInitials))
            {
                var newCinema = new Cinema
                {
                    Id = _cinemaRepo.Items.Count + 1,
                    CinemaName = CinemaName,
                    CinemaInitials = CinemaInitials,
                    Halls = CinemaHalls?.ToList() ?? new List<CinemaHall>()
                };

                _cinemaRepo.Add(newCinema);
                StatusMessage = $"{newCinema.CinemaName}({newCinema.CinemaInitials}) successfully added!";
                MessageBox.Show(StatusMessage);
                CinemaName = string.Empty;
                CinemaInitials = string.Empty;
                CinemaHalls = new ObservableCollection<CinemaHall>();
            }
            else
            {
                StatusMessage = "Please fill out all fields!";
                MessageBox.Show(StatusMessage);
            }
        }

        private void AddHall()
        {
            CinemaHalls.Add(new CinemaHall
            {
                HallNumber = $"Hall {CinemaHalls.Count + 1}",
                CleaningTime = 15,
                CinemaInitials = CinemaInitials
            });
        }

        private void RemoveHall()
        {
            if (SelectedCinemaHall != null)
                CinemaHalls.Remove(SelectedCinemaHall);
        }

    }
}
