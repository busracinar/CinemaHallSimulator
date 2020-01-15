using CinemaHallSimulation.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaHallSimulation.Models
{
    class ShowtimesModel
    {
        public int ShowtimesId { get; set; }
        public Hall Hall { get; set; }
        public string Clock { get; set; }
        public System.DateTime Date { get; set; }
    }
}
