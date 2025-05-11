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
    public partial class basla : Form
    {
        // Formun yapıcı metodu
        public basla()
        {
            InitializeComponent(); // Form bileşenlerini başlatır

            // Form kapatıldığında uygulamayı tamamen sonlandır
            this.FormClosed += (sender, e) => Application.Exit();
        }

        // "Ana Sayfa" butonuna tıklandığında Form1'e geçiş yapar
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(); // Yeni bir Form1 nesnesi oluştur
            form1.Show(); // Yeni formu göster
            this.Hide(); // Mevcut formu gizle
        }

        // "At Yarışları" butonuna tıklandığında atYarislari formuna geçiş yapar
        private void button2_Click(object sender, EventArgs e)
        {
            atYarislari atYarislari = new atYarislari(); // Yeni bir atYarislari nesnesi oluştur
            atYarislari.Show(); // Yeni formu göster
            this.Hide(); // Mevcut formu gizle
        }

        // "Hesap" butonuna tıklandığında hesap formuna geçiş yapar
        private void button3_Click(object sender, EventArgs e)
        {
            hesap hesap = new hesap(); // Yeni bir hesap nesnesi oluştur
            hesap.Show(); // Yeni formu göster
            this.Hide(); // Mevcut formu gizle
        }

        // "İstatistikler" butonuna tıklandığında istatistikler formuna geçiş yapar
        private void button4_Click(object sender, EventArgs e)
        {
            istatistikler istatistikler = new istatistikler(); // Yeni bir istatistikler nesnesi oluştur
            istatistikler.Show(); // Yeni formu göster
            this.Hide(); // Mevcut formu gizle
        }

        // "Atlarım" butonuna tıklandığında atlarim formuna geçiş yapar
        private void button5_Click(object sender, EventArgs e)
        {
            atlarim atlarim = new atlarim(); // Yeni bir atlarim nesnesi oluştur
            atlarim.Show(); // Yeni formu göster
            this.Hide(); // Mevcut formu gizle
        }

        // Form yüklendiğinde çalışır (şu anda boş)
        private void basla_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde yapılacak işlemler buraya eklenebilir
        }

        // Fare "At Yarışları" butonunun üzerine geldiğinde butonu büyüt
        private void button2_MouseEnter(object sender, EventArgs e)
        {
            Form1.boyutlama.butonbuyut(button2, 5, 10);
        }

        // Fare "At Yarışları" butonundan ayrıldığında butonu küçült
        private void button2_MouseLeave(object sender, EventArgs e)
        {
            Form1.boyutlama.butonkucult(button2, 5, 10);
        }

        // Fare "Hesap" butonunun üzerine geldiğinde butonu büyüt
        private void button3_MouseEnter(object sender, EventArgs e)
        {
            Form1.boyutlama.butonbuyut(button3, 5, 10);
        }

        // Fare "Hesap" butonundan ayrıldığında butonu küçült
        private void button3_MouseLeave(object sender, EventArgs e)
        {
            Form1.boyutlama.butonkucult(button3, 5, 10);
        }

        // Fare "İstatistikler" butonunun üzerine geldiğinde butonu büyüt
        private void button4_MouseEnter(object sender, EventArgs e)
        {
            Form1.boyutlama.butonbuyut(button4, 5, 10);
        }

        // Fare "İstatistikler" butonundan ayrıldığında butonu küçült
        private void button4_MouseLeave(object sender, EventArgs e)
        {
            Form1.boyutlama.butonkucult(button4, 5, 10);
        }

        // Fare "Atlarım" butonunun üzerine geldiğinde butonu büyüt
        private void button5_MouseEnter(object sender, EventArgs e)
        {
            Form1.boyutlama.butonbuyut(button5, 5, 10);
        }

        // Fare "Atlarım" butonundan ayrıldığında butonu küçült
        private void button5_MouseLeave(object sender, EventArgs e)
        {
            Form1.boyutlama.butonkucult(button5, 5, 10);
        }

        // Fare "Ana Sayfa" butonunun üzerine geldiğinde butonu büyüt
        private void button1_MouseEnter(object sender, EventArgs e)
        {
            Form1.boyutlama.butonbuyut(button1, 5, 10);
        }

        // Fare "Ana Sayfa" butonundan ayrıldığında butonu küçült
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            Form1.boyutlama.butonkucult(button1, 5, 10);
        }
    }
}

