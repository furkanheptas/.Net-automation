using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace AtYarisi
{
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();

            // Form kapatıldığında uygulamayı tamamen sonlandır
            this.FormClosed += (sender, e) => Application.Exit();
        }

        // "Geri Dön" butonuna tıklandığında ana forma (Form1) geçiş yap
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide(); // Admin formunu gizle
        }

        // "At Ekle" butonuna tıklandığında at ekleme formuna geçiş yap
        private void button3_Click(object sender, EventArgs e)
        {
            atEkle atEkle = new atEkle();
            atEkle.Show();
            this.Hide(); // Admin formunu gizle
        }

        // "At Düzenle" butonuna tıklandığında at düzenleme formuna geçiş yap
        private void button4_Click(object sender, EventArgs e)
        {
            atDuzenle atDuzenle = new atDuzenle();
            atDuzenle.Show();
            this.Hide(); // Admin formunu gizle
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            Form1.boyutlama.butonbuyut(button1, 4, 8);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            Form1.boyutlama.butonkucult(button1, 4, 8);
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            Form1.boyutlama.butonbuyut(button3, 4, 8);
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            Form1.boyutlama.butonkucult(button3, 4, 8);
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            Form1.boyutlama.butonbuyut(button4, 4, 8);
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            Form1.boyutlama.butonkucult(button4, 4, 8);
        }
    }
}
