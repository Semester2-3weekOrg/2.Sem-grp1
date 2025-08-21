namespace TheMovies.Models
{
    class CinemaHall
    {
        public int Id { get; set; }

        private string _hallNumber;
        public string HallNumber
        {
            get => _hallNumber;
            set
            {
                _hallNumber = value;
                UpdateHallId();
            }
        }

        private string _cinemaInitials;
        public string CinemaInitials
        {
            get => _cinemaInitials;
            set
            {
                _cinemaInitials = value;
                UpdateHallId();
            }
        }

        public string HallId { get; private set; }

        public int CleaningTime { get; set; }

        private void UpdateHallId()
        {
            HallId = $"{HallNumber}{CinemaInitials}";
        }
    }
}
