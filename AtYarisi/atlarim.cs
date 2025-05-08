using Google.Apis.Requests.Parameters;
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
    public partial class atlarim : Form
    {
        public atlarim()
        {
            InitializeComponent();
            this.FormClosed += (sender, e) => Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            basla basla = new basla();
            basla.Show();
            this.Hide();
        }

        private void atlarim_Load(object sender, EventArgs e)
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

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            boyutlama.butonbuyut(button1, 5, 10);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            boyutlama.butonkucult(button1, 5, 10);
        }
    }
}
