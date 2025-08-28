using System.Collections.ObjectModel;
using TheMovies.Models;

namespace TheMovies.ViewModels
{
    class AddBookingViewModel : ViewModelBase
    {
        private MovieShow _selectedMovieShow;

        public ObservableCollection<MovieShow> MovieShows { get; set; }
        public ObservableCollection<TimeOnly> AvailableTimes { get; set; }

        public MovieShow SelectedMovieShow
        {
            get => _selectedMovieShow;
            set
            {
                _selectedMovieShow = value;
                OnPropertyChanged();
            }
        }
        private TimeOnly _selectedTime;
        public TimeOnly SelectedTime
        {
            get => _selectedTime;
            set
            {
                if (_selectedTime != value)
                {
                    _selectedTime = value;
                    OnPropertyChanged(nameof(SelectedTime));
                }
            }
        }



        public AddBookingViewModel()
        {
            // Example MovieShows
            MovieShows = new ObservableCollection<MovieShow>
            {
            new MovieShow
            {
                Id = 1,
                MovieTitle = "Inception",
                PlayTime = new TimeOnly(14, 0),
                PlayDate = DateOnly.FromDateTime(DateTime.Now),
                Commercial = TimeSpan.FromMinutes(15),
                Duration = TimeSpan.FromMinutes(148)
            },
            new MovieShow
            {
                Id = 2,
                MovieTitle = "Avatar",
                PlayTime = new TimeOnly(16, 30),
                PlayDate = DateOnly.FromDateTime(DateTime.Now),
                Commercial = TimeSpan.FromMinutes(20),
                Duration = TimeSpan.FromMinutes(162)
            }
        };

            // Populate AvailableTimes (30 min increments from 08:00 to 23:30)
            AvailableTimes = new ObservableCollection<TimeOnly>(
                Enumerable.Range(8, 16) // Hours 8 to 23
                          .SelectMany(h => new[] { new TimeOnly(h, 0), new TimeOnly(h, 30) })
            );
        }

    }
}
