using CinemaHallSimulation.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaHallSimulation.Models
{
    class HallModel
    {
        public int HallId { get; set; }
        public string Name { get; set; }
        public Movie Movie { get; set; }
    }
}
