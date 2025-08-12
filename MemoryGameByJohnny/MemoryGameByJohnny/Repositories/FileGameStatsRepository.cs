using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using MemoryGameByJohnny.Interfaces;
using MemoryGameByJohnny.Models;

namespace MemoryGame.Repositories
{
    /// <summary>
    /// File-based implementation of <see cref="IGameStatsRepository"/> that persists
    /// and reads game statistics in a simple CSV file.
    ///
    /// CSV contract:
    ///   Header: PlayerName,Moves,GameTime,CompletedAt
    ///   Example: Anna,12,00:01:45,2024-08-07 10:30:25
    ///
    /// Conventions:
    /// - GameTime is formatted as "hh:mm:ss".
    /// - CompletedAt is written in UTC as "yyyy-MM-dd HH:mm:ss".
    /// - Missing file => treated as "no data" (empty list), not an error.
    /// - Malformed lines should be skipped (best-effort parsing).
    /// </summary>
    public sealed class FileGameStatsRepository : IGameStatsRepository
    {
        private readonly string _filePath;

        // Fixed header for a newly created file.
        private const string Header = "PlayerName,Moves,GameTime,CompletedAt";

        /// <summary>
        /// Create a repository bound to a specific CSV file path.
        /// </summary>
        /// <param name="filePath">Target CSV file path (can include directories).</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="filePath"/> is null/empty.</exception>
        public FileGameStatsRepository(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("filePath must not be empty.", nameof(filePath));

            _filePath = filePath;
        }

        /// <summary>
        /// Appends a single game's statistics to the CSV file.
        /// Creates the directory and the file (with header) if they do not exist.
        /// </summary>
        /// <param name="stats">The completed game's stats to persist.</param>
        /// <exception cref="IOException">Wraps low-level I/O errors with a clearer message.</exception>
        public void Save(GameStats stats)
        {
            try
            {
                // 1) Ensure directory exists.
                string? dir = Path.GetDirectoryName(_filePath);
                if (!string.IsNullOrWhiteSpace(dir))
                    Directory.CreateDirectory(dir);

                // 2) Create file with header if it doesn't exist.
                if (!File.Exists(_filePath))
                    File.WriteAllText(_filePath, Header + Environment.NewLine);

                // 3) Convert stats to a CSV row (culture-invariant formatting).
                var line = ToCsvLine(stats);

                // 4) Append the row.
                File.AppendAllText(_filePath, line + Environment.NewLine);
            }
            catch (IOException ioEx)
            {
                throw new IOException($"Could not save stats to '{_filePath}'.", ioEx);
            }
        }

        /// <summary>
        /// Returns the top <paramref name="count"/> results, sorted by fewest moves,
        /// then by shortest game time.
        /// </summary>
        /// <param name="count">Max number of rows to return (default 10).</param>
        /// <returns>Read-only list of results; empty if no file or no valid rows.</returns>
        /// <exception cref="IOException">Wraps low-level I/O errors with a clearer message.</exception>
        public IReadOnlyList<GameStats> GetTop(int count = 10)
        {
            if (count <= 0) return Array.Empty<GameStats>();
            if (!File.Exists(_filePath)) return Array.Empty<GameStats>();

            try
            {
                var list = new List<GameStats>(32);

                foreach (var line in File.ReadLines(_filePath))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    if (line.StartsWith("PlayerName", StringComparison.OrdinalIgnoreCase)) continue;

                    if (TryParseLine(line, out var stats) && stats != null)
                        list.Add(stats);
                }

                return list
                    .OrderBy(s => s.Moves)
                    .ThenBy(s => s.GameTime)
                    .Take(count)
                    .ToList();
            }
            catch (IOException ioEx)
            {
                throw new IOException($"Could not read stats from '{_filePath}'.", ioEx);
            }
        }

        /// <summary>
        /// Returns all results for the given player name.
        /// </summary>
        /// <param name="playerName">Player name to filter by (suggested: case-insensitive compare).</param>
        /// <returns>Read-only list of that player's results; empty if none or file missing.</returns>
        public IReadOnlyList<GameStats> GetByPlayer(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName)) return Array.Empty<GameStats>();
            if (!File.Exists(_filePath)) return Array.Empty<GameStats>();

            try
            {
                var list = new List<GameStats>(16);
                foreach (var line in File.ReadLines(_filePath))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    if (line.StartsWith("PlayerName", StringComparison.OrdinalIgnoreCase)) continue;

                    if (TryParseLine(line, out var s) && s != null &&
                        string.Equals(s.PlayerName, playerName, StringComparison.OrdinalIgnoreCase))
                    {
                        list.Add(s);
                    }
                }

                // Same sort as Top, but no Take()
                return list
                    .OrderBy(s => s.Moves)
                    .ThenBy(s => s.GameTime)
                    .ToList();
            }
            catch (IOException ioEx)
            {
                throw new IOException($"Could not read stats from '{_filePath}'.", ioEx);
            }
        }

        // ----------------- Helpers -----------------

        /// <summary>
        /// Serializes a <see cref="GameStats"/> to a single CSV row using
        /// invariant culture and the agreed date/time formats.
        /// </summary>
        private static string ToCsvLine(GameStats s)
        {
            // Simple CSV without quote-escaping. Assumes PlayerName does not contain
            // commas or line breaks. If needed, add proper CSV quoting later.
            // Formats:
            // - GameTime: "hh:mm:ss"
            // - CompletedAt: "yyyy-MM-dd HH:mm:ss" in UTC
            var player = s.PlayerName?.Trim() ?? "Guest";
            var moves = s.Moves.ToString();
            var gameTime = s.GameTime.ToString(@"hh\:mm\:ss");
            var completed = s.CompletedAt.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

            return $"{player},{moves},{gameTime},{completed}";
        }

        /// <summary>
        /// Tries to parse a CSV row into a <see cref="GameStats"/> using the repository's CSV contract.
        /// Returns false on any parse error and leaves <paramref name="stats"/> as null.
        /// </summary>
        private static bool TryParseLine(string line, out GameStats? stats)
        {
            stats = null;

            // Simple split (assumes no commas inside PlayerName)
            var parts = line.Split(',', 4, StringSplitOptions.TrimEntries);
            if (parts.Length != 4) return false;

            var player = parts[0];
            if (!int.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out var moves))
                return false;

            if (!TimeSpan.TryParseExact(parts[2], @"hh\:mm\:ss", CultureInfo.InvariantCulture, out var gameTime))
                return false;

            if (!DateTime.TryParseExact(
                    parts[3],
                    "yyyy-MM-dd HH:mm:ss",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                    out var completedUtc))
                return false;

            stats = new GameStats
            {
                PlayerName = player,
                Moves = moves,
                GameTime = gameTime,
                CompletedAt = completedUtc // keep as UTC in model
            };
            return true;
        }


    }
}
