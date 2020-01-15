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
    public partial class Yonetmen : Form
    {
        User user;
        public Yonetmen(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Alanlar boş bırakılamaz. Düzenleyip tekrar deneyiniz.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Director director = new Director();
                director.Name = textBox1.Text;
                director.Surname = textBox3.Text;
                var a = HelperDirector.DirectorCUD(director, System.Data.Entity.EntityState.Added);
                if (a.Item2)
                {
                    RefreshTheDatabase();
                    textBox1.Clear();
                    textBox3.Clear();
                    MessageBox.Show("Yönetmen eklendi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Yönetmen eklenemedi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void RefreshTheDatabase()
        {
            dataGridView1.Rows.Clear();
            List<Director> directors = HelperDirector.GetDirectorList();
            foreach (var item in directors)
            {
                dataGridView1.Rows.Add(item.DirectorId, item.Name, item.Surname);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox3.Enabled = true;
            label3.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            textBox4.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Alanlar boş bırakılamaz. Düzenleyip tekrar deneyiniz.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Director director = HelperDirector.GetDirectorById(int.Parse(label3.Text));
                director.Name = textBox4.Text;
                director.Surname = textBox2.Text;
                var a = HelperDirector.DirectorCUD(director, System.Data.Entity.EntityState.Modified);
                if (a.Item2)
                {
                    RefreshTheDatabase();
                    textBox4.Clear();
                    textBox2.Clear();
                    label3.Text = null;
                    groupBox3.Enabled = false;
                    MessageBox.Show("Yönetmen düzenlendi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Yönetmen düzenlenemedi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void Yonetmen_Load(object sender, EventArgs e)
        {
            if (user.Type == 1)
            {
                groupBox2.Enabled = false;
                button1.Enabled = false;
            }
            RefreshTheDatabase();
        }
    }
}
