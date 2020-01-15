using CinemaHallSimulation.Entity;
using CinemaHallSimulation.Helpers;
using CinemaHallSimulation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CinemaHallSimulation
{
    public partial class Film : Form
    {
        string messageBoxHeader = "Bilgilendirme";
        Movie movie;
        User user;
        public Film(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            string filePath = openFileDialog.FileName;
            string fileName = openFileDialog.SafeFileName;
            pictureBox1.ImageLocation = filePath;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox5.Text) || pictureBox1.Image == null)
            {
                MessageBox.Show("Alan boş bırakılamaz. Düzenleyip tekrar deneyiniz.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    byte[] imageBytes;
                    Movie movie = new Movie();
                    movie.Name = textBox1.Text;
                    movie.CategoryId = Convert.ToInt32(comboBox1.SelectedValue);
                    movie.DirectorId = Convert.ToInt32(comboBox2.SelectedValue);
                    movie.Minutes = Convert.ToInt32(textBox5.Text);
                    movie.Description = textBox2.Text;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        if (Path.GetExtension(pictureBox1.ImageLocation).ToUpper() == ".PNG")
                        {
                            pictureBox1.Image.Save(ms, ImageFormat.Png);
                        }

                        if (Path.GetExtension(pictureBox1.ImageLocation).ToUpper() == ".JPG")
                        {
                            pictureBox1.Image.Save(ms, ImageFormat.Png);
                        }
                        imageBytes = ms.ToArray();
                    }
                    movie.Banner = Convert.ToBase64String(imageBytes);
                    var a = HelperMovie.MovieCUD(movie, System.Data.Entity.EntityState.Added);
                    if (a.Item2)
                    {
                        textBox1.Clear();
                        textBox2.Clear();
                        comboBox1.Text = null;
                        comboBox2.Text = null;
                        pictureBox1.ImageLocation = null;
                        textBox5.Text = null;
                        RefreshTheDatabase();
                        MessageBox.Show("Film eklendi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Film eklenemedi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (FormatException exc)
                {
                    MessageBox.Show("Yanlış giriş yapıldı. Düzenleyip tekrar deneyin.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void Film_Load(object sender, EventArgs e)
        {
            if (user.Type == 1)
            {
                groupBox2.Enabled = false; 
                button1.Enabled = false;
            }
            RefreshTheDatabase();
            List<Category> categories = HelperCategory.GetCategoryList();
            comboBox1.DataSource = categories;
            comboBox1.ValueMember = "CategoryId";
            comboBox1.DisplayMember = "Name";
            comboBox4.DataSource = categories;
            comboBox4.ValueMember = "CategoryId";
            comboBox4.DisplayMember = "Name";
            List<DirectorModel> directorModels = HelperDirector.GetDirectorModelList();
            comboBox2.DataSource = directorModels;
            comboBox2.ValueMember = "DirectorId";
            comboBox2.DisplayMember = "FullName";
            comboBox3.DataSource = directorModels;
            comboBox3.ValueMember = "DirectorId";
            comboBox3.DisplayMember = "FullName";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (user.Type == 0)
            {
                movie = HelperMovie.GetMovieById(Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value));
                label13.Text = movie.MovieId.ToString();
                textBox4.Text = movie.Name;
                comboBox4.Text = HelperCategory.GetCategoryById(movie.CategoryId).Name;
                comboBox3.Text = HelperDirector.GetDirectorById(movie.DirectorId).Name;
                textBox6.Text = movie.Minutes.ToString();
                byte[] imageBytes = Convert.FromBase64String(movie.Banner);
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);
                pictureBox2.Image = Image.FromStream(ms, true);
                textBox3.Text = movie.Description;
                groupBox3.Enabled = true;
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox6.Text) || pictureBox2.Image == null)
            {
                MessageBox.Show("Alan boş bırakılamaz. Düzenleyip tekrar deneyiniz.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    movie = HelperMovie.GetMovieById(int.Parse(label13.Text));
                    movie.Name = textBox4.Text;
                    movie.CategoryId = Convert.ToInt32(comboBox4.SelectedValue);
                    movie.DirectorId = Convert.ToInt32(comboBox3.SelectedValue);
                    movie.Minutes = Convert.ToInt32(textBox6.Text);
                    movie.Description = textBox3.Text;
                    var a = HelperMovie.MovieCUD(movie, System.Data.Entity.EntityState.Modified);
                    if (a.Item2)
                    {
                        textBox4.Clear();
                        comboBox4.Text = null;
                        comboBox3.Text = null;
                        groupBox3.Enabled = false;
                        textBox3.Text = null;
                        label13.Text = null;
                        pictureBox2.ImageLocation = null;
                        textBox6.Text = null;
                        MessageBox.Show("Film düzenlendi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Film düzenlenemedi.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                catch (FormatException exc)
                {
                    MessageBox.Show("Yanlış giriş yapıldı. Düzenleyip tekrar deneyin.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        public void RefreshTheDatabase()
        {
            dataGridView1.Rows.Clear();
            List<MovieModel> movieModels = HelperMovie.GetMovieModelList();
            foreach (var item in movieModels)
            {
                dataGridView1.Rows.Add(item.MovieId, item.Name, item.Category.Name, item.Director.Name + " " + item.Director.Surname, item.Minutes, item.Description);
            }
            dataGridView1.ClearSelection();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            string filePath = openFileDialog.FileName;
            string fileName = openFileDialog.SafeFileName;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Movie movie = HelperMovie.GetMovieById(Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value));
                FilmDetay filmDetay = new FilmDetay(movie);
                filmDetay.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Önce film seçmeniz gerekmektedir.", messageBoxHeader, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
    }
}
