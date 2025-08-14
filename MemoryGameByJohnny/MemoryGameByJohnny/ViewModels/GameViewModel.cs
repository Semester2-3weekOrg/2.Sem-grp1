using MemoryGame.Repositories;
using MemoryGameByJohnny.Commands;
using MemoryGameByJohnny.Interfaces;
using MemoryGameByJohnny.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace MemoryGameByJohnny.ViewModels
{
    /// <summary>
    /// Primary ViewModel for the Memory game.
    /// 
    /// Responsibilities:
    /// - Holds UI-bound game state (cards, player name, moves, time, completion flag).
    /// - Implements the game flow: flipping cards, matching pairs, and end-of-game detection.
    /// - Exposes commands for flipping a card and starting a new game.
    /// - Updates a UI-friendly timer string while measuring elapsed time with <see cref="Stopwatch"/>.
    ///
    /// Notes:
    /// - Keeps logic in the ViewModel (MVVM). The View is only XAML bindings and triggers.
    /// - Uses a small state flag (<see cref="IsEvaluating"/>) to prevent extra clicks during match checks.
    /// - <see cref="FlipCard_Execute(object)"/> is asynchronous (non-blocking UI) when showing a mismatch delay.
    /// - Save command is present but wiring to a repository is done elsewhere.
    /// </summary>
    public sealed class GameViewModel : BaseViewModel
    {
        // --- Internal helpers/state ---

        private readonly Random _rng = new(); // Used for shuffling the deck.
        private readonly IGameStatsRepository _repo;
        /// <summary>
        /// True while comparing two selected cards; disables further flips during evaluation.
        /// </summary>
        private bool _isEvaluating;
        public bool IsEvaluating
        {
            get => _isEvaluating;
            set
            {
                if (SetProperty(ref _isEvaluating, value))
                    (FlipCardCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        private readonly Stopwatch _stopwatch = new();                  // Accurate time measurement.
        private readonly DispatcherTimer _uiTimer = new() { Interval = TimeSpan.FromMilliseconds(200) }; // UI-friendly time updates.

        // --- Properties required by the assignment ---

        private ObservableCollection<Card> _cards = new();
        /// <summary>
        /// The 16 cards in the current game (4x4). Bound to an ItemsControl in the View.
        /// </summary>
        public ObservableCollection<Card> Cards
        {
            get => _cards;
            set => SetProperty(ref _cards, value);
        }

        private string _playerName = "Guest";
        /// <summary>
        /// The current player's display name. (Validated/trimmed by the caller/UI.)
        /// </summary>
        public string PlayerName
        {
            get => _playerName;
            set => SetProperty(ref _playerName, value);
        }

        private int _moveCount;
        /// <summary>
        /// Number of moves (pair attempts). Incremented when the second card of a pair is flipped.
        /// </summary>
        public int MoveCount
        {
            get => _moveCount;
            set => SetProperty(ref _moveCount, value);
        }

        private string _gameTime = "00:00:00";
        /// <summary>
        /// UI-friendly elapsed time in hh:mm:ss, updated from a DispatcherTimer.
        /// </summary>
        public string GameTime
        {
            get => _gameTime;
            set => SetProperty(ref _gameTime, value);
        }

        private bool _isGameCompleted;
        /// <summary>
        /// True when all pairs are matched; used to enable Save and stop the timer.
        /// </summary>
        public bool IsGameCompleted
        {
            get => _isGameCompleted;
            set
            {
                if (SetProperty(ref _isGameCompleted, value))
                    (SaveStatsCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<GameStats> HighScores { get; } = new();
        public void RefreshHighScores()
        {
            HighScores.Clear();
            foreach (var s in _repo.GetTop(10))
                HighScores.Add(s);
        }
        private Card? _firstSelectedCard;
        /// <summary>
        /// The first selected (flipped) card in a pair attempt.
        /// </summary>
        public Card? FirstSelectedCard
        {
            get => _firstSelectedCard;
            set => SetProperty(ref _firstSelectedCard, value);
        }

        private Card? _secondSelectedCard;
        /// <summary>
        /// The second selected (flipped) card in a pair attempt.
        /// </summary>
        public Card? SecondSelectedCard
        {
            get => _secondSelectedCard;
            set => SetProperty(ref _secondSelectedCard, value);
        }

        // --- Commands exposed to the View ---

        /// <summary>
        /// Flips a card (parameter: the bound <see cref="Card"/>). Handles first/second selection, match check, and mismatch delay.
        /// </summary>
        public ICommand FlipCardCommand { get; }

        /// <summary>
        /// Starts a new game: resets state, generates and shuffles a fresh deck.
        /// </summary>
        public ICommand NewGameCommand { get; }

        /// <summary>
        /// Saves current game stats (enabled only when <see cref="IsGameCompleted"/> is true).
        /// Actual persistence is wired elsewhere.
        /// </summary>
        public ICommand SaveStatsCommand { get; }

        /// <summary>
        /// Initializes commands, creates a new game, and wires the UI timer to update <see cref="GameTime"/>.
        /// </summary>
        public GameViewModel()
        {
            FlipCardCommand = new RelayCommand(FlipCard_Execute, FlipCard_CanExecute);

            // Note: This first assignment is a placeholder and then overwritten below.
            // You can safely remove the placeholder line if you prefer.
            NewGameCommand = new RelayCommand(_ => { /* TODO: New game setup */ });

            SaveStatsCommand = new RelayCommand(SaveStats_Execute, _ => IsGameCompleted);

            // Actual NewGame binding:
            NewGameCommand = new RelayCommand(_ => NewGame());
            var dataPath = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
    "MemoryGameByJohnny", "scores.csv");

            _repo = new FileGameStatsRepository(dataPath);
            NewGame(); // Prepare an initial deck so the UI has content on startup.

            // Update the human-readable time string on a fixed interval.
            _uiTimer.Tick += (s, e) =>
            {
                GameTime = _stopwatch.Elapsed.ToString(@"hh\:mm\:ss");
            };
        }

        private void SaveStats_Execute(object? _)
        {
            // Basic validation/normalization
            var name = string.IsNullOrWhiteSpace(PlayerName) ? "Guest" : PlayerName.Trim();

            _repo.Save(new GameStats
            {
                PlayerName = name,
                Moves = MoveCount,
                GameTime = _stopwatch.Elapsed,
                CompletedAt = DateTime.UtcNow
            });
        }
        /// <summary>
        /// Guards the flip command: disallow flips while evaluating, or for already flipped/matched cards.
        /// </summary>
        private bool FlipCard_CanExecute(object? parameter)
        {
            if (IsEvaluating) return false;
            if (parameter is not Card card) return false;
            if (card.IsMatched || card.IsFlipped) return false;
            return true;
        }

        /// <summary>
        /// Handles flipping logic. On first flip, stores selection and ensures the timer is running.
        /// On second flip, increments moves, checks for match, and either marks matched or flips back after a short delay.
        /// Non-blocking: uses <see cref="Task.Delay(int)"/> so the UI remains responsive.
        /// </summary>
        private async void FlipCard_Execute(object? parameter)
        {
            if (parameter is not Card card) return;
            if (!FlipCard_CanExecute(card)) return;

            // First card
            if (FirstSelectedCard is null)
            {
                card.IsFlipped = true;
                FirstSelectedCard = card;
                OnPropertyChanged(nameof(FirstSelectedCard));
                EnsureTimerStarted();
                return;
            }

            // Second card
            if (SecondSelectedCard is null && !ReferenceEquals(card, FirstSelectedCard))
            {
                card.IsFlipped = true;
                SecondSelectedCard = card;
                OnPropertyChanged(nameof(SecondSelectedCard));

                MoveCount++;
                OnPropertyChanged(nameof(MoveCount));

                IsEvaluating = true;

                // Match?
                bool isMatch = FirstSelectedCard.Symbol == SecondSelectedCard.Symbol;
                if (isMatch)
                {
                    FirstSelectedCard.IsMatched = true;
                    SecondSelectedCard.IsMatched = true;

                    // Reset selections
                    FirstSelectedCard = null;
                    SecondSelectedCard = null;
                    IsEvaluating = false;

                    // Completed?
                    if (Cards.All(c => c.IsMatched))
                    {
                        IsGameCompleted = true;
                        StopTimer();
                        (SaveStatsCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    }
                }
                else
                {
                    // Short pause so the player can see the second card (without blocking the UI thread).
                    await Task.Delay(700);

                    // Flip back
                    FirstSelectedCard.IsFlipped = false;
                    SecondSelectedCard.IsFlipped = false;

                    // Reset selections
                    FirstSelectedCard = null;
                    SecondSelectedCard = null;
                    IsEvaluating = false;
                }

                // Re-evaluate CanExecute after state changes.
                (FlipCardCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Resets state, clears and regenerates a shuffled 16-card deck (8 pairs).
        /// Also resets and stops the timer and counters.
        /// </summary>
        private void NewGame()
        {
            // Reset state
            StopTimer();
            _stopwatch.Reset();
            PlayerName = "Guest";        // Keep or remove depending on whether you want to persist the typed name across games.
            MoveCount = 0;
            GameTime = "00:00:00";
            IsGameCompleted = false;
            FirstSelectedCard = null;
            SecondSelectedCard = null;

            // Generate and shuffle new cards
            var cards = CreateShuffledDeck();
            Cards = new ObservableCollection<Card>(cards);
        }

        /// <summary>
        /// Creates a 16-card deck (8 symbols duplicated) and shuffles it.
        /// </summary>
        private List<Card> CreateShuffledDeck()
        {
            // Pick 8 symbols (easy to swap to emojis later).
            var symbolsPool = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };
            var selected = symbolsPool.Take(8).ToList();

            // Duplicate to form pairs (16 cards).
            var pairs = selected.Concat(selected).ToList();

            // Instantiate Card objects
            var list = new List<Card>(16);
            int id = 1;
            foreach (var s in pairs)
            {
                list.Add(new Card
                {
                    Id = id++,
                    Symbol = s,
                    IsFlipped = false,
                    IsMatched = false
                });
            }

            // Shuffle using Fisher–Yates
            Shuffle(list);
            return list;
        }

        /// <summary>
        /// Fisher–Yates shuffle in-place.
        /// </summary>
        private void Shuffle<T>(IList<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = _rng.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }

        /// <summary>
        /// Starts the <see cref="Stopwatch"/> and UI timer if not already running.
        /// </summary>
        private void EnsureTimerStarted()
        {
            if (!_stopwatch.IsRunning)
            {
                _stopwatch.Start();
                _uiTimer.Start();
            }
        }

        /// <summary>
        /// Stops both the <see cref="Stopwatch"/> and the UI timer (safe to call multiple times).
        /// </summary>
        private void StopTimer()
        {
            if (_stopwatch.IsRunning)
                _stopwatch.Stop();

            _uiTimer.Stop();
        }
    }
}
