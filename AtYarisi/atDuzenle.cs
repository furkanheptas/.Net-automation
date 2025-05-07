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
        private readonly FirebaseDBService _dbService;
        private Dictionary<string, At> _atlar;
        private string _secilenAtId;

        private async Task LoadHorses()
        {
            _atlar = await _dbService.GetAllHorsesDictionaryAsync();
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

            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        public atDuzenle()
        {
            InitializeComponent();
            _dbService = new FirebaseDBService("https://atyarisibahis-default-rtdb.europe-west1.firebasedatabase.app/");
            _atlar = new Dictionary<string, At>();
        }

        private async void atDuzenle_Load(object sender, EventArgs e)
        {
            int x = 800;
            int y = 600;

            this.Size = new System.Drawing.Size(x, y);

            int ekranGenisligi = Screen.PrimaryScreen.WorkingArea.Width;
            int ekranYuksekligi = Screen.PrimaryScreen.WorkingArea.Height;

            int baslangicX = (ekranGenisligi - this.Width) / 2;
            int baslangicY = (ekranYuksekligi - this.Height) / 2;

            this.Location = new System.Drawing.Point(baslangicX, baslangicY);
            await LoadHorses();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            admin adminForm = new admin();
            adminForm.Show();
            this.Hide();
        }

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

        private async void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_secilenAtId) && _atlar.ContainsKey(_secilenAtId))
            {
                if (!int.TryParse(textBox2.Text, out int hiz) ||
                    !int.TryParse(textBox3.Text, out int dayaniklilik) ||
                    !int.TryParse(textBox4.Text, out int bakim) ||
                    !int.TryParse(textBox5.Text, out int rng)
                    )
                {
                    MessageBox.Show("Hız, Dayanıklılık, RNG ve bakım değerleri sayısal olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                At guncellenmisAt = new At
                {
                    Isim = textBox1.Text,
                    Hiz = hiz,
                    Dayaniklilik = dayaniklilik,
                    Bakim = bakim,
                    Rng = rng,
                    Sahiplik = checkBox1.Checked
                };

                await _dbService.UpdateHorseAsync(_secilenAtId, guncellenmisAt);
                MessageBox.Show("At bilgileri güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadHorses();
            }
            else
            {
                MessageBox.Show("Lütfen önce listeden bir at seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_secilenAtId))
            {
                if (MessageBox.Show("Seçilen atı silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    await _dbService.DeleteHorseAsync(_secilenAtId);
                    MessageBox.Show("At başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadHorses();
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
    }
}
