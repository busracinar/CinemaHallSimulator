using CinemaHallSimulation.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaHallSimulation.Helpers
{
    class HelperChair
    {
        public static (Chair, bool) ChairCUD(Chair chair, EntityState entityState)
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                c.Entry(chair).State = entityState;
                if (c.SaveChanges() > 0)
                {
                    return (chair, true);
                }
                else
                {
                    return (chair, false);
                }
            }
        }
        public static Chair GetChairById(int chairId)
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                return c.Chair.Find(chairId);
            }
        }
        public static List<Chair> GetChairsByShowtimesId(int showtimesId)
        {
            using (CinemaDbEntities c =new CinemaDbEntities())
            {
                return c.Chair.Where(x => x.ShowtimesId == showtimesId).ToList();
            }
        }
        public static Chair GetChairByShowtimesIdAndName(int showtimesId, string name)
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                return c.Chair.Where(x => x.ShowtimesId == showtimesId && x.Name == name).FirstOrDefault();
            }
        }

    }
}
