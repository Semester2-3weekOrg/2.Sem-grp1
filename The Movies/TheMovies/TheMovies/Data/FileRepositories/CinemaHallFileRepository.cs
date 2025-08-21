using TheMovies.Data.Interfaces;
using TheMovies.Models;

namespace TheMovies.Data.FileRepositories
{
    internal class CinemaHallFileRepository : BaseFileRepository<CinemaHall>, ICinemaHallRepository
    {
        public CinemaHallFileRepository()
        : base(new Datahandler<CinemaHall>(

            serializeFunc: cinemaHall => $"{cinemaHall.Id};{cinemaHall.HallId}",
            deserializeFunc: line =>
            {
                var parts = line.Split(';');
                return new CinemaHall
                {
                    Id = int.Parse(parts[0]),
                    HallNumber = parts[1],
                    CinemaInitials = parts[2],
                    HallId = parts[3],
                    CleaningTime = int.Parse(parts[4])
                };
            }))
        {
        }
    }
}
