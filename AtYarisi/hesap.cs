using System;
using System.IO;
using System.Globalization;
using System.Windows.Forms;

namespace AtYarisi
{
    public partial class hesap : Form
    {
        // Kullanıcının mevcut bakiyesi
        private decimal _kullaniciBakiyesi;

        // Kullanıcının mevcut borç miktarı
        private decimal _borcMiktari;

        // Kullanıcının kalan yarış hakkı
        private int _kalanYarisHakki;

        // Borç bilgilerini saklayan dosyanın adı
        private const string BorcDosyasi = "borc_bilgisi.txt";

        // Hesap işlemleri için kullanılan yardımcı sınıf
        private readonly hesapIslemleri _hesapIslemleri = new hesapIslemleri();

        // Yarış sona erdiğinde tetiklenecek bir olay
        public delegate void YarisSonlandiEventHandler(string kazananAtIsmi);
        public event YarisSonlandiEventHandler YarisSonlandi;

        // Formun yapıcı metodu
        public hesap()
        {
            InitializeComponent(); // Form bileşenlerini başlatır

            // Form kapatıldığında uygulamayı tamamen sonlandır
            this.FormClosed += (sender, e) => Application.Exit();

            // Kullanıcı bakiyesini dosyadan yükle
            _kullaniciBakiyesi = _hesapIslemleri.DosyadanBakiyeYukle();

            // Borç bilgilerini yükle
            BorcBilgileriniYukle();

            // Bakiye ve borç bilgilerini güncelle
            BakiyeBilgisiGuncelle();
            BorcBilgisiGuncelle();

            // Yarış sona erdiğinde Hesap_YarisSonlandi metodunu çağır
            YarisSonlandi += Hesap_YarisSonlandi;
        }

        // Form yüklendiğinde ekran boyutunu ve konumunu ayarla
        private void hesap_Load(object sender, EventArgs e)
        {
            int x = 800; // Formun genişliği
            int y = 600; // Formun yüksekliği

            this.Size = new System.Drawing.Size(x, y);

            // Ekran çözünürlüğünü al
            int ekranGenisligi = Screen.PrimaryScreen.WorkingArea.Width;
            int ekranYuksekligi = Screen.PrimaryScreen.WorkingArea.Height;

            // Formu ekranın ortasına yerleştir
            int baslangicX = (ekranGenisligi - this.Width) / 2;
            int baslangicY = (ekranYuksekligi - this.Height) / 2;

            this.Location = new System.Drawing.Point(baslangicX, baslangicY);
        }

        // "Geri Dön" butonuna tıklandığında ana forma geçiş yap
        private void button1_Click(object sender, EventArgs e)
        {
            basla basla = new basla(); // Yeni bir basla formu oluştur
            basla.Show(); // Yeni formu göster
            this.Hide(); // Mevcut formu gizle
        }

        // Kullanıcı bakiyesini ekranda günceller
        private void BakiyeBilgisiGuncelle()
        {
            label1.Text = $"Bakiye: {_kullaniciBakiyesi:F2} TL";
        }

        // Kullanıcının borç bilgilerini ekranda günceller
        private void BorcBilgisiGuncelle()
        {
            if (_borcMiktari > 0) // Eğer borç varsa
            {
                label3.Text = $"Borç: {_borcMiktari:F2} TL\nKalan Yarış Hakkı: {_kalanYarisHakki}";
            }
            else // Eğer borç yoksa
            {
                label3.Text = "Borcunuz bulunmamaktadır.";
            }
        }

        // Borç bilgilerini dosyadan yükler
        private void BorcBilgileriniYukle()
        {
            if (File.Exists(BorcDosyasi)) // Eğer borç dosyası varsa
            {
                try
                {
                    // Dosyadaki verileri oku
                    string[] satirlar = File.ReadAllLines(BorcDosyasi);

                    // Verileri ayrıştır ve değişkenlere ata
                    if (satirlar.Length == 2 &&
                        decimal.TryParse(satirlar[0], NumberStyles.Float, CultureInfo.InvariantCulture, out _borcMiktari) &&
                        int.TryParse(satirlar[1], out _kalanYarisHakki))
                    {
                        // Başarılı bir şekilde yüklendi
                    }
                    else
                    {
                        // Dosya formatı hatalıysa sıfırla
                        SifirlaVeKaydet();
                    }
                }
                catch
                {
                    // Dosya okunamazsa sıfırla
                    SifirlaVeKaydet();
                }
            }
            else
            {
                // Dosya yoksa sıfırla
                SifirlaVeKaydet();
            }
        }

        // Borç bilgilerini sıfırlar ve dosyaya kaydeder
        private void SifirlaVeKaydet()
        {
            _borcMiktari = 0;
            _kalanYarisHakki = 0;
            DosyayaBorcBilgileriniKaydet();
        }

