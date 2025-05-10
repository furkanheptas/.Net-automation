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
        private readonly FirebaseDBService _dbService;
        private Dictionary<string, At> _atlar;
        private decimal _toplamPasifGelir = 0;
        public atlarim()
        {
            InitializeComponent();
            this.FormClosed += (sender, e) => Application.Exit();
            _dbService = new FirebaseDBService("https://atyarisibahis-default-rtdb.europe-west1.firebasedatabase.app/");
            _atlar = new Dictionary<string, At>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            basla basla = new basla();
            basla.Show();
            this.Hide();
        }

        private async void atlarim_Load(object sender, EventArgs e)
        {
            int x = 800;
            int y = 600;

            this.Size = new System.Drawing.Size(x, y);

            int ekranGenisligi = Screen.PrimaryScreen.WorkingArea.Width;
            int ekranYuksekligi = Screen.PrimaryScreen.WorkingArea.Height;

            int baslangicX = (ekranGenisligi - this.Width) / 2;
            int baslangicY = (ekranYuksekligi - this.Height) / 2;

            this.Location = new System.Drawing.Point(baslangicX, baslangicY);
            await atYukle();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            boyutlama.butonbuyut(button1, 5, 10);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            boyutlama.butonkucult(button1, 5, 10);
        }
        public async Task atYukle()
        {
            _atlar = await _dbService.GetAllHorsesDictionaryAsync();

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

            dataGridView1.Columns["ID"].Visible = false;

            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string selectedAtId = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();

                if (_atlar.TryGetValue(selectedAtId, out At selectedAt))
                {
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
