using CinemaHallSimulation.Entity;
using CinemaHallSimulation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaHallSimulation.Helpers
{
    class HelperHall
    {
        public static (Hall, bool) HallCUD(Hall hall, EntityState entityState)
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                c.Entry(hall).State = entityState;
                if (c.SaveChanges() > 0)
                {
                    return (hall, true);
                }
                else
                {
                    return (hall, false);
                }
            }
        }
        public static List<Hall> GetHallList()
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                return c.Hall.ToList();
            }
        }
        public static Hall GetHallById(int hallId)
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                return c.Hall.Find(hallId);
            }
        }
        public static List<HallModel> GetHallModelList()
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                List<HallModel> hallModels = new List<HallModel>();
                List<Hall> halls = GetHallList();
                foreach (var item in halls)
                {
                    HallModel hallModel = new HallModel();
                    hallModel.HallId = item.HallId;
                    hallModel.Name = item.Name;
                    hallModel.Movie = HelperMovie.GetMovieById(item.MovieId);
                    hallModels.Add(hallModel);
                }
                return hallModels;
            }
        }
    }
}
