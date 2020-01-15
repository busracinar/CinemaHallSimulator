using CinemaHallSimulation.Entity;
using CinemaHallSimulation.Helpers;
using CinemaHallSimulation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CinemaHallSimulation
{
    public partial class Salon : Form
    {
        User user;
        public Salon(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Alanlar boş bırakılamaz. Düzenleyip tekrar deneyiniz.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Hall hall = new Hall();
                hall.Name = textBox1.Text;
                hall.MovieId = Convert.ToInt32(comboBox1.SelectedValue);
                var a = HelperHall.HallCUD(hall, System.Data.Entity.EntityState.Added);
                if (a.Item2)
                {
                    RefreshTheDatabase();
                    textBox1.Clear();
                    comboBox2.Text = null;
                    MessageBox.Show("Salon eklendi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Salon eklenemedi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void RefreshTheDatabase()
        {
            dataGridView1.Rows.Clear();
            List<HallModel> hallModels = HelperHall.GetHallModelList();
            foreach (var item in hallModels)
            {
                dataGridView1.Rows.Add(item.HallId, item.Name, item.Movie.Name);
            }
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button2.Enabled = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void Salon_Load(object sender, EventArgs e)
        {
            if (user.Type == 1)
            {
                button2.Enabled = false;
                groupBox2.Enabled = false;
            }
            RefreshTheDatabase();
            List<Movie> movies = HelperMovie.GetMovieList();
            comboBox1.DataSource = movies;
            comboBox1.ValueMember = "MovieId";
            comboBox1.DisplayMember = "Name";
            comboBox2.DataSource = movies;
            comboBox2.ValueMember = "MovieId";
            comboBox2.DisplayMember = "Name";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            groupBox3.Enabled = true;
            label5.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Alanlar boş bırakılamaz. Düzenleyip tekrar deneyiniz.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                groupBox3.Enabled = false;
                Hall hall = HelperHall.GetHallById(Convert.ToInt32(label5.Text));
                hall.Name = textBox2.Text;
                hall.MovieId = Convert.ToInt32(comboBox2.SelectedValue);
                var a = HelperHall.HallCUD(hall, System.Data.Entity.EntityState.Modified);
                if (a.Item2)
                {
                    RefreshTheDatabase();
                    textBox1.Clear();
                    comboBox2.Text = null;
                    MessageBox.Show("Salon düzenlendi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Salon düzenlenemedi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void Salon_FormClosed(object sender, FormClosedEventArgs e)
        {   
            Anasayfa anasayfa = (Anasayfa)Application.OpenForms["Anasayfa"];
            anasayfa.RefreshHalls();
        }
    }
}
