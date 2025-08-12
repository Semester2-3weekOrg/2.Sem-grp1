using System.IO;
using MemoryGame.Data.Interfaces;
using MemoryGame.Model;

namespace MemoryGame.Data.FileRepo
{
    internal class FileGameStatsRepository : IGameStatsRepository
    {
        private static string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        private static string folder = Path.Combine(projectPath, "Data");
        private static string subFolder = Path.Combine(folder, "SavedFiles");

        private readonly string _playerStatsPath;
        private readonly string _highScoresPath;

        public FileGameStatsRepository(string filePath)
        {
            _playerStatsPath = Path.Combine(subFolder, "playerStats.csv");
            _highScoresPath = Path.Combine(subFolder, "highScores.csv");
        }

        public List<GameStats> LoadFromCSV(string path)
        {
            var gameStatsList = new List<GameStats>();

            if (!File.Exists(path))
            {
                return gameStatsList;
            }

            using var read = new StreamReader(path);
            string? line;
            while ((line = read.ReadLine()) != null)
            {
                var parts = line.Split(',');
                if (parts.Length == 4 && int.TryParse(parts[1], out var moves) && TimeSpan.TryParse(parts[2], out var time) && DateTime.TryParse(parts[3], out var date))
                {
                    gameStatsList.Add(new GameStats
                    {
                        PlayerName = parts[0],
                        Moves = moves,
                        GameTime = time,
                        CompletedAt = date
                    });
                }
            }
            return gameStatsList;

        }

        public void SavePlayerGame(GameStats stats)
        {
            using var writer = new StreamWriter(_playerStatsPath, append: true);
            writer.WriteLine($"{stats.PlayerName},{stats.Moves},{stats.GameTime},{stats.CompletedAt:yyyy-MM-dd}");
        }

        public void SaveHighScore(GameStats stats)
        {
            using var writer = new StreamWriter(_highScoresPath, append: true);
            writer.WriteLine($"{stats.PlayerName},{stats.Moves},{stats.GameTime},{stats.CompletedAt:yyyy-MM-dd}");
        }

        public List<GameStats> LoadPlayerGames(string playerName)
        {
            return LoadFromCSV(_playerStatsPath)
                .Where(s => s.PlayerName.Equals(playerName, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<GameStats> LoadHighScores()
        {
            return LoadFromCSV(_highScoresPath)
                .OrderByDescending(s => s.Moves)
                .Take(10)
                .ToList();
        }

    }
}
