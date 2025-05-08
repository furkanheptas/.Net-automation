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
        private readonly FirebaseDBService _dbService;
        private static bool basladiMi = false;
        public Form1()
        {
            InitializeComponent();
            _dbService = new FirebaseDBService("https://atyarisibahis-default-rtdb.europe-west1.firebasedatabase.app/");
          
        }
        public static class muzik
        {
            public static WindowsMediaPlayer player;
            private static string dosyayolu;

            public static void Baslat()
            {
                if (basladiMi)
                {
                    return;
                }
                dosyayolu = Path.Combine(Path.GetTempPath(), "jojo.mp3");
                using (var memoryStream = new MemoryStream())
                {
                    Properties.Resources.muzik.CopyTo(memoryStream);
                    File.WriteAllBytes(dosyayolu, memoryStream.ToArray());
                }
                
                player = new WindowsMediaPlayer();
                player.URL = dosyayolu;
                player.settings.setMode("loop", true);
                player.settings.volume = 50;
                player.controls.play();
                basladiMi = true;
            }
           

            public static void SesiAzalt()
            {
                if (basladiMi && player.settings.volume > 0)
                    player.settings.volume -= 10;
            }

            public static void SesiArtir()
            {
                if (basladiMi && player.settings.volume < 100)
                    player.settings.volume += 10;
            }

            public static int AnlikSes()
            {
                return basladiMi ? player.settings.volume : 0 ;
            }
            public static void Durdur()
            {
                if (basladiMi)
                {
                    player.controls.stop();
                    if (File.Exists(dosyayolu))
                    {
                        File.Delete(dosyayolu);

                        basladiMi = false;
                    }
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            muzik.Baslat();
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            string yazilanId = await _dbService.AddDataAsync("testVeri", new { Mesaj = "Deneme" });
            MessageBox.Show($"Veri başarıyla yazıldı. ID: {yazilanId}");

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
           private void button2_Click(object sender, EventArgs e)
        {
            basla basla = new basla();
            basla.Show();
            this.Hide();
         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ayarlar ayarlar = new ayarlar();
            ayarlar.Show();
            this.Hide();
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            admin admin = new admin();
            admin.Show();
            this.Hide();
         
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            boyutlama.butonbuyut(button2, 5, 10);
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            boyutlama.butonkucult(button2, 5, 10);
        }

      

    public static class boyutlama
        {
            public static void butonkucult(Button button, int location, int size)
            {
                button.Location = new Point(button.Location.X + location, button.Location.Y + location);
                button.Size = new Size(button.Size.Width - size, button.Size.Height - size);
            }
            public static void butonbuyut(Button button, int location, int size)
            {
                button.Location = new Point(button.Location.X - location, button.Location.Y - location);
                button.Size = new Size(button.Size.Width + size, button.Size.Height + size);
            }
        }
        private void button5_MouseEnter(object sender, EventArgs e)
        {
            boyutlama.butonbuyut(button5, 5, 10);
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            boyutlama.butonkucult(button5 , 5, 10);
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
