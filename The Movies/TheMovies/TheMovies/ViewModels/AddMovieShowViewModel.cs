using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
        }

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
                }
            }
        }













        public event PropertyChangedEventHandler? PropertyChanged; // Nullable to avoid CS8618
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
