using Google.Apis.Requests.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AtYarisi.Form1;

namespace AtYarisi
{
    public partial class atlarim : Form
    {
        private readonly FirebaseDBService _dbService; // Firebase veritabanı işlemleri için servis
        private Dictionary<string, At> _atlar; // Atların ID ve bilgilerini tutan sözlük
        private decimal _toplamPasifGelir = 0; // Seçilen atın pasif gelirini tutar

        public atlarim()
        {
            InitializeComponent();

            // Form kapatıldığında uygulamayı tamamen sonlandır
            this.FormClosed += (sender, e) => Application.Exit();

            // Firebase veritabanı bağlantısını başlat
            _dbService = new FirebaseDBService("https://atyarisibahis-default-rtdb.europe-west1.firebasedatabase.app/");
            _atlar = new Dictionary<string, At>();
        }

        // "Geri Dön" butonuna tıklandığında ana forma geçiş yap
        private void button1_Click(object sender, EventArgs e)
        {
            basla basla = new basla();
            basla.Show();
            this.Hide(); // Mevcut formu gizle
        }

        // Form yüklendiğinde ekran boyutunu ve konumunu ayarla, atları yükle
        private async void atlarim_Load(object sender, EventArgs e)
        {
           // Atları yükle
            await atYukle();
        }

        // "Geri Dön" butonuna fareyle gelindiğinde butonu büyüt
        private void button1_MouseEnter(object sender, EventArgs e)
        {
            boyutlama.butonbuyut(button1, 5, 10);
        }

        // "Geri Dön" butonundan fare ayrıldığında butonu küçült
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            boyutlama.butonkucult(button1, 5, 10);
        }

        // Firebase'den atları yükler ve DataGridView'e doldurur
        public async Task atYukle()
        {
            // Firebase'den tüm atları al
            _atlar = await _dbService.GetAllHorsesDictionaryAsync();

            // Kullanıcıya ait olan atları filtrele ve DataGridView'e ekle
            dataGridView1.DataSource = _atlar
                .Where(kvp => kvp.Value.Sahiplik == true)
                .Select(kvp => new
                {
                    ID = kvp.Key,
                    Isim = kvp.Value.Isim,
                    Hiz = kvp.Value.Hiz,
                    Dayaniklilik = kvp.Value.Dayaniklilik,
                    Bakim = kvp.Value.Bakim,
                    Rng = kvp.Value.Rng
                })
                .ToList();

            // ID sütununu gizle
            dataGridView1.Columns["ID"].Visible = false;

            // DataGridView ayarları
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // DataGridView'de bir hücreye tıklandığında seçilen atın bilgilerini göster
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Geçerli bir satır seçildiyse
            {
                // Seçilen atın ID'sini al
                string selectedAtId = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();

                // ID'ye göre at bilgilerini getir
                if (_atlar.TryGetValue(selectedAtId, out At selectedAt))
                {
                    // Pasif gelir hesapla ve ekranda göster
                    _toplamPasifGelir = (decimal)Math.Pow((selectedAt.Hiz * 10) + (selectedAt.Dayaniklilik * 5), 1.2);
                    label1.Text = $"Seçilen At: {selectedAt.Isim}\nPasif Gelir: {_toplamPasifGelir:F2} TL";
                }
                else
                {
                    label1.Text = "At bilgisi bulunamadı.";
                }
            }
        }
    }
}

