using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TheMovies.Data.FileRepositories;
using TheMovies.Models;

namespace TheMovies.ViewModels
{

    public class MainViewModel : INotifyPropertyChanged
    {
        private MovieFileRepository _movieRepo;
        // Lister til visning.
        public ObservableCollection<Movie> Movies => _movieRepo.Items;
        public ObservableCollection<Genre> Genres { get; set; }

        // Bindings til inputfelter

        private int _duration;
        public int Duration
        {
            get => _duration;
            set { _duration = value; OnPropertyChanged(); }
        }

        private Genre? _selectedGenre;
        public Genre? SelectedGenre
        {
            get => _selectedGenre;
            set { _selectedGenre = value; OnPropertyChanged(); }
        }

        private string _statusMessage = string.Empty; // Initialized to avoid CS8618
        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        // Commands
        public ICommand RemoveMovieCommand { get; }

        public MainViewModel()
        {

            _movieRepo = new MovieFileRepository();

            #region DUMMY DATA
            Genres = new ObservableCollection<Genre>
            {
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Comedy" },
                new Genre { Id = 3, Name = "Drama" },
                new Genre { Id = 4, Name = "Thriller" },
                new Genre { Id = 5, Name = "War" },
                new Genre { Id = 6, Name = "Biography" },
                new Genre { Id = 7, Name = "Music" },
                new Genre { Id = 8, Name = "History" },
                new Genre { Id = 9, Name = "Crime" },
                new Genre { Id = 10, Name = "Romance" },
                new Genre { Id = 11, Name = "Horror" },
                new Genre { Id = 12, Name = "Mystery" },
                new Genre { Id = 13, Name = "Adventure" },
                new Genre { Id = 14, Name = "Fantasy" }
            };

            //List<Movie> movie;
            //{
            //    new Movie { Id = 1, Title = "1917", Length = 117, Genre = Genres.First(g => g.Name == "Drama") }, // Drama, Thriller, War
            //    new Movie { Id = 2, Title = "The Wife", Length = 99, Genre = Genres.First(g => g.Name == "Drama") },
            //    new Movie { Id = 3, Title = "Ayka", Length = 100, Genre = Genres.First(g => g.Name == "Drama") },
            //    new Movie { Id = 4, Title = "Bohemian Rhapsody", Length = 134, Genre = Genres.First(g => g.Name == "Biography") },
            //    new Movie { Id = 5, Title = "Darkest Hour", Length = 125, Genre = Genres.First(g => g.Name == "Biography") },
            //    new Movie { Id = 6, Title = "Dogman", Length = 103, Genre = Genres.First(g => g.Name == "Crime") },
            //    new Movie { Id = 7, Title = "Dunkirk", Length = 106, Genre = Genres.First(g => g.Name == "Action") },
            //    new Movie { Id = 8, Title = "Green Book", Length = 130, Genre = Genres.First(g => g.Name == "Biography") },
            //    new Movie { Id = 9, Title = "Joker", Length = 122, Genre = Genres.First(g => g.Name == "Crime") },
            //    new Movie { Id = 10, Title = "Judy", Length = 118, Genre = Genres.First(g => g.Name == "Biography") },
            //    new Movie { Id = 11, Title = "Little Joe", Length = 105, Genre = Genres.First(g => g.Name == "Drama") },
            //    new Movie { Id = 12, Title = "Pain & Glory", Length = 113, Genre = Genres.First(g => g.Name == "Drama") },
            //    new Movie { Id = 13, Title = "Parasite", Length = 132, Genre = Genres.First(g => g.Name == "Comedy") },
            //    new Movie { Id = 14, Title = "Shoplifters", Length = 121, Genre = Genres.First(g => g.Name == "Crime") },
            //    new Movie { Id = 15, Title = "The Favourite", Length = 119, Genre = Genres.First(g => g.Name == "Biography") },
            //    new Movie { Id = 16, Title = "The Shape of Water", Length = 123, Genre = Genres.First(g => g.Name == "Adventure") },
            //    new Movie { Id = 17, Title = "Three Billboards Outside Ebbing, Missouri", Length = 115, Genre = Genres.First(g => g.Name == "Comedy") }
            //};
            #endregion

            //RemoveMovieCommand = new RelayCommand(_ => RemoveMovie());
        }


        public event PropertyChangedEventHandler? PropertyChanged; // Nullable to avoid CS8618
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
