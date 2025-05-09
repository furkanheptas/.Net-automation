using System;
using System.IO;
using System.Globalization;
using System.Windows.Forms;

namespace AtYarisi
{
    public partial class hesap : Form
    {
        private decimal _kullaniciBakiyesi;
        private decimal _borcMiktari;
        private int _kalanYarisHakki;
        private const string BorcDosyasi = "borc_bilgisi.txt";
        private readonly hesapIslemleri _hesapIslemleri = new hesapIslemleri();

        public delegate void YarisSonlandiEventHandler(string kazananAtIsmi);
        public event YarisSonlandiEventHandler YarisSonlandi;

        public hesap()
        {
            InitializeComponent();
            this.FormClosed += (sender, e) => Application.Exit();
            _kullaniciBakiyesi = _hesapIslemleri.DosyadanBakiyeYukle();
            BorcBilgileriniYukle();
            BakiyeBilgisiGuncelle();
            BorcBilgisiGuncelle();
            YarisSonlandi += Hesap_YarisSonlandi;
        }

        private void hesap_Load(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            basla basla = new basla();
            basla.Show();
            this.Hide();
        }

        private void BakiyeBilgisiGuncelle()
        {
            label1.Text = $"Bakiye: {_kullaniciBakiyesi:F2} TL";
        }

        private void BorcBilgisiGuncelle()
        {
            if (_borcMiktari > 0)
            {
                label3.Text = $"Borç: {_borcMiktari:F2} TL\nKalan Yarış Hakkı: {_kalanYarisHakki}";
            }
            else
            {
                label3.Text = "Borcunuz bulunmamaktadır.";
            }
        }

        private void BorcBilgileriniYukle()
        {
            if (File.Exists(BorcDosyasi))
            {
                try
                {
                    string[] satirlar = File.ReadAllLines(BorcDosyasi);
                    if (satirlar.Length == 2 &&
                        decimal.TryParse(satirlar[0], NumberStyles.Float, CultureInfo.InvariantCulture, out _borcMiktari) &&
                        int.TryParse(satirlar[1], out _kalanYarisHakki))
                    {
                    }
                    else
                    {
                        SifirlaVeKaydet();
                    }
                }
                catch
                {
                    SifirlaVeKaydet();
                }
            }
            else
            {
                SifirlaVeKaydet();
            }
        }

        private void SifirlaVeKaydet()
        {
            _borcMiktari = 0;
            _kalanYarisHakki = 0;
            DosyayaBorcBilgileriniKaydet();
        }

        private void DosyayaBorcBilgileriniKaydet()
        {
            try
            {
                string[] veri = {
                    _borcMiktari.ToString(CultureInfo.InvariantCulture),
                    _kalanYarisHakki.ToString()
                };
                File.WriteAllLines(BorcDosyasi, veri);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Borç dosyası kaydedilemedi: " + ex.Message);
            }
        }

        private void BorcAl(decimal miktar)
        {
            if (miktar <= 0)
            {
                MessageBox.Show("Geçersiz borç miktarı.");
                return;
            }

            if (_borcMiktari > 0)
            {
                MessageBox.Show("Zaten bir borcunuz var.");
                return;
            }

            int yarisHakki = (int)Math.Floor(Math.Log10((double)miktar) * 3 + 1);
            if (yarisHakki < 1)
                yarisHakki = 1;

            _borcMiktari = miktar;
            _kullaniciBakiyesi += miktar;
            _kalanYarisHakki = yarisHakki;

            _hesapIslemleri.DosyayaBakiyeKaydet(_kullaniciBakiyesi);
            DosyayaBorcBilgileriniKaydet();
            BakiyeBilgisiGuncelle();
            BorcBilgisiGuncelle();

            MessageBox.Show($"Borç alındı. Yarış hakkınız: {_kalanYarisHakki}");
        }


        private void BorcOde(decimal miktar)
        {
            if (miktar <= 0)
            {
                MessageBox.Show("Geçersiz ödeme miktarı.");
                return;
            }

            if (_borcMiktari == 0)
            {
                MessageBox.Show("Borcunuz yok.");
                return;
            }

            if (miktar > _kullaniciBakiyesi)
            {
                MessageBox.Show("Yetersiz bakiye.");
                return;
            }

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

            _hesapIslemleri.DosyayaBakiyeKaydet(_kullaniciBakiyesi);
            DosyayaBorcBilgileriniKaydet();
            BakiyeBilgisiGuncelle();
            BorcBilgisiGuncelle();

            MessageBox.Show("Ödeme başarıyla yapıldı.");
        }

        public void Hesap_YarisSonlandi(string kazananAtIsmi)
        {
            if (_borcMiktari > 0)
            {
                if (_kalanYarisHakki > 0)
                {
                    _kalanYarisHakki--;
                }
                else
                {
                    _borcMiktari *= 1.10m;
                    MessageBox.Show("Yarış hakkınız bittiği için borcunuza %10 faiz eklendi!");
                }

                DosyayaBorcBilgileriniKaydet();
                BorcBilgisiGuncelle();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(textBox1.Text, out decimal miktar))
            {
                BorcAl(miktar);
            }
            else
            {
                MessageBox.Show("Geçersiz miktar.");
            }
            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(textBox2.Text, out decimal miktar))
            {
                BorcOde(miktar);
            }
            else
            {
                MessageBox.Show("Geçersiz miktar.");
            }
            textBox2.Text = "";
        }
    }
}
