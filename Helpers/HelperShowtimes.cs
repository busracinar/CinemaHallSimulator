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
    class HelperShowtimes
    {
        public static (Showtimes, bool) ShowtimesCUD(Showtimes showtimes, EntityState entityState)
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                c.Entry(showtimes).State = entityState;
                if (c.SaveChanges() > 0)
                {
                    return (showtimes, true);
                }
                else
                {
                    return (showtimes, false);
                }
            }
        }
        public static List<Showtimes> GetShowtimesList()
        {
            using (CinemaDbEntities c=new CinemaDbEntities())
            {
                return c.Showtimes.ToList();
            }
        }
        public static Showtimes GetShowtimesById(int showtimesId)
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                return c.Showtimes.Find(showtimesId);
            }
        }
        public static List<Showtimes> GetShowtimesListByHallIdAndDate(int hallId, DateTime dateTime)
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                return c.Showtimes.Where(x => x.HallId == hallId && x.Date.Day==dateTime.Day && x.Date.Month == dateTime.Month && x.Date.Year == dateTime.Year).ToList();
            }
        }
        public static List<ShowtimesModel> GetShowtimesModelList()
        {
            using (CinemaDbEntities c=new CinemaDbEntities())
            {
                List<ShowtimesModel> showtimesModels = new List<ShowtimesModel>();
                List<Showtimes> showtimes = GetShowtimesList();
                foreach (var item in showtimes)
                {
                    ShowtimesModel showtimesModel = new ShowtimesModel();
                    showtimesModel.ShowtimesId = item.ShowtimesId;
                    showtimesModel.Hall = HelperHall.GetHallById(item.HallId);
                    if (item.Clock == 1)
                    {
                        showtimesModel.Clock = "09:00";
                    }
                    else if (item.Clock == 2)
                    {
                        showtimesModel.Clock = "12:00";
                    }
                    else if (item.Clock == 3)
                    {
                        showtimesModel.Clock = "15:00";
                    }
                    else if (item.Clock == 4)
                    {
                        showtimesModel.Clock = "18:00";
                    }
                    else if (item.Clock == 5)
                    {
                        showtimesModel.Clock = "21:00";
                    }
                    showtimesModel.Date = item.Date;
                    showtimesModels.Add(showtimesModel);
                }
                return showtimesModels;
            }
        }
        public static List<ShowtimesModel> GetShowtimesModelPartialList(List<Showtimes> showtimes)
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                List<ShowtimesModel> showtimesModels = new List<ShowtimesModel>();
                foreach (var item in showtimes)
                {
                    ShowtimesModel showtimesModel = new ShowtimesModel();
                    showtimesModel.ShowtimesId = item.ShowtimesId;
                    showtimesModel.Hall = HelperHall.GetHallById(item.HallId);
                    if (item.Clock == 1)
                    {
                        showtimesModel.Clock = "09:00";
                    }
                    else if (item.Clock == 2)
                    {
                        showtimesModel.Clock = "12:00";
                    }
                    else if (item.Clock == 3)
                    {
                        showtimesModel.Clock = "15:00";
                    }
                    else if (item.Clock == 4)
                    {
                        showtimesModel.Clock = "18:00";
                    }
                    else if (item.Clock == 5)
                    {
                        showtimesModel.Clock = "21:00";
                    }
                    showtimesModel.Date = item.Date;
                    showtimesModels.Add(showtimesModel);
                }
                return showtimesModels;
            }
        }
        public static Showtimes GetShowtimesByHallIdAndDateAndClock(int hallId, DateTime dateTime, string _clock)
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                int clock = 0;
                if (_clock == "09:00")
                {
                    clock = 1;
                }
                else if (_clock == "12:00")
                {
                    clock = 2;
                }
                else if (_clock == "15:00")
                {
                    clock = 3;
                }
                else if (_clock == "18:00")
                {
                    clock = 4;
                }
                else if (_clock == "21:00")
                {
                    clock = 5;
                }
                return c.Showtimes.Where(x => x.HallId == hallId && x.Date.Day == dateTime.Day && x.Date.Month == dateTime.Month && x.Date.Year == dateTime.Year && x.Clock == clock).FirstOrDefault();
            }
        }
    }
}
