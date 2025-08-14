using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheMovies.Commands;
using TheMovies.Models;

namespace TheMovies.ViewModels
{
    /// <summary>
    /// Represents the main view model for managing movies and genres in a user interface.
    /// </summary>
    /// <remarks>This view model provides collections of movies and genres for display, bindings for user
    /// input fields,  and commands for adding and removing movies. It implements <see cref="INotifyPropertyChanged"/>
    /// to  support data binding and notify the UI of property changes.</remarks>
    public class MainViewModel : INotifyPropertyChanged
    {
        // Lister til visning
        public ObservableCollection<Movie> Movies { get; set; }
        public ObservableCollection<Genre> Genres { get; set; }

        // Bindings til inputfelter
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

        private string _statusMessage = string.Empty; // Initialized to avoid CS8618
        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        // Commands
        public ICommand AddMovieCommand { get; }
        public ICommand RemoveMovieCommand { get; }

        public MainViewModel()
        {
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

            Movies = new ObservableCollection<Movie>
            {
                new Movie { Id = 1, Title = "1917", Length = 117, Genre = Genres.First(g => g.Name == "Drama") }, // Drama, Thriller, War
                new Movie { Id = 2, Title = "The Wife", Length = 99, Genre = Genres.First(g => g.Name == "Drama") },
                new Movie { Id = 3, Title = "Ayka", Length = 100, Genre = Genres.First(g => g.Name == "Drama") },
                new Movie { Id = 4, Title = "Bohemian Rhapsody", Length = 134, Genre = Genres.First(g => g.Name == "Biography") },
                new Movie { Id = 5, Title = "Darkest Hour", Length = 125, Genre = Genres.First(g => g.Name == "Biography") },
                new Movie { Id = 6, Title = "Dogman", Length = 103, Genre = Genres.First(g => g.Name == "Crime") },
                new Movie { Id = 7, Title = "Dunkirk", Length = 106, Genre = Genres.First(g => g.Name == "Action") },
                new Movie { Id = 8, Title = "Green Book", Length = 130, Genre = Genres.First(g => g.Name == "Biography") },
                new Movie { Id = 9, Title = "Joker", Length = 122, Genre = Genres.First(g => g.Name == "Crime") },
                new Movie { Id = 10, Title = "Judy", Length = 118, Genre = Genres.First(g => g.Name == "Biography") },
                new Movie { Id = 11, Title = "Little Joe", Length = 105, Genre = Genres.First(g => g.Name == "Drama") },
                new Movie { Id = 12, Title = "Pain & Glory", Length = 113, Genre = Genres.First(g => g.Name == "Drama") },
                new Movie { Id = 13, Title = "Parasite", Length = 132, Genre = Genres.First(g => g.Name == "Comedy") },
                new Movie { Id = 14, Title = "Shoplifters", Length = 121, Genre = Genres.First(g => g.Name == "Crime") },
                new Movie { Id = 15, Title = "The Favourite", Length = 119, Genre = Genres.First(g => g.Name == "Biography") },
                new Movie { Id = 16, Title = "The Shape of Water", Length = 123, Genre = Genres.First(g => g.Name == "Adventure") },
                new Movie { Id = 17, Title = "Three Billboards Outside Ebbing, Missouri", Length = 115, Genre = Genres.First(g => g.Name == "Comedy") }
            };

            AddMovieCommand = new RelayCommand(AddMovie);
            RemoveMovieCommand = new RelayCommand(RemoveMovie);
        }

        private void AddMovie()
        {
            if (!string.IsNullOrWhiteSpace(MovieName) && SelectedGenre != null && Duration > 0)
            {
                Movies.Add(new Movie
                {
                    Id = Movies.Count + 1,
                    Title = MovieName,
                    Length = Duration,
                    Genre = SelectedGenre
                });

                StatusMessage = "Successfully added!";
                MovieName = string.Empty;
                Duration = 0;
                SelectedGenre = null;
            }
            else
            {
                StatusMessage = "Please fill out all fields!";
            }
        }

        private void RemoveMovie()
        {
            var movieToRemove = Movies.FirstOrDefault(m => m.Title == MovieName);
            if (movieToRemove != null)
            {
                Movies.Remove(movieToRemove);
                StatusMessage = "Successfully removed!";
            }
            else
            {
                StatusMessage = "Movie not found!";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged; // Nullable to avoid CS8618
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
