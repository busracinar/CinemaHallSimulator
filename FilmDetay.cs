using CinemaHallSimulation.Entity;
using CinemaHallSimulation.Helpers;
using CinemaHallSimulation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CinemaHallSimulation
{
    public partial class FilmDetay : Form
    {
        Movie movie;
        public FilmDetay(Movie movie)
        {
            InitializeComponent();
            this.movie = movie;
        }

        private void FilmDetay_Load(object sender, EventArgs e)
        {
            MovieModel movieModel = new MovieModel();
            movieModel.MovieId = movie.MovieId;
            movieModel.Name = movie.Name;
            movieModel.Category = HelperCategory.GetCategoryById(movie.CategoryId);
            movieModel.Director = HelperDirector.GetDirectorById(movie.DirectorId);
            movieModel.Description = movie.Description;
            movieModel.Banner = movie.Banner;
            movieModel.Minutes = movie.Minutes;
            label2.Text = movieModel.Name;
            label7.Text = movieModel.Category.Name;
            label8.Text = movieModel.Director.Name + " " + movieModel.Director.Surname;
            label9.Text = movieModel.Minutes.ToString()+ "dk";
            label10.Text = movieModel.Description;
            byte[] imageBytes = Convert.FromBase64String(movie.Banner.ToString());
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            pictureBox1.Image = Image.FromStream(ms, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
