using TheMovies.Models;

namespace TheMovies.Data.Interfaces
{
    internal interface IMovieRepository : IBaseRepository<Movie>
    {
        Movie GetByTitle(string title);
        List<Movie> GetByGenre(string genre);
        List<Movie> GetByInstructor(string instructor);

    }
}
