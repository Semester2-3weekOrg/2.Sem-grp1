using System.IO;
using System.Windows;
using MemoryGame.Data.FileRepo;

namespace MemoryGame
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string dataFolder = Path.Combine(Environment.CurrentDirectory, "Data");


            var repo = new FileGameStatsRepository(dataFolder);

            var loadedStats = repo.LoadPlayerGames("Alex");


            foreach (var stat in loadedStats)
            {
                MessageBox.Show($"Loaded: {stat.PlayerName} scored {stat.Moves} on {stat.CompletedAt.ToShortDateString()} and used {stat.GameTime.TotalSeconds} seconds");
            }

            MessageBox.Show($"LoadedStats count: {loadedStats.Count}");
            // Create a new GameStats instance
            //var testStats = new GameStats
            //{
            //    PlayerName = "TestPlayer",
            //    Moves = 150,
            //    GameTime = TimeSpan.FromSeconds(20),
            //    CompletedAt = DateTime.Now
            //};

            // Save the test stats to CSV
            //repo.SavePlayerGame(testStats);

            // Load all stats for the player

            // After this, your WPF app will continue launching normally


            
        }
    }

}
