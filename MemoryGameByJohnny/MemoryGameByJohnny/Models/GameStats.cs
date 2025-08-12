using System;

namespace MemoryGameByJohnny.Models
{
    /// <summary>
    /// Immutable-like data record for a completed game session (high-score entry).
    /// Used by the repository to persist and retrieve results.
    ///
    /// Notes:
    /// - <see cref="PlayerName"/> is required (validated by the caller/UI).
    /// - <see cref="Moves"/> should count each pair attempt (including mismatches).
    /// - <see cref="GameTime"/> is the total elapsed play time (recommended: measured via Stopwatch).
    /// - <see cref="CompletedAt"/> should be stored in UTC to avoid time zone ambiguity.
    ///
    /// CSV convention (if using a file repository):
    ///   PlayerName,Moves,GameTime,CompletedAt
    /// Example:
    ///   Anna,12,00:01:45,2024-08-07 10:30:25
    /// Where:
    ///   GameTime  -> formatted as "hh:mm:ss"
    ///   CompletedAt (UTC) -> formatted as "yyyy-MM-dd HH:mm:ss"
    /// </summary>
    public class GameStats
    {
        /// <summary>
        /// The player's display name to associate with this result (required).
        /// </summary>
        public required string PlayerName { get; set; }

        /// <summary>
        /// Number of moves (pair attempts) used to finish the game.
        /// </summary>
        public int Moves { get; set; }

        /// <summary>
        /// Total time spent to complete the game.
        /// </summary>
        public TimeSpan GameTime { get; set; }

        /// <summary>
        /// The timestamp when the game was completed.
        /// Recommendation: store as UTC.
        /// </summary>
        public DateTime CompletedAt { get; set; }
    }
}
