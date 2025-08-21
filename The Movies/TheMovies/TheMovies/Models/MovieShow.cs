namespace TheMovies.Models
{
    class MovieShow
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; }
        public TimeOnly PlayTime { get; set; }
        public DateOnly PlayDate { get; set; }
        public TimeSpan Commercial { get; set; }
        public TimeSpan Duration { get; set; }

        public DateTime PlayDateTime => PlayDate.ToDateTime(PlayTime);


    }


}
