using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Database;
using Firebase.Database.Query;
using System.Media;
using System.Numerics;
using WMPLib;
using System.IO;

namespace AtYarisi
{
    public partial class Form1 : Form
    {
        private readonly FirebaseDBService _dbService; // Firebase veritabanı işlemleri için servis
        private static bool basladiMi = false; // Müzik çalma durumunu kontrol eden bayrak

        public Form1()
        {
            InitializeComponent();
            // Firebase veritabanı bağlantısını başlat
            _dbService = new FirebaseDBService("https://atyarisibahis-default-rtdb.europe-west1.firebasedatabase.app/");
            this.FormClosed += (sender, e) => Application.Exit();
        }

        // Global ayarlar sınıfı
        public static class GlobalAyarlar
        {
            public static bool MuzikAcik = true; // Müzik açık/kapalı durumu
        }

        // Müzik çalma işlemleri için sınıf
        public static class muzik
        {
            public static WindowsMediaPlayer player; // Müzik çalar nesnesi
            private static string dosyayolu; // Müzik dosyasının geçici yolu

            // Müzik başlatma işlemi
            public static void Baslat()
            {
                if (basladiMi || !GlobalAyarlar.MuzikAcik)
                    return;

                // Müzik dosyasını geçici bir konuma yaz
                dosyayolu = Path.Combine(Path.GetTempPath(), "jojo.mp3");
                using (var memoryStream = new MemoryStream())
                {
                    Properties.Resources.muzik.CopyTo(memoryStream);
                    File.WriteAllBytes(dosyayolu, memoryStream.ToArray());
                }

                // Müzik çaları başlat ve döngü modunda çal
                player = new WindowsMediaPlayer();
                player.URL = dosyayolu;
                player.settings.setMode("loop", true);
                player.settings.volume = 50; // Varsayılan ses seviyesi
                player.controls.play();
                basladiMi = true;
            }

            // Müzik sesini azalt
            public static void SesiAzalt()
            {
                if (basladiMi && player.settings.volume > 0)
                    player.settings.volume -= 10;
            }

            // Müzik sesini artır
            public static void SesiArtir()
            {
                if (basladiMi && player.settings.volume < 100)
                    player.settings.volume += 10;
            }

            // Müzik çalmayı durdur ve geçici dosyayı sil
            public static void Durdur()
            {
                if (basladiMi)
                {
                    player.controls.stop();
                    if (File.Exists(dosyayolu))
                    {
                        File.Delete(dosyayolu);
                    }
                    basladiMi = false;
                    GlobalAyarlar.MuzikAcik = false;
                }
            }
        }

        // Form yüklendiğinde müziği başlat
        private void Form1_Load(object sender, EventArgs e)
        {
            if (GlobalAyarlar.MuzikAcik)
                muzik.Baslat();
        }

        // Firebase'e veri ekleme ve okuma işlemi
        private async void button1_Click(object sender, EventArgs e)
        {
            // Firebase'e test verisi ekle
            string yazilanId = await _dbService.AddDataAsync("testVeri", new { Mesaj = "Deneme" });
            MessageBox.Show($"Veri başarıyla yazıldı. ID: {yazilanId}");

            // Eklenen veriyi Firebase'den oku
            var okunanVeri = await _dbService.ReadDataAsync<object>($"testVeri/{yazilanId}");
            if (okunanVeri != null)
            {
                Console.WriteLine($"Okunan Veri (ID: {yazilanId}): {okunanVeri}");
            }
            else
            {
                Console.WriteLine($"Okuma başarısız. ID: {yazilanId} bulunamadı.");
            }
        }

        // "Başla" butonuna tıklandığında yeni formu aç
        private void button2_Click(object sender, EventArgs e)
        {
            basla basla = new basla();
            basla.Show();
            this.Hide(); // Ana sayfayı gizle
        }

        // "Ayarlar" butonuna tıklandığında ayarlar formunu aç
        private void button3_Click(object sender, EventArgs e)
        {
            ayarlar ayarlar = new ayarlar();
            ayarlar.Show();
            this.Hide(); // Ana sayfayı gizle
        }

        // "Çıkış" butonuna tıklandığında uygulamayı kapat
        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // "Admin" butonuna tıklandığında admin formunu aç
        private void button5_Click(object sender, EventArgs e)
        {
            admin admin = new admin();
            admin.Show();
            this.Hide(); // Ana sayfayı gizle
        }

        // Buton üzerine gelindiğinde boyutunu büyüt
        private void button2_MouseEnter(object sender, EventArgs e)
        {
            boyutlama.butonbuyut(button2, 5, 10);
        }

        // Buton üzerinden çıkıldığında boyutunu küçült
        private void button2_MouseLeave(object sender, EventArgs e)
        {
            boyutlama.butonkucult(button2, 5, 10);
        }

        // Buton boyutlandırma işlemleri için sınıf
        public static class boyutlama
        {
            // Butonu küçült
            public static void butonkucult(Button button, int location, int size)
            {
                button.Location = new Point(button.Location.X + location, button.Location.Y + location);
                button.Size = new Size(button.Size.Width - size, button.Size.Height - size);
            }

            // Butonu büyüt
            public static void butonbuyut(Button button, int location, int size)
            {
                button.Location = new Point(button.Location.X - location, button.Location.Y - location);
                button.Size = new Size(button.Size.Width + size, button.Size.Height + size);
            }
        }

        // Diğer butonlar için MouseEnter ve MouseLeave olayları
        private void button5_MouseEnter(object sender, EventArgs e)
        {
            boyutlama.butonbuyut(button5, 5, 10);
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            boyutlama.butonkucult(button5, 5, 10);
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            boyutlama.butonbuyut(button3, 5, 10);
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            boyutlama.butonkucult(button3, 5, 10);
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            boyutlama.butonbuyut(button4, 5, 10);
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            boyutlama.butonkucult(button4, 5, 10);
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
