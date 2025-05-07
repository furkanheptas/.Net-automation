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
    public partial class atEkle : Form
    {
        private readonly FirebaseDBService _dbService;
        private readonly Random _random = new Random();
        public atEkle()
        {
            InitializeComponent();
            _dbService = new FirebaseDBService("https://atyarisibahis-default-rtdb.europe-west1.firebasedatabase.app/");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            admin adminForm = new admin();
            adminForm.Show();
            this.Hide();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string atAdi = textBox1.Text;
            if (!int.TryParse(textBox2.Text, out int hiz))
            {
                MessageBox.Show("Hız değeri sayısal olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(textBox2.Text, out int dayaniklilik))
            {
                MessageBox.Show("Dayanıklılık değeri sayısal olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(textBox3.Text, out int bakim))
            {
                MessageBox.Show("Bakım değeri sayısal olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(textBox4.Text, out int rng))
            {
                MessageBox.Show("RNG değeri sayısal olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            At yeniAt = new At
            {
                Isim = atAdi,
                Hiz = hiz,
                Dayaniklilik = dayaniklilik,
                Bakim = bakim,
                Rng = rng,
                Sahiplik = checkBox1.Checked
            };

            string yeniAtId = await _dbService.AddDataAsync("atlar", yeniAt);

            if (!string.IsNullOrEmpty(yeniAtId))
            {
                MessageBox.Show($"At başarıyla eklendi. ID: {yeniAtId}", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
            }
            else
            {
                MessageBox.Show("At eklenirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void atEkle_Load(object sender, EventArgs e)
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

        private async void button3_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox6.Text, out int eklenecekAdet) || eklenecekAdet <= 0)
            {
                MessageBox.Show("Lütfen geçerli bir sayı girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<Task<string>> eklemeTasklari = new List<Task<string>>();

            for (int i = 0; i < eklenecekAdet; i++)
            {
                string rastgeleAtAdi = RastgeleAtAdiOlustur();
                int rastgeleHiz = _random.Next(70, 100);
                int rastgeleDayaniklilik = _random.Next(70, 100);
                int rastgeleBakim = _random.Next(70, 100);
                int rastgeleRng = _random.Next(1, 101);

                At yeniAt = new At
                {
                    Isim = rastgeleAtAdi,
                    Hiz = rastgeleHiz,
                    Dayaniklilik = rastgeleDayaniklilik,
                    Bakim = rastgeleBakim,
                    Rng = rastgeleRng,
                    Sahiplik = false
                };
                eklemeTasklari.Add(_dbService.AddDataAsync("atlar", yeniAt));
            }

            await Task.WhenAll(eklemeTasklari);

            MessageBox.Show($"{eklenecekAdet} adet at başarıyla eklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

            textBox6.Clear();
        }

        private string RastgeleAtAdiOlustur()
        {
            string[] baslangic = { "Hızlı", "Kara", "Beyaz",  "Yıldırım", "Ateş", "Gümüş", "Altın", "Kızıl" };
            string[] son = { "Yele", "Göz", "Kanat", "Gölge", "Şimşek", "Rüzgar" };
            return baslangic[_random.Next(baslangic.Length)] + son[_random.Next(son.Length)] + _random.Next(100);
        }

    }
}
