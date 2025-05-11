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

        // Admin formu yüklendiğinde ekran boyutlarını ve konumunu ayarla
        private void admin_Load(object sender, EventArgs e)
        {
            int x = 800; // Formun genişliği
            int y = 600; // Formun yüksekliği

            this.Size = new System.Drawing.Size(x, y); // Form boyutunu ayarla

            // Ekran çözünürlüğünü al
            int ekranGenisligi = Screen.PrimaryScreen.WorkingArea.Width;
            int ekranYuksekligi = Screen.PrimaryScreen.WorkingArea.Height;

            // Formu ekranın ortasına yerleştir
            int baslangicX = (ekranGenisligi - this.Width) / 2;
            int baslangicY = (ekranYuksekligi - this.Height) / 2;

            this.Location = new System.Drawing.Point(baslangicX, baslangicY);
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
    }
}
