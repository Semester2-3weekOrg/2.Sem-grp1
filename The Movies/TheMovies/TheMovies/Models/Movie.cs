/// <summary>
/// Represents a movie.
/// </summary>
namespace TheMovies.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int? Length { get; set; }
        public required Genre Genre { get; set; }
        public string Instructor { get; set; }
        public DateOnly? PremiereDate { get; set; }

    }
}
