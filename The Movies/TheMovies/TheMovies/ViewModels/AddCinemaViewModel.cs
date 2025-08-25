using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TheMovies.Commands;
using TheMovies.Data.FileRepositories;
using TheMovies.Models;

namespace TheMovies.ViewModels
{

    internal class AddCinemaViewModel : INotifyPropertyChanged
    {
        private readonly CinemaFileRepository _cinemaRepo;
        #region Lister til visning
        public ObservableCollection<Cinema> Cinemas => _cinemaRepo.Items;



        #endregion

        #region Inputfelter
        private string _cinemaName = string.Empty;
        public string CinemaName

        {
            get => _cinemaName;
            set { _cinemaName = value; OnPropertyChanged(); }
        }

        private string _cinemaInitials = string.Empty;
        public string CinemaInitials
        {
            get => _cinemaInitials;
            set { _cinemaInitials = value; OnPropertyChanged(); }

        }

        private string _statusMessage = string.Empty;
        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }

        }


        #endregion

        #region Commands
        public ICommand AddCinemaCommand { get; }
        public ICommand SaveAllCommand { get; }
        #endregion

        public AddCinemaViewModel()

        {
            _cinemaRepo = new CinemaFileRepository();

            AddCinemaCommand = new RelayCommand(_ => AddCinema());
            SaveAllCommand = new RelayCommand(_ => _cinemaRepo.SaveAll());
        }

        private void AddCinema()
        {
            if (!string.IsNullOrWhiteSpace(CinemaName) && !string.IsNullOrWhiteSpace(CinemaInitials))
            {
                var newCinema = new Cinema()
                {
                    Id = _cinemaRepo.Items.Count + 1,
                    CinemaName = CinemaName,
                    CinemaInitials = CinemaInitials,
                    Halls = new List<CinemaHall>() // Fix: Provide a valid initializer for Halls
                };

                _cinemaRepo.Add(newCinema);
                StatusMessage = $"{newCinema.CinemaName}({newCinema.CinemaInitials}) successfully added!";
                MessageBox.Show($"{StatusMessage}");
                //Nulstil felter
                CinemaName = string.Empty;
                CinemaInitials = string.Empty;
            }
            else
            {
                StatusMessage = "Please fill out all fields!";
                MessageBox.Show($"{StatusMessage}");
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string PropertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        #endregion

    }
}