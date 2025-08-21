using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMovies.Data.Interfaces;
using TheMovies.Models;

namespace TheMovies.Data.FileRepositories
{
    internal class MovieShowFileRepository : BaseFileRepository<MovieShow>, IMovieShowRepository
    {
        public MovieShowFileRepository()
        : base(new Datahandler<MovieShow>(

            serializeFunc: movieShow => $"{movieShow.Id};{movieShow.MovieTitle};{movieShow.PlayTime};{movieShow.PlayDate};{movieShow.Commercial};{movieShow.Duration}",
            deserializeFunc: line =>
            {
                var parts = line.Split(';');
                return new MovieShow
                {
                    Id = int.Parse(parts[0]),
                    MovieTitle = parts[1],
                    PlayTime = TimeOnly.Parse(parts[2]),
                    PlayDate =   DateOnly.Parse(parts[3]),
                    Commercial = TimeSpan.Parse(parts[4]),
                    Duration = TimeSpan.Parse(parts[5])
                };
            }))
        {
        }

        public List<MovieShow> GetByCinema(string cinemaName)
        {
            throw new NotImplementedException();
        }

        public List<MovieShow> GetByCinemaHall(string cinemaHallId)
        {
            throw new NotImplementedException();
        }

        public MovieShow GetByMovieTitle(string movieTitle)
        {
            throw new NotImplementedException();
        }

        public List<MovieShow> GetByPlayDate(DateOnly playDate)
        {
            throw new NotImplementedException();
        }
    }
}
