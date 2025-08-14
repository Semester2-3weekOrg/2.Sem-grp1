using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MemoryGame.Model;

namespace MemoryGame.ViewModel
{
    internal class CardViewModel : INotifyPropertyChanged
    {
        private readonly Card _card;

        public CardViewModel(Card card)
        {
            _card = card;
        }

        public int Id => _card.Id;
        public string Symbol => _card.Symbol;

        public bool IsFlipped
        {
            get => _card.IsFlipped;
            set
            {
                if (_card.IsFlipped != value)
                {
                    _card.IsFlipped = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsMatched
        {
            get => _card.IsMatched;
            set
            {
                if (_card.IsMatched != value)
                {
                    _card.IsMatched = value;
                    OnPropertyChanged();
                }
            }
        }





        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
