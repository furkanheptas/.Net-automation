// Gerekli kütüphaneler
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
using static AtYarisi.istatistik;
using System.Media;
using static AtYarisi.Form1;

namespace AtYarisi
{
    public partial class atYarislari : Form
    {
        // Firebase servisinden veri çekmek için kullanılan servis sınıfı
        private readonly FirebaseDBService _dbService;

        // Yarışları tutan liste
        private List<Yaris> _yarislar;

        // Rastgele sayı üretmek için kullanılan nesne
        private readonly Random _random = new Random();

        // Her at için bahis oranları
        private Dictionary<string, decimal> _bahisOranlari = new Dictionary<string, decimal>();

        // Kullanıcının mevcut bakiyesi
        private decimal _kullaniciBakiyesi;

        // Kullanıcının seçtiği at ismi
        private string _secilenAtIsim;

        // Kullanıcının bahis miktarı
        private decimal _bahisMiktari;

        // Bakiye dosyasından okuma/yazma işlemleri için kullanılan sınıf
        private readonly hesapIslemleri _hesapIslemleri = new hesapIslemleri();

        public atYarislari()
        {
            InitializeComponent();

            // Form kapandığında uygulamayı tamamen kapat
            this.FormClosed += (sender, e) => Application.Exit();

            // Yarış listesi oluştur
            _yarislar = new List<Yaris>();

            // Firebase bağlantısı başlat
            _dbService = new FirebaseDBService("https://atyarisibahis-default-rtdb.europe-west1.firebasedatabase.app/");

            // Dosyadan mevcut kullanıcı bakiyesini yükle
            _kullaniciBakiyesi = _hesapIslemleri.DosyadanBakiyeYukle();
        }

        // Yaris sınıfı: her yarış bir ID'ye ve at listesine sahip
        public class Yaris
        {
            public string Id { get; set; }
            public List<At> KatilanAtlar { get; set; }
        }

        // "Başla" butonuna tıklanınca başka forma geçiş yapar
        private void button1_Click(object sender, EventArgs e)
        {
            basla basla = new basla();
            basla.Show();
            this.Hide();
        }

        // Form yüklendiğinde ekranı ortalar ve yarışları oluşturur
        private async void atYarislari_Load(object sender, EventArgs e)
        {
            // Yarışları oluştur ve listele
            await YarisOlusturVeListele();

            // Kullanıcı bakiyesini ekranda göster
            GuncelleKullaniciBakiyesi();
        }

        // Veritabanından atları çekerek rastgele yarışlar oluşturur ve arayüzde listeler
        private async Task YarisOlusturVeListele()
        {
            var tumAtlar = await _dbService.GetAllHorsesDictionaryAsync(); // Tüm atları veritabanından al

            if (tumAtlar.Count >= 3) // En az 3 at varsa yarış oluşturulabilir
            {
                int maxYarisSayisi = Math.Min(10, tumAtlar.Count / 3); // En fazla 10 yarış, her biri için minimum 3 at şartı
                _yarislar.Clear(); // Önceki yarış listesini temizle
                listBox1.Items.Clear(); // Liste kutusunu temizle
                dataGridView1.DataSource = null; // Önceki tabloyu temizle

                // Belirlenen sayıda rastgele yarış oluştur
                for (int i = 0; i < maxYarisSayisi; i++)
                {
                    Yaris yeniYaris = YarisOlustur(tumAtlar.Values.ToList()); // Rastgele yarış oluştur
                    _yarislar.Add(yeniYaris); // Yarışı listeye ekle
                }

                // Yarışları arayüzde listele
                listBox1.Items.Clear();
                for (int i = 0; i < _yarislar.Count; i++)
                {
                    string katilimciBilgileri = string.Join(", ", _yarislar[i].KatilanAtlar
                        .Select(a => $"{a.Isim} (Hız: {a.Hiz}, Dayanıklılık: {a.Dayaniklilik})")); // At bilgilerini hazırla

                    listBox1.Items.Add($"Yarış {i + 1}: Katılımcılar: {katilimciBilgileri}"); // Liste kutusuna ekle
                }
            }
            else
            {
                // Yeterli at yoksa uyarı ver
                MessageBox.Show("Yeterli sayıda at bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close(); // Pencereyi kapat
            }
        }

        // Tek bir yarış için 3 ila 5 adet rastgele at seçip yarış nesnesi oluşturur
        private Yaris YarisOlustur(List<At> tumAtlar)
        {
            int alinacakSayi = _random.Next(3, 6); // 3 ile 5 arasında rastgele katılımcı sayısı belirle

            // Atları rastgele sırala ve belirlenen sayıda seç
            List<At> secilenAtlar = tumAtlar.OrderBy(a => _random.Next()).Take(alinacakSayi).ToList();

            // Yeni yarış nesnesi oluştur ve seçilen atları ekle
            return new Yaris
            {
                Id = Guid.NewGuid().ToString(), // Her yarış için benzersiz ID
                KatilanAtlar = secilenAtlar
            };
        }

        public static class yarismuzik
        {
            // Yarış müziğini başlatmak için kullanılan metot
            public static void Baslat()
            {
                try
                {
                    // Resources.yarismuzigi içindeki ses verisini kullanarak bir SoundPlayer nesnesi oluşturur.
                    // MemoryStream, ses verisini bellekte saklamak için kullanılır.
                    SoundPlayer yarismuzigi = new SoundPlayer(new MemoryStream(Properties.Resources.yarismuzigi));
                    // Yarış müziğini çalmaya başlar.
                    yarismuzigi.Play();
                }
                catch (Exception ex)
                {
                    // Eğer bir hata oluşursa, kullanıcıya bir hata mesajı gösterir.
                    MessageBox.Show("Yarış müziği çalınamadı başlat: " + ex.Message);
                }
            }

            // Yarış müziğini durdurmak için kullanılan metot
            public static void Durdur()
            {
                try
                {
                    // Resources.yarismuzigi içindeki ses verisini kullanarak bir SoundPlayer nesnesi oluşturur.
                    SoundPlayer yarismuzigi = new SoundPlayer(new MemoryStream(Properties.Resources.yarismuzigi));
                    // Yarış müziğini durdurur.
                    yarismuzigi.Stop();
                    // Form1 sınıfındaki müzik nesnesinin Baslat metodunu çağırır. (Ana müziği tekrar başlatır)
                    Form1.muzik.Baslat();
                    // GlobalAyarlar sınıfındaki MuzikAcik özelliğini true olarak ayarlar.
                    GlobalAyarlar.MuzikAcik = true;
                }
                catch (Exception ex)
                {
                    // Eğer bir hata oluşursa, kullanıcıya bir hata mesajı gösterir.
                    MessageBox.Show("Yarış müziği çalınamadı durdur: " + ex.Message);
                }
            }
        }


        // Yarış seçilince atları grid'e bas ve oranları göster
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

        // Grid'de at seçilince isim değişkenini al
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                _secilenAtIsim = dataGridView1.SelectedRows[0].Cells["Isim"].Value?.ToString();
            }
        }

