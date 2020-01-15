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
    public partial class Anasayfa : Form
    {
        User user;
        string buttonText = null;
        string checkedRadioButton = null;
        Movie movie;

        Showtimes showtimes = new Showtimes();
        public Anasayfa(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            RefreshTheTotalBalance();
            List<Hall> halls = HelperHall.GetHallList();
            comboBox1.DataSource = halls;
            comboBox1.ValueMember = "HallId";
            comboBox1.DisplayMember = "Name";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Film film = new Film(user);
            film.Show();
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            Salon salon = new Salon(user);
            salon.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Seans seans = new Seans();
            seans.Show();
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            Kategori kategori = new Kategori(user);
            kategori.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Yonetmen yonetmen = new Yonetmen(user);
            yonetmen.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            List<Showtimes> showtimesHelper = HelperShowtimes.GetShowtimesListByHallIdAndDate(Convert.ToInt32(comboBox1.SelectedValue), dateTimePicker1.Value);
            List<Showtimes> showtimes = showtimesHelper.OrderBy(x => x.Clock).ToList();
            List<ShowtimesModel> showtimesModels = HelperShowtimes.GetShowtimesModelPartialList(showtimes);
            foreach (var item in showtimesModels)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Text = item.Clock;
                radioButton.ForeColor = Color.White;
                radioButton.CheckedChanged += RadioButton_CheckedChanged;
                flowLayoutPanel1.Controls.Add(radioButton);
            }
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                checkedRadioButton = ((RadioButton)sender).Text;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox4.Visible = true;
            groupBox2.Visible = true;
            panel2.Visible = true;
            Hall hall = HelperHall.GetHallById(Convert.ToInt32(comboBox1.SelectedValue));
            movie = HelperMovie.GetMovieById(hall.MovieId);
            byte[] imageBytes = Convert.FromBase64String(movie.Banner.ToString());
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            pictureBox8.Image = Image.FromStream(ms, true);
            showtimes = HelperShowtimes.GetShowtimesByHallIdAndDateAndClock(Convert.ToInt32(comboBox1.SelectedValue), dateTimePicker1.Value, checkedRadioButton);
            RefreshTheSeats();
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            buttonText = ((Button)sender).Text;
        }

        private void RefreshTheSeats()
        {
            flowLayoutPanel2.Controls.Clear();
            List<Chair> chairs = HelperChair.GetChairsByShowtimesId(showtimes.ShowtimesId);
            foreach (var item in chairs)
            {
                Button button = new Button();
                button.ForeColor = Color.White;
                button.Text = item.Name;
                button.TextAlign = ContentAlignment.BottomCenter;
                button.Size = new Size(75, 75);
                button.Margin = new Padding(0, 0, 7, 5);
                button.Click += new EventHandler(NewButton_Click);
                button.Image = ((System.Drawing.Image)(Properties.Resources.seat2));
                button.ImageAlign = ContentAlignment.TopCenter;
                if (item.IsSold == false)
                {
                    button.BackColor = Color.Green;
                }
                else
                {
                    button.BackColor = Color.Red;
                }
                if (item.Name == "C1" || item.Name == "C2" || item.Name == "C3" || item.Name == "C4" || item.Name == "C5")
                {
                    button.Margin = new Padding(0, 0, 7, 23);
                }
                flowLayoutPanel2.Controls.Add(button);
            }
        }

        private void BtnBiletSat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(buttonText))
            {
                MessageBox.Show("İlk önce koltuk seçmeniz gerekmektedir!", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Chair chair = HelperChair.GetChairByShowtimesIdAndName(showtimes.ShowtimesId, buttonText);
                if (chair.IsSold == true)
                {
                    MessageBox.Show("Bu koltuk daha önce satılmış. Lütfen başka bir koltuk seçiniz.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (radioButton1.Checked == true && radioButton2.Checked == false)
                    {
                        chair.IsSold = true;
                        var b = HelperChair.ChairCUD(chair, System.Data.Entity.EntityState.Modified);
                        Ticket ticket = new Ticket();
                        ticket.ShowtimesId = showtimes.ShowtimesId;
                        ticket.ChairId = chair.ChairId;
                        ticket.Type = 0;
                        ticket.UserId = user.UserId;
                        var a = HelperTicket.TicketCUD(ticket, System.Data.Entity.EntityState.Added);
                        if (a.Item2)
                        {
                            RefreshTheTotalBalance();
                            RefreshTheSeats();
                            MessageBox.Show($"Salonunun {buttonText} koltuğu satılmıştır.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"Koltuk satılamamıştır.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else if (radioButton1.Checked == false && radioButton2.Checked == true)
                    {
                        chair.IsSold = true;
                        var b = HelperChair.ChairCUD(chair, System.Data.Entity.EntityState.Modified);
                        Ticket ticket = new Ticket();
                        ticket.ShowtimesId = showtimes.ShowtimesId;
                        ticket.ChairId = chair.ChairId;
                        ticket.Type = 1;
                        ticket.UserId = user.UserId;
                        var a = HelperTicket.TicketCUD(ticket, System.Data.Entity.EntityState.Added);
                        if (a.Item2)
                        {
                            RefreshTheTotalBalance();
                            RefreshTheSeats();
                            MessageBox.Show($"Salonunun {buttonText} koltuğu satılmıştır.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"Koltuk satılamamıştır.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("İlk önce bilet tipini seçmeniz gerekmektedir!", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnBiletIptal_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(buttonText))
            {
                MessageBox.Show("İlk önce koltuk seçmeniz gerekmektedir!", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Chair chair = HelperChair.GetChairByShowtimesIdAndName(showtimes.ShowtimesId, buttonText);
                if (chair.IsSold == false)
                {
                    MessageBox.Show("Bu koltuk daha önce satılmamış. Satılmamış bir koltuğun biletini iptal edemezsiniz!", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    chair.IsSold = false;
                    var a = HelperChair.ChairCUD(chair, System.Data.Entity.EntityState.Modified);
                    Ticket ticket = HelperTicket.GetTicketByShowtimesIdAndChairId(showtimes.ShowtimesId, chair.ChairId);
                    var b = HelperTicket.TicketCUD(ticket, System.Data.Entity.EntityState.Deleted);
                    RefreshTheSeats();
                    RefreshTheTotalBalance();
                }
            }
        }

        private void PictureBox7_MouseEnter(object sender, EventArgs e)
        {
            pictureBox7.BackColor = Color.White;
        }

        private void PictureBox7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.BackColor = Color.DarkViolet;
        }

        private void PictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.White;
        }

        private void PictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.DarkViolet;
        }

        private void PictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.White;
        }

        private void PictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.DarkViolet;
        }

        private void PictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.White;
        }

        private void PictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.DarkViolet;
        }

        private void PictureBox4_MouseEnter(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.White;
        }

        private void PictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.DarkViolet;
        }

        private void PictureBox6_MouseEnter(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.White;
        }

        private void PictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.DarkViolet;
        }

        public void RefreshTheTotalBalance()
        {
            int totalBalance = 0;
            List<Ticket> tickets = HelperTicket.GetTicketList();
            foreach (var item in tickets)
            {
                if (item.Type == 0)
                {
                    totalBalance += 20;
                }
                else if(item.Type == 1)
                {
                    totalBalance += 15;
                }
            }
            label10.Text = $"BAKİYE : {totalBalance}";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FilmDetay filmDetay = new FilmDetay(movie);
            filmDetay.Show();
        }
        public  void RefreshHalls()
        {
            List<Hall> halls = HelperHall.GetHallList();
            comboBox1.DataSource = halls;
            comboBox1.ValueMember = "HallId";
            comboBox1.DisplayMember = "Name";
        }
    }
}
