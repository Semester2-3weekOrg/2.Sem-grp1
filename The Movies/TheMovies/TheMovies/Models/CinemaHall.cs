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

        public string HallId { get; set; }

        public int CleaningTime = 15;

        private void UpdateHallId()
        {
            HallId = $"{HallNumber}{CinemaInitials}";
        }

        public int Seats { get; set; }
        public List<MovieShow> Shows { get; set; } = new();
    }
}
