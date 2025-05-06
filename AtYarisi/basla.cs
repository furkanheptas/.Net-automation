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
            int x = 800;
            int y = 600;

            this.Size = new System.Drawing.Size(x, y);

            int ekranGenisligi = Screen.PrimaryScreen.WorkingArea.Width;
            int ekranYuksekligi = Screen.PrimaryScreen.WorkingArea.Height;

            int baslangicX = (ekranGenisligi - this.Width) / 2;
            int baslangicY = (ekranYuksekligi - this.Height) / 2;

            this.Location = new System.Drawing.Point(baslangicX, baslangicY);
        }
    }
}
