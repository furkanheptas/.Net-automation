using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AtYarisi.Atlar;
using static System.Net.WebRequestMethods;

namespace AtYarisi
{
    public partial class admin : Form
    {
        private readonly FirebaseDBService _dbService;
        public admin()
        {
            InitializeComponent();
            this.FormClosed += (sender, e) => Application.Exit();
            _dbService = new FirebaseDBService("https://atyarisibahis-default-rtdb.europe-west1.firebasedatabase.app/");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void admin_Load(object sender, EventArgs e)
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
    }
}
