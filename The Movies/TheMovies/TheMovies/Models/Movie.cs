/// <summary>
/// Represents a movie.
/// </summary>
namespace TheMovies.Models
{
    public class Movie
    {
        public int Id { get; set; } // Unique identifier for the movie
        public required string Title { get; set; } // Title of the movie
        public int? Length { get; set; } // Length of the movie in minutes, nullable to allow for unknown lengths
        public required Genre Genre { get; set; } // Genre of the movie, required to ensure every movie has a genre
        public string Instructor { get; set; }
        public DateOnly? PremiereDate { get; set; }

        ////Propfull
        //private int myVar;

        //public int MyProperty
        //{
        //    get { return myVar; }
        //    set { myVar = value; }
        //}
        //private string Id;

        //public string MyProperty
        //{
        //    get { return Id; }
        //    set { Id = value; }
        //}


        ////prop
        //public int MyProperty1 { get; set; }

        ////ctor
        //public Movie()
        //{

        //}


        //Overriding ToString method for better readability in UI
        public override string ToString()
        {
            return $"{Title} ({Length} min) - {Genre.Name} {Instructor} {PremiereDate}";
        }

        //Title = parts[0],
        //            Length = parts[1],
        //            Genre = parts[2],
        //            Instructor = parts[3],
        //            PremiereDate = parts[4]
    }
}
