using CinemaHallSimulation.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaHallSimulation.Models
{
    class MovieModel
    {
        public int MovieId { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public Director Director { get; set; }
        public string Description { get; set; }
        public string Banner { get; set; }
        public int Minutes { get; set; }
    }
}
