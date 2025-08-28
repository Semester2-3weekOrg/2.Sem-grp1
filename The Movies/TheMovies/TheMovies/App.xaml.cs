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

        }
    }

}