        // "Yarışı Oynat" butonuna basınca bahis yapılır ve yarış başlar
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                Yaris secilenYaris = _yarislar[listBox1.SelectedIndex];
                if (BahisYap(secilenYaris))
                {
                    yaris yarisForm = new yaris(secilenYaris);

                    // Yarış bittiğinde bahis sonucu hesaplanır
                    yarisForm.YarisSonlandi += async (kazananAtIsmi) =>
                    {
                        BahisSonucuHesapla(kazananAtIsmi);
                        GuncelleKullaniciBakiyesi();
                        _hesapIslemleri.DosyayaBakiyeKaydet(_kullaniciBakiyesi);
                        this.Show();
                        await YarisOlusturVeListele();
                    };
                    Form1.muzik.Durdur();
                    yarisForm.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Lütfen oynatmak için bir yarış seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Değer sınırlandırıcı fonksiyon
        private decimal Clamp(decimal value, decimal min, decimal max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        // Her at için bahis oranı hesaplama fonksiyonu
        private void HesaplaBahisOranlari(List<At> katilanAtlar)
        {
            _bahisOranlari.Clear();

            var puanliAtlar = katilanAtlar
                .Select(at => new
                {
                    At = at,
                    Puan = at.Hiz * 1.0m + (decimal)at.Dayaniklilik * 0.7m // Gizli formül
                })
                .OrderByDescending(x => x.Puan)
                .ToList();

            decimal minOran = 1.1m;
            decimal maxOran = 4.5m;
            decimal maxPuan = puanliAtlar.First().Puan;
            decimal minPuan = puanliAtlar.Last().Puan;
            decimal puanAraligi = maxPuan - minPuan;

            HashSet<decimal> benzersizOranlar = new HashSet<decimal>();

            for (int i = 0; i < puanliAtlar.Count; i++)
            {
                var at = puanliAtlar[i].At;

                // Normalize puanı hesapla
                decimal normalizedPuan = puanAraligi > 0
                    ? (puanliAtlar[i].Puan - minPuan) / puanAraligi
                    : 0.5m;

                // Oranı puana göre ters orantılı hesapla
                decimal baseOran = Map(normalizedPuan, 0, 1, maxOran, minOran);

                // Küçük varyasyon ekle
                decimal varyasyon = (decimal)_random.NextDouble() * 0.3m - 0.15m;
                baseOran *= (1 + varyasyon);

                // Aynı oran olmasın diye arttır
                while (benzersizOranlar.Contains(Math.Round(baseOran, 2)))
                {
                    baseOran += 0.01m;
                }

                baseOran = Clamp(baseOran, minOran, maxOran);
                baseOran = Math.Round(baseOran, 2);

                benzersizOranlar.Add(baseOran);
                _bahisOranlari[at.Isim] = baseOran;
            }
        }

        // Bir değeri başka aralığa dönüştüren fonksiyon
        private decimal Map(decimal value, decimal fromMin, decimal fromMax, decimal toMin, decimal toMax)
        {
            if (fromMax - fromMin == 0)
                return toMin;

            return toMin + (value - fromMin) * (toMax - toMin) / (fromMax - fromMin);
        }

        // Label'da oranları göster
        private void GosterBahisOranlari()
        {
            string oranText = "Bahis Oranları:\n";
            foreach (var oran in _bahisOranlari)
            {
                oranText += $"{oran.Key}: {oran.Value:F2}\n";
            }
            label8.Text = oranText;
        }

        // Bahis yapma işlemi
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

            // Bahis ücreti kullanıcı bakiyesinden düşülür
            _kullaniciBakiyesi -= _bahisMiktari;
            GuncelleKullaniciBakiyesi();
            return true;
        }

        // Yarış sonucunda bahis kazanıldı mı kontrolü
        private async void BahisSonucuHesapla(string kazananAtIsmi)
        {
            // İstatistik sınıfındaki ToplamBahisSayisi özelliğini bir artırır.
            istatistik.Istatistik.ToplamBahisSayisi++;

            // Eğer kullanıcının seçtiği at kazandıysa
            if (_secilenAtIsim == kazananAtIsmi)
            {
                // Kazanan atın bahis oranını alır.
                decimal bahisOrani = _bahisOranlari[kazananAtIsmi];
                // Kazanılan miktarı hesaplar.
                decimal kazanc = _bahisMiktari * bahisOrani;
                // Kullanıcının bakiyesine kazancı ekler.
                _kullaniciBakiyesi += kazanc;

                try
                {
                    // Galibiyet sesini çalmak için bir SoundPlayer nesnesi oluşturur.
                    SoundPlayer galibiyet = new SoundPlayer(new MemoryStream(Properties.Resources.galibiyet));
                    // Galibiyet sesini çalar.
                    galibiyet.Play();

                }
                catch (Exception ex)
                {
                    // Eğer ses dosyası çalınamazsa, kullanıcıya bir hata mesajı gösterir.
                    MessageBox.Show("Ses dosyası çalınamadı: " + ex.Message);
                }
                // Kullanıcıya kazancını bildiren bir mesaj gösterir.
                MessageBox.Show($"Yarışı kazandınız! Kazanç: {kazanc:F2} TL", "Yarış Sonucu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Form1 sınıfındaki müzik nesnesinin Baslat metodunu çağırır. (Ana müziği tekrar başlatır)
                Form1.muzik.Baslat();



                // İstatistik sınıfındaki KazanilanBahisSayisi özelliğini bir artırır.
                istatistik.Istatistik.KazanilanBahisSayisi++;
                // İstatistik sınıfındaki ToplamKazanilanPara özelliğine kazancı ekler.
                istatistik.Istatistik.ToplamKazanilanPara += kazanc;
            }
            // Eğer kullanıcı kaybettiyse
            else
            {
                try
                {
                    // Mağlubiyet sesini çalmak için bir SoundPlayer nesnesi oluşturur.
                    SoundPlayer maglubiyet = new SoundPlayer(new MemoryStream(Properties.Resources.maglubiyet));
                    // Mağlubiyet sesini çalar.
                    maglubiyet.Play();
                }
                catch (Exception ex)
                {
                    // Eğer ses dosyası çalınamazsa, kullanıcıya bir hata mesajı gösterir.
                    MessageBox.Show("Ses dosyası çalınamadı: " + ex.Message);
                }
                // Kullanıcıya kaybettiğini bildiren bir mesaj gösterir.
                MessageBox.Show("Yarışı kaybettiniz.", "Yarış Sonucu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // İstatistik sınıfındaki ToplamKaybedilenPara özelliğine kaybedilen bahis miktarını ekler.
                istatistik.Istatistik.ToplamKaybedilenPara += _bahisMiktari;
                // Form1 sınıfındaki müzik nesnesinin Baslat metodunu çağırır. (Ana müziği tekrar başlatır)
                Form1.muzik.Baslat();


            }
        }

        // Label'daki bakiye bilgisini günceller
        private void GuncelleKullaniciBakiyesi()
        {
            label7.Text = $"Bakiye: {_kullaniciBakiyesi:F2} TL";
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            Form1.boyutlama.butonbuyut(button2, 5, 10);
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            Form1.boyutlama.butonkucult(button2, 5, 10);
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            Form1.boyutlama.butonbuyut(button1, 3, 6);    
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            Form1.boyutlama.butonkucult(button1, 3, 6);
        }
    }
}
