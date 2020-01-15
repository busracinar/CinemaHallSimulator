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
    class HelperMovie
    {
        public static (Movie, bool) MovieCUD(Movie movie, EntityState entityState)
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                c.Entry(movie).State = entityState;
                if (c.SaveChanges() > 0)
                {
                    return (movie, true);
                }
                else
                {
                    return (movie, false);
                }
            }
        }
        public static List<Movie> GetMovieList()
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                return c.Movie.ToList();
            }
        }
        public static List<MovieModel> GetMovieModelList()
        {
            using (CinemaDbEntities c = new CinemaDbEntities())
            {
                List<MovieModel> movieModels = new List<MovieModel>();
                List<Movie> movies = GetMovieList();
                foreach (var item in movies)
                {
                    MovieModel movieModel = new MovieModel();
                    movieModel.MovieId = item.MovieId;
                    movieModel.Name = item.Name;
                    movieModel.Category = HelperCategory.GetCategoryById(item.CategoryId);
                    movieModel.Director = HelperDirector.GetDirectorById(item.DirectorId);
                    movieModel.Description = item.Description;
                    movieModel.Banner = item.Banner;
                    movieModel.Minutes = item.Minutes;
                    movieModels.Add(movieModel);
                }
                return movieModels;
            }
        }
        public static Movie GetMovieById(int movieId)
        {
            using (CinemaDbEntities c=new CinemaDbEntities())
            {
                return c.Movie.Find(movieId);
            }
        }
    }
}
