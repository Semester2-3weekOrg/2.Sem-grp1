using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovies.Models
{
    class Cinema
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<CinemaHall> Halls { get; set; }

        //Overriding ToString method for better readability in UI
        public override string ToString()
        {
            return $"{Name} {Halls}";
        }
    }
}
