using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TheMovies.Commands;
using TheMovies.Data.FileRepositories;
using TheMovies.Models;

namespace TheMovies.ViewModels
{
    internal class AddMovieShowViewModel : INotifyPropertyChanged
    {
        #region REPOSITORY
        private readonly MovieShowFileRepository _movieShowRepo;
        private readonly MovieFileRepository _movieRepo;
        private readonly CinemaFileRepository _cinemaRepo;
        private readonly CinemaHallFileRepository _cinemaHallRepo;

        #endregion


        #region Lister til visning
        public ObservableCollection<Movie> Movies => _movieRepo.Items;
        public ObservableCollection<Cinema> Cinemas => _cinemaRepo.Items;
        public ObservableCollection<CinemaHall> SelectedCinemaHalls
            => new ObservableCollection<CinemaHall>(SelectedCinema?.Halls ?? new List<CinemaHall>());

        #endregion

        public AddMovieShowViewModel()
        {
            _movieRepo = new MovieFileRepository();
            _cinemaRepo = new CinemaFileRepository();

            //AddMovieShowCommand = new RelayCommand(_ => AddMovieShow());
            SaveAllCommand = new RelayCommand(_ => _movieShowRepo.SaveAll());
        }

        #region Input
        private Cinema _selectedCinema;
        public Cinema SelectedCinema
        {
            get => _selectedCinema;
            set
            {
                if (_selectedCinema != value)
                {
                    _selectedCinema = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(SelectedCinemaHalls));
                    SelectedHall = null;
                }
            }
        }

        private CinemaHall _selectedHall;
        public CinemaHall SelectedHall
        {
            get => _selectedHall;
            set
            {
                if (_selectedHall != value)
                {
                    _selectedHall = value;
                    OnPropertyChanged();
                }
            }
        }

        private Movie _selectedMovie;
        public Movie SelectedMovie
        {
            get => _selectedMovie;
            set
            {
                if (value != _selectedMovie)
                {
                    _selectedMovie = value;
                    OnPropertyChanged();
                }
            }
        }

        private TimeOnly _playTime;
        public TimeOnly PlayTime
        {
            get => _playTime;
            set { _playTime = value; OnPropertyChanged(); }
        }

        private DateOnly _playDate;
        public DateOnly PlayDate
        {
            get => _playDate;
            set { _playDate = value; OnPropertyChanged(); }
        }

        private string _statusMessage = string.Empty;
        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }



        #endregion

        #region Commands
        public ICommand AddMovieShowCommand { get; }
        #endregion

        public ICommand SaveAllCommand { get; }
        public void AddMovieShow()
        {
            if (SelectedCinema != null && SelectedCinemaHalls != null && SelectedMovie != null && !string.IsNullOrWhiteSpace(PlayTime.ToString()) && !string.IsNullOrWhiteSpace(PlayDate.ToString()))
            {
                var commercialMinutes = 15;
                var movieLength = SelectedMovie.Length ?? 0;
                var duration = TimeSpan.FromMinutes(movieLength + commercialMinutes);

                var newMovieShow = new MovieShow()
                {
                    Id = _movieShowRepo.Items.Count + 1,
                    MovieTitle = SelectedMovie.Title,
                    PlayTime = PlayTime,
                    PlayDate = PlayDate,
                    Cinema = SelectedCinema,
                    CinemaHall = SelectedHall,
                    Commercial = TimeSpan.FromMinutes(commercialMinutes),
                    Duration = duration,
                };
                _movieShowRepo.Add(newMovieShow);
                StatusMessage = "Successfully added!";
                MessageBox.Show($"{StatusMessage}");
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged; // Nullable to avoid CS8618
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