        // Borç bilgilerini dosyaya kaydeder
        private void DosyayaBorcBilgileriniKaydet()
        {
            try
            {
                // Borç bilgilerini dosyaya yaz
                string[] veri = {
                    _borcMiktari.ToString(CultureInfo.InvariantCulture),
                    _kalanYarisHakki.ToString()
                };
                File.WriteAllLines(BorcDosyasi, veri);
            }
            catch (Exception ex)
            {
                // Dosya kaydedilemezse hata mesajı göster
                MessageBox.Show("Borç dosyası kaydedilemedi: " + ex.Message);
            }
        }

        // Kullanıcıdan borç alır
        private void BorcAl(decimal miktar)
        {
            if (miktar <= 0) // Geçersiz borç miktarı kontrolü
            {
                MessageBox.Show("Geçersiz borç miktarı.");
                return;
            }

            if (_borcMiktari > 0) // Zaten borç varsa
            {
                MessageBox.Show("Zaten bir borcunuz var.");
                return;
            }

            // Yarış hakkını hesapla
            int yarisHakki = (int)Math.Floor(Math.Log10((double)miktar) * 3 + 1);
            if (yarisHakki < 1)
                yarisHakki = 1;

            // Borç bilgilerini güncelle
            _borcMiktari = miktar;
            _kullaniciBakiyesi += miktar;
            _kalanYarisHakki = yarisHakki;

            // Verileri kaydet ve ekranda güncelle
            _hesapIslemleri.DosyayaBakiyeKaydet(_kullaniciBakiyesi);
            DosyayaBorcBilgileriniKaydet();
            BakiyeBilgisiGuncelle();
            BorcBilgisiGuncelle();

            MessageBox.Show($"Borç alındı. Yarış hakkınız: {_kalanYarisHakki}");
        }

        // Kullanıcıdan borç ödemesi alır
        private void BorcOde(decimal miktar)
        {
            if (miktar <= 0) // Geçersiz ödeme miktarı kontrolü
            {
                MessageBox.Show("Geçersiz ödeme miktarı.");
                return;
            }

            if (_borcMiktari == 0) // Borç yoksa
            {
                MessageBox.Show("Borcunuz yok.");
                return;
            }

            if (miktar > _kullaniciBakiyesi) // Yetersiz bakiye kontrolü
            {
                MessageBox.Show("Yetersiz bakiye.");
                return;
            }

            // Borç ödeme işlemi
            if (miktar >= _borcMiktari)
            {
                _kullaniciBakiyesi -= _borcMiktari;
                _borcMiktari = 0;
                _kalanYarisHakki = 0;
            }
            else
            {
                _kullaniciBakiyesi -= miktar;
                _borcMiktari -= miktar;
            }

            // Verileri kaydet ve ekranda güncelle
            _hesapIslemleri.DosyayaBakiyeKaydet(_kullaniciBakiyesi);
            DosyayaBorcBilgileriniKaydet();
            BakiyeBilgisiGuncelle();
            BorcBilgisiGuncelle();

            MessageBox.Show("Ödeme başarıyla yapıldı.");
        }

        // Yarış sona erdiğinde borç bilgilerini günceller
        public void Hesap_YarisSonlandi(string kazananAtIsmi)
        {
            if (_borcMiktari > 0) // Eğer borç varsa
            {
                if (_kalanYarisHakki > 0) // Yarış hakkı varsa azalt
                {
                    _kalanYarisHakki--;
                }
                else // Yarış hakkı yoksa borca faiz ekle
                {
                    _borcMiktari *= 1.10m;
                    MessageBox.Show("Yarış hakkınız bittiği için borcunuza %10 faiz eklendi!");
                }

                // Verileri kaydet ve ekranda güncelle
                DosyayaBorcBilgileriniKaydet();
                BorcBilgisiGuncelle();
            }
        }

        // "Borç Al" butonuna tıklandığında çalışır
        private void button3_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(textBox1.Text, out decimal miktar)) // Girilen miktarı kontrol et
            {
                BorcAl(miktar); // Borç al
            }
            else
            {
                MessageBox.Show("Geçersiz miktar.");
            }
            textBox1.Text = ""; // Giriş alanını temizle
        }

        // "Borç Öde" butonuna tıklandığında çalışır
        private void button2_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(textBox2.Text, out decimal miktar)) // Girilen miktarı kontrol et
            {
                BorcOde(miktar); // Borç öde
            }
            else
            {
                MessageBox.Show("Geçersiz miktar.");
            }
            textBox2.Text = ""; // Giriş alanını temizle
        }
    }
}
