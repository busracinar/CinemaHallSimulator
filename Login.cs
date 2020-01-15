using CinemaHallSimulation.Entity;
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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<User> users = Helpers.HelperUser.GetList();
            foreach (var item in users)
            {
                if (item.UserName == textBox1.Text && item.Password != textBox2.Text)
                {
                    label3.Text = "Parolayı yanlış girdiniz.";
                }
                else if (item.UserName != textBox1.Text && item.Password == textBox2.Text)
                {
                    label3.Text = "Kullanıcı adını yanlış girdiniz.";
                }
                else if (item.UserName == textBox1.Text && item.Password == textBox2.Text)
                {
                    Anasayfa form2 = new Anasayfa(item);
                    this.Hide();
                    form2.Show();
                }
            }
            label3.Text = "Kullanıcı adını ve parolayı yanlış girdiniz.";
        }
    }
}
