using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Threading;
using MemoryGame.Data.FileRepo;
using MemoryGame.Model;

namespace MemoryGame.ViewModel
{
    internal class GameViewModel : INotifyPropertyChanged
    {
        string dataFolder = Path.Combine(Environment.CurrentDirectory, "Data");



        private readonly Random _rng = new Random();
        private readonly DispatcherTimer _timer;
        private TimeSpan _elapsed;
        private bool _isGameRunning;


        private string _playerName;
        public string PlayerName
        {
            get => _playerName;
            set { _playerName = value; OnPropertyChanged(); }
        }

        private int _moveCount;
        public int MoveCount
        {
            get => _moveCount;
            set { _moveCount = value; OnPropertyChanged(); }
        }

        private string _gameTime;
        public string GameTime
        {
            get => _gameTime;
            set { _gameTime = value; OnPropertyChanged(); }
        }
        private string _completedAt;
        public string CompletedAt
        {
            get => _completedAt;
            set { _completedAt = value; OnPropertyChanged(); }
        }

        private bool _isGameCompleted;
        public bool IsGameCompleted
        {
            get => _isGameCompleted;
            set { _isGameCompleted = value; OnPropertyChanged(); }
        }

        private Card _firstSelectedCard;
        public Card FirstSelectedCard
        {
            get => _firstSelectedCard;
            set
            {
                _firstSelectedCard = value;
                OnPropertyChanged();
            }
        }

        private Card _secondSelectedCard;
        public Card SecondSelectedCard
        {
            get => _secondSelectedCard;
            set
            {
                _secondSelectedCard = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<Card> Cards { get; set; }

        private bool _isBusy = false;

        public RelayCommand FlipCardCommand { get; }
        public RelayCommand StartGameCommand { get; }

        public GameViewModel()
        {

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;

            Cards = new ObservableCollection<Card>();

            StartGameCommand = new RelayCommand(_ => StartGame());
            FlipCardCommand = new RelayCommand(FlipCard, CanFlipCard);

            NewGame();
        }

        private bool CanFlipCard(object? parameter)
        {
            var card = parameter as Card;
            return card != null && !card.IsFlipped && !card.IsMatched && !_isBusy;
        }

        private void FlipCard(object? parameter)
        {
            if (!_timer.IsEnabled)
            {
                _elapsed = TimeSpan.Zero;
                _timer.Start();
            }

            var card = parameter as Card;
            if (card == null)
            {
                return;
            }

            if (_isBusy)
            {
                return;
            }

            if (FirstSelectedCard == null)
            {
                FirstSelectedCard = card;
                MoveCount++;
                return;
            }
            else
            {
                SecondSelectedCard = card;
                _isBusy = true;
                MoveCount++;

                System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(100);
                timer.Tick += (s, e) =>
                {
                    timer.Stop();
                    CheckForMatch();
                    _isBusy = false;
                };
                timer.Start();
            }
        }

        private void CheckForMatch()
        {
            if (FirstSelectedCard != null && SecondSelectedCard != null)
            {
                if (FirstSelectedCard.Symbol == SecondSelectedCard.Symbol)
                {
                    FirstSelectedCard.IsMatched = true;
                    SecondSelectedCard.IsMatched = true;
                }
                else
                {
                    FirstSelectedCard.IsMatched = false;
                    SecondSelectedCard.IsMatched = false;
                }
            }
            FirstSelectedCard = null;
            SecondSelectedCard = null;

            if (Cards.All(c => c.IsMatched))
            {
                StopGame();
            }
        }

        private void StartGame()
        {
            _elapsed = TimeSpan.Zero;
            GameTime = _elapsed.ToString(@"mm\:ss\:ms");
            IsGameCompleted = false;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!_isGameRunning) return;

            _elapsed = _elapsed.Add(TimeSpan.FromSeconds(1));
            GameTime = _elapsed.ToString(@"mm\:ss\:ms");
        }

        public void StopGame()
        {
            IsGameCompleted = true;
            _isGameRunning = false;
            _timer.Stop();
            var repo = new FileGameStatsRepository(dataFolder);
            var stats = new GameStats()
            {
                PlayerName = _playerName,
                Moves = _moveCount,
                GameTime = _elapsed,
                CompletedAt = DateTime.Now
            };
            repo.SaveHighScore(stats);
            repo.SavePlayerGame(stats);



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
        }

        public void NewGame()
        {
            _timer.Stop();
            MoveCount = 0;
            FirstSelectedCard = null;
            SecondSelectedCard = null;
            _isBusy = false;

            int pairCount = 8;

            var symbolPool = new List<string>()
            {
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
                "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
            };

            var chosenSymbols = new List<string>(symbolPool);
            Shuffle(chosenSymbols);
            chosenSymbols = chosenSymbols.GetRange(0, pairCount);

            var cardList = new List<Card>();
            int id = 1;

            foreach (var sym in chosenSymbols)
            {
                var c1 = new Card { Id = id++, Symbol = sym, IsFlipped = false, IsMatched = false };
                var c2 = new Card { Id = id++, Symbol = sym, IsFlipped = false, IsMatched = false };

                cardList.Add(c1);
                cardList.Add(c2);
            }

            Shuffle(cardList);

            Cards.Clear();

            foreach (var card in cardList)
            {
                Cards.Add(card);
            }

            OnPropertyChanged(nameof(Cards));
        }

        private void Shuffle<T>(IList<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = _rng.Next(i + 1);
                var tmp = list[i];
                list[i] = list[j];
                list[j] = tmp;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
