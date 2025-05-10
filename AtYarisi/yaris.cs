using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AtYarisi.atYarislari;

namespace AtYarisi
{
    public partial class yaris : Form
    {
        public delegate void YarisSonlandiEventHandler(string kazananAtIsmi);
        public event YarisSonlandiEventHandler YarisSonlandi;
        private Yaris _secilenYaris;
        private Random _yarismasirasiRnd = new Random();
        private PictureBox _kazananAtKontrol = null;
        private bool _yarisBitti = false;
        public yaris(Yaris secilenYaris)
        {
            InitializeComponent();
            _secilenYaris = secilenYaris;
        }

        private void yaris_Load(object sender, EventArgs e)
        {
            foreach (var at in _secilenYaris.KatilanAtlar)
            {
                Console.WriteLine(at.Isim);

                PictureBox atResim = new PictureBox();
                atResim.Image = Properties.Resources.anasayfafoto;
                atResim.Text = at.Isim;
                atResim.Size = new Size(50, 50);
                atResim.Location = new Point(10, 50 + _secilenYaris.KatilanAtlar.IndexOf(at) * 60);
                this.Controls.Add(atResim);
                atResim.Tag = at;

                Label atIsimLabel = new Label();
                atIsimLabel.Text = at.Isim;
                atIsimLabel.Location = new Point(10, 100 + _secilenYaris.KatilanAtlar.IndexOf(at) * 60);
                atIsimLabel.AutoSize = true;
                this.Controls.Add(atIsimLabel);
                atResim.Name = "atResim" + at.Isim;
                atIsimLabel.Name = "atIsimLabel" + at.Isim;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_kazananAtKontrol == null)
            {
                foreach (Control kontrol in this.Controls)
                {
                    if (kontrol is PictureBox && kontrol.Tag is At atBilgisi)
                    {
                        atBilgisi.Dayaniklilik -= (float)_yarismasirasiRnd.Next(1, 5) / 7f;
                        atBilgisi.Dayaniklilik = Math.Max(0, atBilgisi.Dayaniklilik);

                        float dayaniklilikCarpani = Math.Max(0.5f, atBilgisi.Dayaniklilik / 100f);
                        float bakimCarpani = 1 + (atBilgisi.Bakim / 300f);
                        int rngLimit = atBilgisi.Rng / 5;
                        float rngBonus = _yarismasirasiRnd.Next(-rngLimit, rngLimit + 1) / 2f;

                        float dengeFreni = kontrol.Left > this.ClientSize.Width * 0.7f ? 0.9f : 1.0f;

                        float gerideBonus = 1.0f;
                        int maxKonum = this.Controls.OfType<PictureBox>().Max(pb => pb.Left);
                        if (kontrol.Left < maxKonum)
                        {
                            int fark = maxKonum - kontrol.Left;
                            gerideBonus = 1 + (fark / (float)this.ClientSize.Width) * 0.5f;
                        }

                        if (_yarismasirasiRnd.Next(0, 1000) < 5)
                        {
                            rngBonus += _yarismasirasiRnd.Next(5, 15);
                        }

                        float hareket = (atBilgisi.Hiz / 10f) * dayaniklilikCarpani * bakimCarpani * dengeFreni * gerideBonus + rngBonus;
                        hareket = Math.Max(1f, hareket);

                        kontrol.Left += (int)Math.Round(hareket);
                        kontrol.Tag = atBilgisi;

                        string labelName = "atIsimLabel" + atBilgisi.Isim;
                        Control labelControl = this.Controls.Find(labelName, false).FirstOrDefault();
                        if (labelControl != null)
                        {
                            labelControl.Left = kontrol.Left;
                        }

                        if (_kazananAtKontrol == null && kontrol.Right >= this.ClientSize.Width - 80)
                        {
                            _kazananAtKontrol = kontrol as PictureBox;
                        }
                    }
                }

                if (_kazananAtKontrol != null)
                {
                    timer1.Stop();
                    _yarisBitti = true;
                    string kazananAtIsmi = _kazananAtKontrol.Text;
                    MessageBox.Show($"Yarışı kazanan: {kazananAtIsmi}", "Yarış Sonucu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _kazananAtKontrol = null;

                    YarisSonlandi?.Invoke(kazananAtIsmi);
                    hesap hesap = new hesap();
                    hesap.Hesap_YarisSonlandi(kazananAtIsmi);
                    this.Dispose();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
