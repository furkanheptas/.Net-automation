using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AtYarisi.Form1;

namespace AtYarisi
{
    public partial class ayarlar : Form
    {       
        public static bool acik = false;
        public static void pictureboxkucult(PictureBox pictureBox, int location, int size)
        {
            pictureBox.Location = new Point(pictureBox.Location.X + location, pictureBox.Location.Y + location);
            pictureBox.Size = new Size(pictureBox.Size.Width - size, pictureBox.Size.Height - size);
        }
        public static void pictureboxbuyut(PictureBox pictureBox, int location, int size)
        {
            pictureBox.Location = new Point(pictureBox.Location.X - location, pictureBox.Location.Y - location);
            pictureBox.Size = new Size(pictureBox.Size.Width + size, pictureBox.Size.Height + size);
        }
        public static void yuvarlat(PictureBox btn)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, btn.Width, btn.Height);
            btn.Region = new Region(path);
        }
        public ayarlar()
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

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureboxbuyut(pictureBox1, 3, 6);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureboxkucult(pictureBox1,3,6);
        }

        

        private void ayarlar_Load_1(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form1.muzik.SesiArtir();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1.muzik.SesiAzalt();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (GlobalAyarlar.MuzikAcik)
            {
                Form1.muzik.Durdur();
                pictureBox3.Image = Properties.Resources.mute;
            }
            else
            {
                GlobalAyarlar.MuzikAcik = true;
                Form1.muzik.Baslat();
                pictureBox3.Image = Properties.Resources.unmute;
            }
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureboxbuyut(pictureBox3, 3, 6);
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureboxkucult(pictureBox3, 3, 6);
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureboxbuyut(pictureBox2, 3, 6);
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureboxkucult(pictureBox2, 3, 6);
        }
    }
}
