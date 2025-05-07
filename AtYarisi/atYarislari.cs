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
    public partial class atYarislari : Form
    {
        private readonly FirebaseDBService _dbService;
        private List<Yaris> _yarislar;
        private readonly Random _random = new Random();
        public atYarislari()
        {
            InitializeComponent();
            this.FormClosed += (sender, e) => Application.Exit();
            _yarislar = new List<Yaris>();
            _dbService = new FirebaseDBService("https://atyarisibahis-default-rtdb.europe-west1.firebasedatabase.app/");
        }
        public class Yaris
        {
            public string Id { get; set; }
            public List<At> KatilanAtlar { get; set; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            basla basla = new basla();
            basla.Show();
            this.Hide();
        }

        private async void atYarislari_Load(object sender, EventArgs e)
        {
            int x = 800;
            int y = 600;

            this.Size = new System.Drawing.Size(x, y);

            int ekranGenisligi = Screen.PrimaryScreen.WorkingArea.Width;
            int ekranYuksekligi = Screen.PrimaryScreen.WorkingArea.Height;

            int baslangicX = (ekranGenisligi - this.Width) / 2;
            int baslangicY = (ekranYuksekligi - this.Height) / 2;

            this.Location = new System.Drawing.Point(baslangicX, baslangicY);

            await YarisOlusturVeListele();
        }

        private async Task YarisOlusturVeListele()
        {
            var tumAtlar = await _dbService.GetAllHorsesDictionaryAsync();
            if (tumAtlar.Count >= 3)
            {
                int yarisSayisi = _random.Next(2, tumAtlar.Count / 8 + 1);
                List<At> karistirilmisAtlar = tumAtlar.Values.OrderBy(x => _random.Next()).ToList();

                for (int i = 0; i < yarisSayisi; i++)
                {
                    Yaris yeniYaris = YarisOlustur(karistirilmisAtlar, i);
                    _yarislar.Add(yeniYaris);
                }
                listBox1.Items.Clear();
                for (int i = 0; i < _yarislar.Count; i++)
                {
                    string katilimciBilgileri = string.Join(", ", _yarislar[i].KatilanAtlar.Select(a => $"{a.Isim} (Hız: {a.Hiz}, Dayanıklılık: {a.Dayaniklilik})"));
                    listBox1.Items.Add($"Yarış {i + 1}: Katılımcılar: {katilimciBilgileri}");
                }
            }
            else
            {
                MessageBox.Show("Yeterli sayıda at bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
        }

        private Yaris YarisOlustur(List<At> karistirilmisAtlar, int yarisIndex)
        {
            int baslangicIndex = yarisIndex * 3;
            int alinacakSayi = Math.Min(3 + _random.Next(3), karistirilmisAtlar.Count - baslangicIndex);

            if (alinacakSayi <= 0)
            {
                alinacakSayi = Math.Min(3, karistirilmisAtlar.Count);
                baslangicIndex = Math.Max(0, karistirilmisAtlar.Count - alinacakSayi);
            }

            List<At> katilanAtlar = karistirilmisAtlar.Skip(baslangicIndex).Take(alinacakSayi).ToList();
            return new Yaris { Id = Guid.NewGuid().ToString(), KatilanAtlar = katilanAtlar };
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                Yaris secilenYaris = _yarislar[listBox1.SelectedIndex];
                dataGridView1.DataSource = secilenYaris.KatilanAtlar.Select(at => new
                {
                    Isim = at.Isim,
                    Hiz = at.Hiz,
                    Dayaniklilik = at.Dayaniklilik
                }).ToList();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string secilenAtIsim = dataGridView1.SelectedRows[0].Cells["Isim"].Value?.ToString();
                int secilenAtHiz = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Hiz"].Value);
                int secilenAtDayaniklilik = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Dayaniklilik"].Value);

                label4.Text = secilenAtIsim;
                label5.Text = secilenAtHiz.ToString();
                label6.Text = secilenAtDayaniklilik.ToString();
            }
        }
    }
}
