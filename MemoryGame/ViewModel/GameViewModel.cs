using System.Collections.ObjectModel;
using MemoryGame.Model;

namespace MemoryGame.ViewModel
{
    internal class GameViewModel
    {
        public ObservableCollection<Card> Cards { get; set; }
        public string PlayerName { get; set; }
        public int MoveCount { get; set; }
        public string GameTime { get; set; }
        public bool IsGameCompleted { get; set; }
        public Card FirstSelectedCard { get; set; }
        public Card SecondSelectedCard { get; set; }
    }
}
