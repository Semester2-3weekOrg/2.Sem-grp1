using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovies.Models
{
    internal class Movie
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int? Lenght { get; set; }

    }
}
