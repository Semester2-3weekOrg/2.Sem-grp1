using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Represents a movie.
/// </summary>
namespace TheMovies.Models
{
    public class Movie
    {
        public int Id { get; set; } // Unique identifier for the movie
        public required string Title { get; set; } // Title of the movie
        public int? Length { get; set; } // Length of the movie in minutes, nullable to allow for unknown lengths
        public required Genre Genre { get; set; } // Genre of the movie, required to ensure every movie has a genre

        //Overriding ToString method for better readability in UI
        public override string ToString()
        {
            return $"{Title} ({Length} min) - {Genre.Name}";
        }
    }
}
