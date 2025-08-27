using System.Windows;
using TheMovies.Data.FileRepositories;
using TheMovies.Helpers;
using TheMovies.Models;


namespace TheMovies
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly CinemaFileRepository _cinemaRepo = new CinemaFileRepository();
        private readonly CinemaHallFileRepository _cinemaHallRepo = new CinemaHallFileRepository();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            //var cinemaName = "CineMaxx Århus";
            //var cinema = new Cinema()
            //{
            //    Id = _cinemaRepo.Items.Count + 1,
            //    CinemaName = cinemaName,
            //    CinemaInitials = InitialsConverter.GetInitialsFromName(cinemaName),

            //};
            //_cinemaRepo.Add(cinema);
            //var hall = new CinemaHall()
            //{
            //    Id = _cinemaHallRepo.Items.Count + 1,
            //    HallNumber = "1",
            //    CinemaInitials = InitialsConverter.GetInitialsFromName(cinemaName)
            //};
            //cinema.Halls.Add(hall);
            //_cinemaHallRepo.Add(hall);
            //_cinemaRepo.Update(cinema);


        }
    }

}
