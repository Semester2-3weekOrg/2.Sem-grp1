using TheMovies.Data.Interfaces;
using TheMovies.Models;

namespace TheMovies.Data.FileRepositories
{
    internal class MovieFileRepository : BaseFileRepository<Movie>, IMovieRepository
    {
        public MovieFileRepository()
                : base(new DataHandler<Movie>(

                    serializeFunc: movie => $"{movie.Id};{movie.Title};{movie.Length};{movie.Genre.Name};{movie.Instructor};{movie.PremiereDate}",
                    deserializeFunc: line =>
                    {
                        var parts = line.Split(';');
                        return new Movie
                        {
                            Id = int.Parse(parts[0]),
                            Title = parts[1],
                            Length = int.Parse(parts[2]),
                            Genre = new Genre { Name = parts[3] },
                            Instructor = parts[4],
                            PremiereDate = DateOnly.Parse(parts[5])
                        };
                    }))
        {
        }
        public Movie GetByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public List<Movie> GetByGenre(string genre)
        {
            throw new NotImplementedException();
        }

        public List<Movie> GetByInstructor(string instructor)
        {
            throw new NotImplementedException();
        }

    }
}
