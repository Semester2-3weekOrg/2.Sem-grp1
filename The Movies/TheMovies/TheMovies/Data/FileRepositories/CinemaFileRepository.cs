using TheMovies.Data.Interfaces;
using TheMovies.Models;

namespace TheMovies.Data.FileRepositories
{
    internal class CinemaFileRepository : BaseFileRepository<Cinema>, ICinemaRepository
    {
        public CinemaFileRepository()
        : base(new Datahandler<Cinema>(

            serializeFunc: cinema => $"{cinema.Id};{cinema.CinemaName};{cinema.CinemaInitials};{cinema.Halls}",
            deserializeFunc: line =>
            {
                var parts = line.Split(';');
                return new Cinema
                {
                    Id = int.Parse(parts[0]),
                    CinemaName = parts[1],
                    CinemaInitials = parts[2],
                    Halls = string.IsNullOrWhiteSpace(parts[3])
                    ? new List<CinemaHall>()
                    : parts[3].Split('|').Select(hallStr =>
                      {
                          var hallParts = hallStr.Split(',');
                          return new CinemaHall
                          {
                              Id = int.Parse(hallParts[0]),
                              HallNumber = hallParts[1],
                              CinemaInitials = hallParts[2],
                              HallId = hallParts[3],
                              CleaningTime = int.Parse(hallParts[4])
                          };
                      }).ToList(),
                };
            }))
        {
        }
    }
}
