using CinemaHallSimulation.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaHallSimulation.Models
{
    class TicketModel
    {
        public int TicketId { get; set; }
        public Movie Movie { get; set; }
        public Showtimes Showtimes { get; set; }
        public Chair Chair { get; set; }
        public string Type { get; set; }
        public User User { get; set; }
    }
}
