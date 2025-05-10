using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AtYarisi
{
    public partial class istatistikler : Form
    {
        public istatistikler()
        {
            InitializeComponent();
            this.FormClosed += (sender, e) => Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            basla basla = new basla();
            basla.Show();
            this.Hide();
        }

        private void istatistikler_Load(object sender, EventArgs e)
        {

            lblToplamBahis.Text = $"Toplam Bahis Sayısı: {istatistik.Istatistik.ToplamBahisSayisi}";
            lblKazanilanBahis.Text = $"Kazanılan Bahis Sayısı: {istatistik.Istatistik.KazanilanBahisSayisi}";
            lblKaybedilenBahis.Text = $"Kaybedilen Bahis Sayısı: {istatistik.Istatistik.ToplamBahisSayisi - istatistik.Istatistik.KazanilanBahisSayisi}";

            lblKazanc.Text = $"Kazanılan Para: {istatistik.Istatistik.ToplamKazanilanPara:F2} TL";
            lblKayip.Text = $"Kaybedilen Para: {istatistik.Istatistik.ToplamKaybedilenPara:F2} TL";
           
            chart1.Series.Clear();
            chart1.Titles.Add("Bahis Kazanma Oranı");

            Series s = new Series();
            s.ChartType = SeriesChartType.Pie;
            s.Points.AddXY("Kazanılan", istatistik.Istatistik.KazanilanBahisSayisi);
            s.Points.AddXY("Kaybedilen", istatistik.Istatistik.ToplamBahisSayisi - istatistik.Istatistik.KazanilanBahisSayisi);
            s.IsValueShownAsLabel = true;

            s.Points[0].Color = Color.Green;
            s.Points[1].Color = Color.Red;

            chart1.Series.Add(s);
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            Form1.boyutlama.butonbuyut(button1, 5, 10);
        }
    }
}
