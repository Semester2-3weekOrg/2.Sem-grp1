using System.Collections.Generic;
using MemoryGameByJohnny.Models;

namespace MemoryGameByJohnny.Interfaces
{
    /// <summary>
    /// Abstraction for reading and writing game statistics (high scores).
    /// Intended to decouple persistence (e.g., CSV file) from the ViewModel/UI.
    ///
    /// Typical usage:
    /// - Call <see cref="Save(GameStats)"/> after a completed game.
    /// - Use <see cref="GetTop(int)"/> to show a high-score list (e.g., Top 10).
    /// - Use <see cref="GetByPlayer(string)"/> to display a player’s history.
    ///
    /// Notes/Conventions (recommended, not enforced by the interface):
    /// - Sorting for "Top" is usually by fewest moves, then shortest time.
    /// - Missing or empty data sources should return an empty list (not throw).
    /// - Implementations should be robust to malformed lines/records and skip them.
    /// </summary>
    public interface IGameStatsRepository
    {
        /// <summary>
        /// Persists a single completed game’s statistics.
        /// </summary>
        /// <param name="stats">The game statistics to save (must represent a completed game).</param>
        void Save(GameStats stats);

        /// <summary>
        /// Returns the top <paramref name="count"/> scores according to the implementation’s ranking rules
        /// (commonly: lowest moves first, then shortest time).
        /// </summary>
        /// <param name="count">Maximum number of items to return (default 10).</param>
        /// <returns>A read-only list with at most <paramref name="count"/> items. Empty if none.</returns>
        IReadOnlyList<GameStats> GetTop(int count = 10);

        /// <summary>
        /// Returns all saved games for a specific player.
        /// Implementations should treat player name matching in a sensible way (e.g., case-insensitive).
        /// </summary>
        /// <param name="playerName">The player’s name to filter by.</param>
        /// <returns>A read-only list of all matching records. Empty if none.</returns>
        IReadOnlyList<GameStats> GetByPlayer(string playerName);
    }
}
