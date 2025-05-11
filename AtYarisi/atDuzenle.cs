using Firebase.Database;
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
    public partial class atDuzenle : Form
    {
        private readonly FirebaseDBService _dbService; // Firebase veritabanı işlemleri için servis
        private Dictionary<string, At> _atlar; // Atların ID ve bilgilerini tutan sözlük
        private string _secilenAtId; // Seçilen atın ID'si

        // Firebase'den atları yükler ve DataGridView'e doldurur
        public async Task atYukle()
        {
            _atlar = await _dbService.GetAllHorsesDictionaryAsync(); // Tüm atları Firebase'den al
            dataGridView1.DataSource = _atlar.Select(kvp => new
            {
                ID = kvp.Key,
                Isim = kvp.Value.Isim,
                Hiz = kvp.Value.Hiz,
                Dayaniklilik = kvp.Value.Dayaniklilik,
                Bakim = kvp.Value.Bakim,
                Rng = kvp.Value.Rng,
                KullaniciyaAit = kvp.Value.Sahiplik
            }).ToList();

            // DataGridView ayarları
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // Formun yapıcı metodu
        public atDuzenle()
        {
            InitializeComponent();
            _dbService = new FirebaseDBService("https://atyarisibahis-default-rtdb.europe-west1.firebasedatabase.app/");
            _atlar = new Dictionary<string, At>();
            this.FormClosed += (sender, e) => Application.Exit();
        }

        // Form yüklendiğinde ekran boyutunu ve konumunu ayarla, atları yükle
        private async void atDuzenle_Load(object sender, EventArgs e)
        {
            int x = 800; // Formun genişliği
            int y = 600; // Formun yüksekliği

            this.Size = new System.Drawing.Size(x, y);

            // Ekran çözünürlüğünü al ve formu ekranın ortasına yerleştir
            int ekranGenisligi = Screen.PrimaryScreen.WorkingArea.Width;
            int ekranYuksekligi = Screen.PrimaryScreen.WorkingArea.Height;

            int baslangicX = (ekranGenisligi - this.Width) / 2;
            int baslangicY = (ekranYuksekligi - this.Height) / 2;

            this.Location = new System.Drawing.Point(baslangicX, baslangicY);

            // Atları yükle
            await atYukle();
        }

        // "Geri Dön" butonuna tıklandığında admin formuna geçiş yap
        private void button1_Click(object sender, EventArgs e)
        {
            admin adminForm = new admin();
            adminForm.Show();
            this.Hide(); // Mevcut formu gizle
        }

        // DataGridView'de bir satır seçildiğinde ilgili atın bilgilerini doldur
        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                _secilenAtId = dataGridView1.SelectedRows[0].Cells["ID"].Value?.ToString();
                if (!string.IsNullOrEmpty(_secilenAtId) && _atlar.ContainsKey(_secilenAtId))
                {
                    At secilenAt = _atlar[_secilenAtId];
                    textBox1.Text = secilenAt.Isim;
                    textBox2.Text = secilenAt.Hiz.ToString();
                    textBox3.Text = secilenAt.Dayaniklilik.ToString();
                    textBox4.Text = secilenAt.Bakim.ToString();
                    textBox5.Text = secilenAt.Rng.ToString();
                    checkBox1.Checked = secilenAt.Sahiplik;
                }
            }
        }

        // "Güncelle" butonuna tıklandığında seçilen atın bilgilerini güncelle
        private async void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_secilenAtId) && _atlar.ContainsKey(_secilenAtId))
            {
                // Girişlerin sayısal olup olmadığını kontrol et
                if (!int.TryParse(textBox2.Text, out int hiz) ||
                    !int.TryParse(textBox3.Text, out int dayaniklilik) ||
                    !int.TryParse(textBox4.Text, out int bakim) ||
                    !int.TryParse(textBox5.Text, out int rng))
                {
                    MessageBox.Show("Hız, Dayanıklılık, RNG ve bakım değerleri sayısal olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Güncellenmiş at bilgilerini oluştur
                At guncellenmisAt = new At
                {
                    Isim = textBox1.Text,
                    Hiz = hiz,
                    Dayaniklilik = dayaniklilik,
                    Bakim = bakim,
                    Rng = rng,
                    Sahiplik = checkBox1.Checked
                };

                // Firebase'de atı güncelle
                await _dbService.UpdateHorseAsync(_secilenAtId, guncellenmisAt);
                MessageBox.Show("At bilgileri güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Atları yeniden yükle
                await atYukle();
            }
            else
            {
                MessageBox.Show("Lütfen önce listeden bir at seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // "Sil" butonuna tıklandığında seçilen atı sil
        private async void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_secilenAtId))
            {
                // Silme onayı al
                if (MessageBox.Show("Seçilen atı silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    // Firebase'den atı sil
                    await _dbService.DeleteHorseAsync(_secilenAtId);
                    MessageBox.Show("At başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Atları yeniden yükle ve formu temizle
                    await atYukle();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    checkBox1.Checked = false;
                    _secilenAtId = null;
                }
            }
            else
            {
                MessageBox.Show("Lütfen önce listeden silmek istediğiniz bir at seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // DataGridView hücre tıklama olayı (şu anda kullanılmıyor)
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
