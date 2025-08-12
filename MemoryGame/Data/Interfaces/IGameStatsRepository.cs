using MemoryGame.Model;

namespace MemoryGame.Data.Interfaces
{
    internal interface IGameStatsRepository
    {
        void SavePlayerGame(GameStats gameStats);
        List<GameStats> LoadPlayerGames(string playerName);
        void SaveHighScore(GameStats gameStats);
        List<GameStats> LoadHighScores();
        List<GameStats> LoadFromCSV(string path);
    }
}
