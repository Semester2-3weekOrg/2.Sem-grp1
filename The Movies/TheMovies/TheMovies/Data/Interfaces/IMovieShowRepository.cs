using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMovies.Models;

namespace TheMovies.Data.Interfaces
{
    internal interface IMovieShowRepository : IBaseRepository<MovieShow>
    {
        MovieShow GetByMovieTitle(string movieTitle);
        List<MovieShow> GetByPlayDate(DateOnly playDate);
        List<MovieShow> GetByCinema(string cinemaName);
        List<MovieShow> GetByCinemaHall(string cinemaHallId);
    }
}
