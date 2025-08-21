namespace TheMovies.Models
{
    class Cinema
    {
        public int Id { get; set; }
        public required string CinemaName { get; set; }
        public required string CinemaInitials { get; set; }
        public List<CinemaHall> Halls { get; set; }

        //Overriding ToString method for better readability in UI
        public override string ToString()
        {
            return $"{CinemaName} {Halls}";
        }
    }
}
