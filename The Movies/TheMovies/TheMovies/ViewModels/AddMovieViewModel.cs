using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using TheMovies.Commands;
using TheMovies.Data.FileRepositories;
using TheMovies.Models;

namespace TheMovies.ViewModels
{
    internal class AddMovieViewModel : INotifyPropertyChanged
    {
        private readonly MovieFileRepository _movieRepo;
        #region Lister til visning
        //public ObservableCollection<Movie> Movies { get; set; }
        public ObservableCollection<Genre> Genres { get; set; }
        public ObservableCollection<Movie> Movies => _movieRepo.Items;
        #endregion

        #region Binding til inputfelter

        private string _movieName = string.Empty; // Initialized to avoid CS8618
        public string MovieName
        {
            get => _movieName;
            set { _movieName = value; OnPropertyChanged(); }
        }
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
        private string _instructor;
        public string Instructor
        {
            get => _instructor;
            set { _instructor = value; OnPropertyChanged(); }
        }
        private DateOnly? _premiereDate;
        public DateOnly? PremiereDate
        {
            get => _premiereDate;
            set { _premiereDate = value; OnPropertyChanged(); }
        }
        private string? _premiereDateInput;
        public string? PremiereDateInput
        {
            get => _premiereDateInput;
            set { _premiereDateInput = value; OnPropertyChanged(); PremiereDate = TryParseToDate(value); }
        }
        private string _statusMessage = string.Empty;
        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        #endregion

        #region Commands
        public ICommand AddMovieCommand { get; }
        //public ICommand RemoveMovieCommand { get; }
        public ICommand SaveAllCommand { get; }
        #endregion

        public AddMovieViewModel()
        {
            //_movieRepo = new MovieFileRepository();

            // Create the repository
            _movieRepo = new MovieFileRepository();

            AddMovieCommand = new RelayCommand(_ => AddMovie());
            //RemoveMovieCommand = new RelayCommand(movie => RemoveMovie(movie as Movie), movie => movie is Movie);
            SaveAllCommand = new RelayCommand(_ => _movieRepo.SaveAll());

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

            //Movies = new ObservableCollection<Movie>
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
        }

        #region METHODS
        private void AddMovie()
        {
            if (!string.IsNullOrWhiteSpace(MovieName) && SelectedGenre != null && Duration > 0)
            {

                var newMovie = new Movie()
                {
                    Id = _movieRepo.Items.Count + 1,
                    Title = MovieName,
                    Length = Duration,
                    Genre = SelectedGenre,
                    Instructor = Instructor,
                    PremiereDate = PremiereDate
                };
                //_movieRepo.Items.Add(newMovie);
                _movieRepo.Add(newMovie);
                StatusMessage = "Successfully added!";
                MessageBox.Show($"{StatusMessage}");
                MovieName = string.Empty;
                Duration = 0;
                SelectedGenre = null;
                Instructor = string.Empty;
                PremiereDate = DateOnly.MinValue;
            }
            else
            {
                StatusMessage = "Please fill out all fields!";
                MessageBox.Show($"{StatusMessage}");
            }
        }

        private DateOnly? TryParseToDate(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;
            string cleaned = Regex.Replace(input.Trim(), @"\D", "");

            if (cleaned.Length == 4) // e.g. 8825 -> 08/08/2025
                cleaned = $"0{cleaned[0]}0{cleaned[1]}{cleaned[2]}{cleaned[3]}";

            string? format = cleaned.Length switch
            {
                6 => "ddMMyy",   // 080825 -> 08/08/25
                8 => "ddMMyyyy", // 08082025 -> 08/08/2025
                _ => null
            };

            if (format != null &&
                DateOnly.TryParseExact(cleaned, format, null, System.Globalization.DateTimeStyles.None, out var dt))
            {
                return dt;
            }

            if (DateOnly.TryParse(input, out var fallback))
                return fallback;

            return null;
        }


        #endregion


        public event PropertyChangedEventHandler? PropertyChanged; // Nullable to avoid CS8618
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
