using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtYarisi
{
    public partial class basla : Form
    {
        public basla()
        {
            InitializeComponent();
            this.FormClosed += (sender, e) => Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            atYarislari atYarislari = new atYarislari();
            atYarislari.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            hesap hesap = new hesap();
            hesap.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            istatistikler istatistikler = new istatistikler();
            istatistikler.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            atlarim atlarim = new atlarim();
            atlarim.Show();
            this.Hide();
        }

        private void basla_Load(object sender, EventArgs e)
        {
           
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            Form1.boyutlama.butonbuyut(button2, 5, 10);
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            Form1.boyutlama.butonkucult(button2, 5, 10);
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            Form1.boyutlama.butonbuyut(button3, 5, 10);
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            Form1.boyutlama.butonkucult(button3, 5, 10);
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            Form1.boyutlama.butonbuyut(button4, 5, 10);
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            Form1.boyutlama.butonkucult(button4, 5, 10);
        }

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            Form1.boyutlama.butonbuyut(button5, 5, 10);
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            Form1.boyutlama.butonkucult(button5, 5, 10);
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            Form1.boyutlama.butonbuyut(button1, 5, 10);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            Form1.boyutlama.butonkucult(button1, 5, 10);
        }
    }
}
