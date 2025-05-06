using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Database;
using Firebase.Database.Query;



namespace AtYarisi
{
    public partial class Form1 : Form
    {
        private readonly FirebaseDBService _dbService;
        public Form1()
        {
            InitializeComponent();
            _dbService = new FirebaseDBService("https://atyarisibahis-default-rtdb.europe-west1.firebasedatabase.app/");
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string yazilanId = await _dbService.AddDataAsync("testVeri", new { Mesaj = "Deneme" });
            MessageBox.Show($"Veri başarıyla yazıldı. ID: {yazilanId}");

            var okunanVeri = await _dbService.ReadDataAsync<object>($"testVeri/{yazilanId}");
            if (okunanVeri != null)
            {
                Console.WriteLine($"Okunan Veri (ID: {yazilanId}): {okunanVeri}");
            }
            else
            {
                Console.WriteLine($"Okuma başarısız. ID: {yazilanId} bulunamadı.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            basla basla = new basla();
            basla.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ayarlar ayarlar = new ayarlar();
            ayarlar.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            admin admin = new admin();
            admin.Show();
            this.Hide();
        }
    }
}
