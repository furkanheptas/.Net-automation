using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AtYarisi.Form1; // Form1 sınıfındaki statik üyeleri doğrudan kullanmak için içe aktarılıyor

namespace AtYarisi
{
    public partial class ayarlar : Form
    {
        // Müzik açık/kapalı durumunu kontrol eden değişken
        public static bool acik = false;

        // PictureBox'ı küçültmek için kullanılan statik metot
        public static void pictureboxkucult(PictureBox pictureBox, int location, int size)
        {
            // PictureBox'ın konumunu ve boyutunu küçült
            pictureBox.Location = new Point(pictureBox.Location.X + location, pictureBox.Location.Y + location);
            pictureBox.Size = new Size(pictureBox.Size.Width - size, pictureBox.Size.Height - size);
        }

        // PictureBox'ı büyütmek için kullanılan statik metot
        public static void pictureboxbuyut(PictureBox pictureBox, int location, int size)
        {
            // PictureBox'ın konumunu ve boyutunu büyüt
            pictureBox.Location = new Point(pictureBox.Location.X - location, pictureBox.Location.Y - location);
            pictureBox.Size = new Size(pictureBox.Size.Width + size, pictureBox.Size.Height + size);
        }

        // PictureBox'ı yuvarlak hale getirmek için kullanılan statik metot
        public static void yuvarlat(PictureBox btn)
        {
            // Yuvarlak bir şekil oluştur ve PictureBox'ın bölgesine uygula
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, btn.Width, btn.Height);
            btn.Region = new Region(path);
        }

        // Formun yapıcı metodu
        public ayarlar()
        {
            InitializeComponent(); // Form bileşenlerini başlatır

            // Form kapatıldığında uygulamayı tamamen sonlandır
            this.FormClosed += (sender, e) => Application.Exit();
        }

        // "Geri Dön" butonuna tıklandığında ana forma geçiş yap
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(); // Yeni bir Form1 nesnesi oluştur
            form1.Show(); // Yeni formu göster
            this.Hide(); // Mevcut formu gizle
        }

        // Fare PictureBox1'in üzerine geldiğinde boyutunu büyüt
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureboxbuyut(pictureBox1, 3, 6);
        }

        // Fare PictureBox1'den ayrıldığında boyutunu küçült
        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureboxkucult(pictureBox1, 3, 6);
        }

        // PictureBox2'ye tıklandığında sesi artır
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form1.muzik.SesiArtir(); // Müzik sesini artır
        }

        // PictureBox1'e tıklandığında sesi azalt
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1.muzik.SesiAzalt(); // Müzik sesini azalt
        }

        // PictureBox3'e tıklandığında müziği durdur veya başlat
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (GlobalAyarlar.MuzikAcik) // Müzik açık mı kontrol et
            {
                Form1.muzik.Durdur(); // Müziği durdur
                pictureBox3.Image = Properties.Resources.mute; // İkonu "mute" olarak değiştir
            }
            else
            {
                GlobalAyarlar.MuzikAcik = true; // Müzik durumunu açık olarak ayarla
                Form1.muzik.Baslat(); // Müziği başlat
                pictureBox3.Image = Properties.Resources.unmute; // İkonu "unmute" olarak değiştir
            }
        }

        // Fare PictureBox3'ün üzerine geldiğinde boyutunu büyüt
        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureboxbuyut(pictureBox3, 3, 6);
        }

        // Fare PictureBox3'ten ayrıldığında boyutunu küçült
        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureboxkucult(pictureBox3, 3, 6);
        }

        // Fare PictureBox2'nin üzerine geldiğinde boyutunu büyüt
        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureboxbuyut(pictureBox2, 3, 6);
        }

        // Fare PictureBox2'den ayrıldığında boyutunu küçült
        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureboxkucult(pictureBox2, 3, 6);
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            Form1.boyutlama.butonbuyut(button1, 4, 10);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            Form1.boyutlama.butonkucult(button1, 4, 10);
        }
    }
}
