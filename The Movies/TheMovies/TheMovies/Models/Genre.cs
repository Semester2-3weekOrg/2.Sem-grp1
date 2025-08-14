using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Represents a movie genre.
/// </summary>
namespace TheMovies.Models
{
    public class Genre
    {
        public int Id { get; set; } // Unique identifier for the genre
        public required string Name { get; set; } // Name of the genre, required to ensure every genre has a name
    }
}
