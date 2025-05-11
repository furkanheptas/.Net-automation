using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AtYarisi.atYarislari;

namespace AtYarisi
{
    public partial class yaris : Form
    {
        // Yarışın sona erdiğini bildirmek için kullanılan delegate
        public delegate void YarisSonlandiEventHandler(string kazananAtIsmi);
        // Yarış sona erdiğinde tetiklenen event
        public event YarisSonlandiEventHandler YarisSonlandi;
        // Yarış sınıfından seçilen yarış nesnesi
        private Yaris _secilenYaris;
        // Atların yarış sırasını belirlemek için kullanılan Random nesnesi
        private Random _yarismasirasiRnd = new Random();
        // Yarışı kazanan atın PictureBox kontrolünü saklamak için kullanılır
        private PictureBox _kazananAtKontrol = null;
        // Yarışın bitip bitmediğini kontrol etmek için kullanılır
        private bool _yarisBitti = false;
        public yaris(Yaris secilenYaris)
        {
            InitializeComponent(); // Formun bileşenlerini başlatır
            _secilenYaris = secilenYaris; // Seçilen yarış nesnesini alır
        }
       
        int gerisayim = 3;
        // Form yüklendiğinde yapılacak işlemler
        private void yaris_Load(object sender, EventArgs e)
        {
            gerisayimTimer.Start();

            // Seçilen yarışa katılan her at için görsel öğeler oluşturur
            foreach (var at in _secilenYaris.KatilanAtlar)
            {
                Console.WriteLine(at.Isim); // Konsola atın ismini yazdırır (debug amaçlı)

                // Atın resmini temsil eden PictureBox oluşturur
                PictureBox atResim = new PictureBox();
                atResim.Image = Properties.Resources.kosucu; // Varsayılan bir resim atar
                atResim.SizeMode = PictureBoxSizeMode.StretchImage;//
                atResim.Text = at.Isim; // Atın ismini PictureBox'ın Text özelliğine atar
                atResim.Size = new Size(50, 50); // PictureBox'ın boyutunu ayarlar
                // PictureBox'ın konumunu, atın yarışa katılım sırasına göre ayarlar
                atResim.Location = new Point(10, 50 + _secilenYaris.KatilanAtlar.IndexOf(at) * 60);
                panel1.Controls.Add(atResim); // PictureBox'ı forma ekler
                atResim.Tag = at; // At nesnesini PictureBox'ın Tag özelliğine atar (veri saklamak için)
                atResim.Name = "atResim" + at.Isim; // PictureBox'a benzersiz bir isim verir

                // Atın ismini gösteren Label oluşturur
                Label atIsimLabel = new Label();
                atIsimLabel.Text = at.Isim; // Label'ın Text özelliğine atın ismini atar
                // Label'ın konumunu, atın yarışa katılım sırasına göre ayarlar
                atIsimLabel.Location = new Point(10, 100 + _secilenYaris.KatilanAtlar.IndexOf(at) * 60);
                atIsimLabel.AutoSize = true; // Label'ın boyutunun içeriğe göre ayarlanmasını sağlar
                panel1.Controls.Add(atIsimLabel); // Label'ı forma ekler
                atIsimLabel.Name = "atIsimLabel" + at.Isim; // Label'a benzersiz bir isim verir

                ImageAnimator.Animate(atResim.Image, (senderAnimate, args) => atResim.Invalidate());// Her kare değiştiğinde gif framelerini yeniden çizme

            }
            gifTimer = new Timer();
            gifTimer.Interval = 50;
            gifTimer.Tick += (s, ev) =>
            {
                ImageAnimator.UpdateFrames();
                foreach (Control c in panel1.Controls)
                {
                    if (c is PictureBox pb && pb.Image != null)
                        pb.Invalidate();
                }
                // Form üzerindeki bütün PictureBoxlara uygulanacak.
            };


        }
        

