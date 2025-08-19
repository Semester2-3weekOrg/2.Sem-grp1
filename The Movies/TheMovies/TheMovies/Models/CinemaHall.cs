using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMovies.Models
{
    class CinemaHall
    {
        public int Id { get; set; }
        public string HallId { get; set; } // HallNumber + CinemaInitials
        public string HallNumber { get; set; }
        public string CinemaInitials { get; set; }

        //Overriding ToString method for better readability in UI
        public override string ToString()
        {
            return $"{HallId}";
        }
    }
}
