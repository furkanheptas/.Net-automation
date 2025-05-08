using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace AtYarisi
{
    public partial class atYarislari : Form
    {
        private readonly FirebaseDBService _dbService;
        private List<Yaris> _yarislar;
        private readonly Random _random = new Random();
        private Dictionary<string, decimal> _bahisOranlari = new Dictionary<string, decimal>();
        private decimal _kullaniciBakiyesi;
        private const string BakiyeDosyasi = "kullanici_bakiye.txt";
        private string _secilenAtIsim;
        private decimal _bahisMiktari;

        public atYarislari()
        {
            InitializeComponent();
            this.FormClosed += (sender, e) => Application.Exit();
            _yarislar = new List<Yaris>();
            _dbService = new FirebaseDBService("https://atyarisibahis-default-rtdb.europe-west1.firebasedatabase.app/");
            _kullaniciBakiyesi = DosyadanBakiyeYukle();
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
            GuncelleKullaniciBakiyesi();
        }

        private async Task YarisOlusturVeListele()
        {
            var tumAtlar = await _dbService.GetAllHorsesDictionaryAsync();
            if (tumAtlar.Count >= 3)
            {
                int yarisSayisi = _random.Next(2, tumAtlar.Count / 8 + 1);
                List<At> karistirilmisAtlar = tumAtlar.Values.OrderBy(x => _random.Next()).ToList();

                _yarislar.Clear();
                listBox1.Items.Clear();
                dataGridView1.DataSource = null;

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

                HesaplaBahisOranlari(secilenYaris.KatilanAtlar);
                GosterBahisOranlari();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                _secilenAtIsim = dataGridView1.SelectedRows[0].Cells["Isim"].Value?.ToString();
                int secilenAtHiz = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Hiz"].Value);
                float secilenAtDayaniklilik = (float)dataGridView1.SelectedRows[0].Cells["Dayaniklilik"].Value;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                Yaris secilenYaris = _yarislar[listBox1.SelectedIndex];
                if (BahisYap(secilenYaris))
                {
                    yaris yarisForm = new yaris(secilenYaris);
                    yarisForm.YarisSonlandi += async (kazananAtIsmi) =>
                    {
                        BahisSonucuHesapla(kazananAtIsmi);
                        GuncelleKullaniciBakiyesi();
                        DosyayaBakiyeKaydet(_kullaniciBakiyesi);
                        this.Show();
                        await YarisOlusturVeListele();
                    };
                    yarisForm.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Lütfen oynatmak için bir yarış seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private decimal Clamp(decimal value, decimal min, decimal max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        private void HesaplaBahisOranlari(List<At> katilanAtlar)
        {
            _bahisOranlari.Clear();
            Dictionary<string, decimal> atPuanlari = new Dictionary<string, decimal>();
            decimal toplamPuan = 0;

            foreach (var at in katilanAtlar)
            {
                decimal puan = at.Hiz * 1.0m + (decimal)at.Dayaniklilik * 0.7m + at.Bakim * 0.2m;
                atPuanlari[at.Isim] = puan;
                toplamPuan += puan;
            }

            foreach (var at in katilanAtlar)
            {
                decimal puan = atPuanlari[at.Isim];
                decimal kazanmaOlasiligi = puan / toplamPuan;


                var minOlasilik = atPuanlari.Values.Min() / toplamPuan;
                var maxOlasilik = atPuanlari.Values.Max() / toplamPuan;

                decimal oran = Map(kazanmaOlasiligi, maxOlasilik, minOlasilik, 1.2m, 10.0m);

                _bahisOranlari[at.Isim] = Math.Round(oran, 2);
            }
        }

        private decimal Map(decimal value, decimal fromSource, decimal toSource, decimal fromTarget, decimal toTarget)
        {
            if (fromSource == toSource)
                return (fromTarget + toTarget) / 2;

            return fromTarget + ((value - fromSource) / (toSource - fromSource)) * (toTarget - fromTarget);
        }



        private void GosterBahisOranlari()
        {
            string oranText = "Bahis Oranları:\n";
            foreach (var oran in _bahisOranlari)
            {
                oranText += $"{oran.Key}: {oran.Value:F2}\n";
            }
            label8.Text = oranText;
        }

        private bool BahisYap(Yaris secilenYaris)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bahis yapmak için bir at seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string bahisMiktariStr = textBox1.Text;
            if (string.IsNullOrWhiteSpace(bahisMiktariStr))
            {
                MessageBox.Show("Bahis miktarı girmediniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(bahisMiktariStr, NumberStyles.Float, CultureInfo.InvariantCulture, out _bahisMiktari) || _bahisMiktari <= 0)
            {
                MessageBox.Show("Geçersiz bahis miktarı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (_bahisMiktari > _kullaniciBakiyesi)
            {
                MessageBox.Show("Yetersiz bakiye.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!_bahisOranlari.TryGetValue(_secilenAtIsim, out decimal bahisOrani))
            {
                MessageBox.Show("Seçilen at için oran bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            _kullaniciBakiyesi -= _bahisMiktari;
            GuncelleKullaniciBakiyesi();
            return true;
        }

        private void BahisSonucuHesapla(string kazananAtIsmi)
        {
            if (_secilenAtIsim == kazananAtIsmi)
            {
                decimal bahisOrani = _bahisOranlari[kazananAtIsmi];
                decimal kazanc = _bahisMiktari * bahisOrani;
                _kullaniciBakiyesi += kazanc;
                MessageBox.Show($"Yarışı kazandınız! Kazanç: {kazanc:F2} TL", "Yarış Sonucu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Yarışı kaybettiniz.", "Yarış Sonucu", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void GuncelleKullaniciBakiyesi()
        {
            label7.Text = $"Bakiye: {_kullaniciBakiyesi:F2} TL";
        }

        private decimal DosyadanBakiyeYukle()
        {
            if (File.Exists(BakiyeDosyasi))
            {
                try
                {
                    string bakiyeStr = File.ReadAllText(BakiyeDosyasi);
                    if (decimal.TryParse(bakiyeStr, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal bakiye))
                    {
                        return bakiye;
                    }
                    else
                    {
                        MessageBox.Show("Geçersiz bakiye dosyası. Varsayılan bakiye yüklendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return 1000;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bakiye dosyası okunamadı. Varsayılan bakiye yüklendi. Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 1000;
                }
            }
            else
            {
                DosyayaBakiyeKaydet(1000);
                return 1000;
            }
        }

        private void DosyayaBakiyeKaydet(decimal bakiye)
        {
            try
            {
                File.WriteAllText(BakiyeDosyasi, bakiye.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bakiye dosyası yazılamadı. Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

