using CinemaHallSimulation.Entity;
using CinemaHallSimulation.Helpers;
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
    public partial class Seans : Form
    {
        public Seans()
        {
            InitializeComponent();
        }

        private void Seans_Load(object sender, EventArgs e)
        {
            List<Hall> halls = HelperHall.GetHallList();
            comboBox1.DataSource = halls;
            comboBox1.ValueMember = "HallId";
            comboBox1.DisplayMember = "Name";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("İlk önce salon seçmeniz gerekmektedir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                groupBox1.Visible = true;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
                checkBox5.Enabled = true;
                button2.Enabled = true;
                List<Showtimes> showtimes = HelperShowtimes.GetShowtimesListByHallIdAndDate(Convert.ToInt32(comboBox1.SelectedValue), dateTimePicker1.Value);
                foreach (var item in showtimes)
                {

                    if (item.Clock == 1)
                    {
                        checkBox1.Checked = true;
                        checkBox1.Enabled = false;
                    }
                    if (item.Clock == 2)
                    {
                        checkBox2.Checked = true;
                        checkBox2.Enabled = false;
                    }
                    if (item.Clock == 3)
                    {
                        checkBox3.Checked = true;
                        checkBox3.Enabled = false;
                    }
                    if (item.Clock == 4)
                    {
                        checkBox4.Checked = true;
                        checkBox4.Enabled = false;
                    }
                    if (item.Clock == 5)
                    {
                        checkBox5.Checked = true;
                        checkBox5.Enabled = false;
                    }
                }
            }
            if (checkBox1.Checked && checkBox2.Checked && checkBox3.Checked && checkBox4.Checked && checkBox5.Checked)
            {
                button2.Enabled = false;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Enabled == true && checkBox1.Checked == true)
            {
                Showtimes showtimes = new Showtimes();
                showtimes.HallId = Convert.ToInt32(comboBox1.SelectedValue);
                showtimes.Clock = 1;
                showtimes.Date = dateTimePicker1.Value;
                var a = HelperShowtimes.ShowtimesCUD(showtimes, System.Data.Entity.EntityState.Added);
                CreateChairs(a);
                checkBox1.Enabled = false;
            }
            if (checkBox2.Enabled == true && checkBox2.Checked == true)
            {
                Showtimes showtimes = new Showtimes();
                showtimes.HallId = Convert.ToInt32(comboBox1.SelectedValue);
                showtimes.Clock = 2;
                showtimes.Date = dateTimePicker1.Value;
                var a = HelperShowtimes.ShowtimesCUD(showtimes, System.Data.Entity.EntityState.Added);
                CreateChairs(a);
                checkBox2.Enabled = false;
            }
            if (checkBox3.Enabled == true && checkBox3.Checked == true)
            {
                Showtimes showtimes = new Showtimes();
                showtimes.HallId = Convert.ToInt32(comboBox1.SelectedValue);
                showtimes.Clock = 3;
                showtimes.Date = dateTimePicker1.Value;
                var a = HelperShowtimes.ShowtimesCUD(showtimes, System.Data.Entity.EntityState.Added);
                CreateChairs(a);
                checkBox3.Enabled = false;
            }
            if (checkBox4.Enabled == true && checkBox4.Checked == true)
            {
                Showtimes showtimes = new Showtimes();
                showtimes.HallId = Convert.ToInt32(comboBox1.SelectedValue);
                showtimes.Clock = 4;
                showtimes.Date = dateTimePicker1.Value;
                var a = HelperShowtimes.ShowtimesCUD(showtimes, System.Data.Entity.EntityState.Added);
                CreateChairs(a);
                checkBox4.Enabled = false;
            }
            if (checkBox5.Enabled == true && checkBox5.Checked == true)
            {
                Showtimes showtimes = new Showtimes();
                showtimes.HallId = Convert.ToInt32(comboBox1.SelectedValue);
                showtimes.Clock = 5;
                showtimes.Date = dateTimePicker1.Value;
                var a = HelperShowtimes.ShowtimesCUD(showtimes, System.Data.Entity.EntityState.Added);
                CreateChairs(a);
                checkBox5.Enabled = false;
            }
            MessageBox.Show("Seans veya seanslar başarıyla eklendi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (checkBox1.Checked && checkBox2.Checked && checkBox3.Checked && checkBox4.Checked && checkBox5.Checked)
            {
                button2.Enabled = false;
            }
        }
        private void CreateChairs((Showtimes, bool) a)
        {
            char[] letters = new char[] { 'A', 'B', 'C', 'D', 'E', 'F' };
            char[] numbers = new char[] { '1', '2', '3', '4', '5' };
            int letterIndex = 0;
            int numberIndex = 0;
            for (int i = 1; i <= 30; i++)
            {
                if (i % 5 == 0)
                {
                    Chair chair = new Chair();
                    chair.Name = letters[letterIndex].ToString() + numbers[numberIndex].ToString();
                    chair.IsSold = false;
                    chair.ShowtimesId = a.Item1.ShowtimesId;
                    var b = HelperChair.ChairCUD(chair, System.Data.Entity.EntityState.Added);
                    letterIndex++;
                    numberIndex = 0;
                }
                else
                {
                    Chair chair = new Chair();
                    chair.Name = letters[letterIndex].ToString() + numbers[numberIndex].ToString();
                    chair.IsSold = false;
                    chair.ShowtimesId = a.Item1.ShowtimesId;
                    var b = HelperChair.ChairCUD(chair, System.Data.Entity.EntityState.Added);
                    numberIndex++;
                }
            }
        }
    }
}