        // Timer her tetiklendiğinde (belirli aralıklarla) yapılacak işlemler
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Eğer kazanan at henüz belirlenmediyse
            if (_kazananAtKontrol == null)
            {
                // Formdaki her bir kontrolü dolaşır
                foreach (Control kontrol in panel1.Controls)
                {
                    // Eğer kontrol bir PictureBox ise ve Tag özelliği bir At nesnesi ise
                    if (kontrol is PictureBox && kontrol.Tag is At atBilgisi)
                    {
                        // Atın dayanıklılığını azaltır
                        atBilgisi.Dayaniklilik -= (float)_yarismasirasiRnd.Next(1, 5) / 7f;
                        atBilgisi.Dayaniklilik = Math.Max(0, atBilgisi.Dayaniklilik); // Dayanıklılık 0'ın altına düşemez

                        // Atın hızını etkileyen faktörleri hesaplar
                        float dayaniklilikCarpani = Math.Max(0.5f, atBilgisi.Dayaniklilik / 100f); // Dayanıklılık çarpanı (0.5 ile 1 arasında)
                        float bakimCarpani = 1 + (atBilgisi.Bakim / 300f); // Bakım çarpanı
                        int rngLimit = atBilgisi.Rng / 5; // Rastgelelik limitini hesaplar
                        float rngBonus = _yarismasirasiRnd.Next(-rngLimit, rngLimit + 1) / 2f; // Rastgele bonus

                        // At yarışı bitiş çizgisine yaklaştıkça hızı azaltan bir faktör
                        float dengeFreni = kontrol.Left > this.ClientSize.Width * 0.7f ? 0.9f : 1.0f;

                        // Geride kalan atlara hız bonusu veren bir faktör
                        float gerideBonus = 1.0f;
                        int maxKonum = panel1.Controls.OfType<PictureBox>().Max(pb => pb.Left); // En öndeki atın konumunu bulur
                        if (kontrol.Left < maxKonum)
                        {
                            int fark = maxKonum - kontrol.Left; // Öndeki atla arasındaki farkı hesaplar
                            gerideBonus = 1 + (fark / (float)this.ClientSize.Width) * 0.5f; // Fark arttıkça bonus artar
                        }

                        // Çok nadir durumlarda atın hızını aniden artıran bir rastgele olay
                        if (_yarismasirasiRnd.Next(0, 1000) < 5)
                        {
                            rngBonus += _yarismasirasiRnd.Next(5, 15); // Büyük bir bonus ekler
                        }

                        // Atın hareket miktarını hesaplar
                        float hareket = (atBilgisi.Hiz / 10f) * dayaniklilikCarpani * bakimCarpani * dengeFreni * gerideBonus + rngBonus;
                        hareket = Math.Max(1f, hareket); // Hareket miktarı en az 1 olabilir

                        // Atın konumunu günceller
                        kontrol.Left += (int)Math.Round(hareket);
                        kontrol.Tag = atBilgisi; // Güncellenmiş at bilgisini PictureBox'a geri atar

                        // Atın ismini gösteren Label'ın konumunu günceller
                        string labelName = "atIsimLabel" + atBilgisi.Isim;
                        Control labelControl = panel1.Controls.Find(labelName, false).FirstOrDefault();
                        if (labelControl != null)
                        {
                            labelControl.Left = kontrol.Left;
                        }

                        // Eğer bir at yarışı bitiş çizgisine ulaştıysa ve kazanan henüz belirlenmediyse
                        if (_kazananAtKontrol == null && kontrol.Right >= this.ClientSize.Width - 80)
                        {
                            _kazananAtKontrol = kontrol as PictureBox; // Kazanan atın PictureBox'ını saklar
                        }
                    }
                }

                // Eğer kazanan at belirlendiyse
                if (_kazananAtKontrol != null)
                {
                    yarismuzik.Durdur();
                    timer1.Stop(); // Timer'ı durdurur
                    _yarisBitti = true; // Yarışın bittiğini işaretler
                    string kazananAtIsmi = _kazananAtKontrol.Text; // Kazanan atın ismini alır
                    MessageBox.Show($"Yarışı kazanan: {kazananAtIsmi}", "Yarış Sonucu", MessageBoxButtons.OK, MessageBoxIcon.Information); // Sonucu gösterir
                    _kazananAtKontrol = null; // Kazanan at kontrolünü sıfırlar (gerekli olmayabilir)

                    YarisSonlandi?.Invoke(kazananAtIsmi); // Yarışın bittiğini ve kazananı bildiren event'i tetikler
                    //Yarışı bitiren hesap formuna ulaşır.
                    hesap hesap = new hesap();
                    hesap.Hesap_YarisSonlandi(kazananAtIsmi);

                    this.Dispose(); // Formu kapatır

                }
            }
        }

        // Button enter leave olayları

        private void yaris_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void gerisayimTimer_Tick(object sender, EventArgs e)
        {
         
            if (gerisayim > 0)
            {
                gerisayimLabel.Text = gerisayim.ToString();
                SoundPlayer baslatt = new SoundPlayer("gerisayim.wav");
                baslatt.Play();
            }
            else if (gerisayim == 0)
            {
                gerisayimLabel.Text = "GO!";
                SoundPlayer baslatt = new SoundPlayer("baslaa.wav");
                baslatt.Play();
            }
            else
            {
                gerisayimTimer.Stop();
                gerisayimLabel.Visible = false;

                timer1.Start();
                yarismuzik.Baslat();
                gifTimer.Start();
            }

            gerisayim--;
        }
    }
}
