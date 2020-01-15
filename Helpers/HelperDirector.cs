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
    class HelperDirector
    {
        public static (Director, bool) DirectorCUD(Director director, EntityState entityState)
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                c.Entry(director).State = entityState;
                if (c.SaveChanges() > 0)
                {
                    return (director, true);
                }
                else
                {
                    return (director, false);
                }
            }
        }
        public static List<Director> GetDirectorList()
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                return c.Director.ToList();
            }
        }
        public static Director GetDirectorById(int directorId)
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                return c.Director.Find(directorId);
            }
        }
        public static List<DirectorModel> GetDirectorModelList()
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                List<DirectorModel> directorModels = new List<DirectorModel>();
                List<Director> directors = GetDirectorList();
                foreach (var item in directors)
                {
                    DirectorModel directorModel = new DirectorModel();
                    directorModel.DirectorId = item.DirectorId;
                    directorModel.Name = item.Name;
                    directorModel.Surname = item.Surname;
                    directorModel.FullName = item.Name + " " + item.Surname;
                    directorModels.Add(directorModel);
                }
                return directorModels;
            }
        }
    }
}
