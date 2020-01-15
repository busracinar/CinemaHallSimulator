using CinemaHallSimulation.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaHallSimulation.Helpers
{
    static class HelperUser
    {
        public static List<User> GetList()
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                return c.User.ToList();
            }
        }
        public static User GetUserById(int userId)
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                return c.User.Find(userId);
            }
        }
    }
}
