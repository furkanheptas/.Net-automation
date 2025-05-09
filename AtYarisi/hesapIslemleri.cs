using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AtYarisi
{
    internal class hesapIslemleri
    {
        private const string BakiyeDosyasi = "kullanici_bakiye.txt";
        public decimal DosyadanBakiyeYukle()
        {
            if (File.Exists(BakiyeDosyasi))
            {
                try
                {
                    string bakiyeStr = File.ReadAllText(BakiyeDosyasi);
                    if (decimal.TryParse(bakiyeStr, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal bakiye))
                    {
                        return bakiye;
                    }
                    else
                    {
                        MessageBox.Show("Geçersiz bakiye dosyası. Varsayılan bakiye yüklendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return 1000;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bakiye dosyası okunamadı. Varsayılan bakiye yüklendi. Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 1000;
                }
            }
            else
            {
                DosyayaBakiyeKaydet(1000);
                return 1000;
            }
        }

        public void DosyayaBakiyeKaydet(decimal bakiye)
        {
            try
            {
                File.WriteAllText(BakiyeDosyasi, bakiye.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bakiye dosyası yazılamadı. Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
