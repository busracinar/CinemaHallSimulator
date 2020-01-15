using CinemaHallSimulation.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaHallSimulation.Helpers
{
    class HelperCategory
    {
        public static (Category, bool) CategoryCUD(Category category, EntityState entityState)
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                c.Entry(category).State = entityState;
                if (c.SaveChanges() > 0)
                {
                    return (category, true);
                }
                else
                {
                    return (category, false);
                }
            }
        }
        public static List<Category> GetCategoryList()
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                return c.Category.ToList();
            }
        }
        public static Category GetCategoryById(int categoryId)
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                return c.Category.Find(categoryId);
            }
        }
    }
}
