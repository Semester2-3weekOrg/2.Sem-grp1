using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMovies.Data.Interfaces;
using TheMovies.Helpers;
using TheMovies.Models;

namespace TheMovies.Data.FileRepositories
{
    internal class MovieFileRepository : BaseFileRepository<Movie>, IMovieRepository
    {
        public MovieFileRepository(string filePath) : base(FilePathProvider.GetSavedDataFilesPath("Movies.csv"))
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
