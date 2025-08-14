using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MemoryGameByJohnny.Models
{
    /// <summary>
    /// Represents a single memory card in the 4x4 grid.
    /// Implements <see cref="INotifyPropertyChanged"/> so WPF bindings update
    /// when the card’s state changes (flipped/matched).
    /// </summary>
    /// <remarks>
    /// Notes:
    /// - <see cref="IsFlipped"/> is true while the card is face-up (either being inspected
    ///   or because it has been matched).
    /// - <see cref="IsMatched"/> is true once the card has been paired; matched cards
    ///   should remain visible.
    /// - Keep game logic in the ViewModel; this model only holds state and raises change notifications.
    /// </remarks>
    public class Card : INotifyPropertyChanged
    {
        private int _id;
        private string _symbol = "";
        private bool _isFlipped;
        private bool _isMatched;

        /// <summary>
        /// Unique identifier for the card instance (useful for tracking and bindings).
        /// </summary>
        public int Id
        {
            get => _id;
            set { if (_id != value) { _id = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// Visual symbol shown when the card is face-up (e.g., letter or emoji).
        /// Two cards share the same symbol to form a matching pair.
        /// </summary>
        public string Symbol
        {
            get => _symbol;
            set { if (_symbol != value) { _symbol = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// Indicates whether the card is currently face-up (visible).
        /// </summary>
        public bool IsFlipped
        {
            get => _isFlipped;
            set { if (_isFlipped != value) { _isFlipped = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// Indicates whether the card has been successfully matched with its pair.
        /// Matched cards should remain visible and no longer be flippable.
        /// </summary>
        public bool IsMatched
        {
            get => _isMatched;
            set { if (_isMatched != value) { _isMatched = value; OnPropertyChanged(); } }
        }

        /// <summary>
        /// Raised whenever a property value changes, so bound UI elements can refresh.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Helper to raise <see cref="PropertyChanged"/> for the calling property.
        /// </summary>
        /// <param name="name">The property name (automatically supplied by the compiler).</param>
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
