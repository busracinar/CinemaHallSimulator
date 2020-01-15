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
    public partial class Kategori : Form
    {
        User user;
        public Kategori(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button1.Enabled = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            label3.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            groupBox3.Enabled = true;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Alan boş bırakılamaz. Düzenleyip tekrar deneyiniz.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Category category = HelperCategory.GetCategoryById(Convert.ToInt32(label3.Text));
                category.Name = textBox2.Text;
                var a = HelperCategory.CategoryCUD(category, System.Data.Entity.EntityState.Modified);
                if (a.Item2)
                {
                    RefreshTheDatabase();
                    textBox2.Clear();
                    label3.Text = null;
                    groupBox3.Enabled = false;
                    MessageBox.Show("Kategori düzenlendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Kategori düzenlenemedi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void RefreshTheDatabase()
        {
            dataGridView1.Rows.Clear();
            List<Category> categories = HelperCategory.GetCategoryList();
            foreach (var item in categories)
            {
                dataGridView1.Rows.Add(item.CategoryId, item.Name);
            }
        }

        private void Kategori_Load(object sender, EventArgs e)
        {
            if (user.Type == 1)
            {
                button1.Enabled = false;
                groupBox2.Enabled = false;
            }
            RefreshTheDatabase();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Alan boş bırakılamaz. Düzenleyip tekrar deneyiniz.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Category category = new Category();
                category.Name = textBox1.Text;
                var a = HelperCategory.CategoryCUD(category, System.Data.Entity.EntityState.Added);
                if (a.Item2)
                {
                    RefreshTheDatabase();
                    textBox1.Clear();
                    label3.Text = null;
                    MessageBox.Show("Kategori eklendi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Kategori eklenemedi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
