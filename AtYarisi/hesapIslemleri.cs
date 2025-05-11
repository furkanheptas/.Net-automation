using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AtYarisi
{
    // Bakiye işlemlerini yöneten sınıf
    internal class hesapIslemleri
    {
        // Kullanıcının bakiyesinin saklanacağı dosya adı
        private const string BakiyeDosyasi = "kullanici_bakiye.txt";

        /// Dosyadan kullanıcı bakiyesini yükler.
        /// Eğer dosya yoksa veya geçersizse, varsayılan bakiye (1000) döner.
        /// <returns>decimal türünde kullanıcı bakiyesi</returns>
        public decimal DosyadanBakiyeYukle()
        {
            // Dosya mevcutsa içeriği oku
            if (File.Exists(BakiyeDosyasi))
            {
                try
                {
                    // Dosyadaki tüm içeriği oku (bakiye string olarak)
                    string bakiyeStr = File.ReadAllText(BakiyeDosyasi);

                    // Bakiye metnini ondalık sayıya dönüştür (yerel ayarlara göre)
                    if (decimal.TryParse(bakiyeStr, out decimal bakiye))
                    {
                        // Geçerli bakiye değeri döndürülür
                        return bakiye;
                    }
                    else
                    {
                        // Geçersiz veri varsa kullanıcı uyarılır ve varsayılan bakiye döner
                        MessageBox.Show("Geçersiz bakiye dosyası. Varsayılan bakiye yüklendi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return 1000;
                    }
                }
                catch (Exception ex)
                {
                    // Dosya okuma sırasında bir hata oluşursa, kullanıcı uyarılır ve varsayılan bakiye döner
                    MessageBox.Show("Bakiye dosyası okunamadı. Varsayılan bakiye yüklendi. Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 1000;
                }
            }
            else
            {
                // Dosya bulunamazsa varsayılan bakiye kaydedilir ve döndürülür
                DosyayaBakiyeKaydet(1000);
                return 1000;
            }
        }

        /// Verilen bakiyeyi dosyaya yazar.
        /// name="bakiye" Kaydedilecek bakiyedir.
        public void DosyayaBakiyeKaydet(decimal bakiye)
        {
            try
            {
                // Bakiyeyi yerel formatta dosyaya yaz (örn. 1000,50 gibi)
                File.WriteAllText(BakiyeDosyasi, bakiye.ToString());
            }
            catch (Exception ex)
            {
                // Dosya yazılamazsa kullanıcıya hata mesajı göster
                MessageBox.Show("Bakiye dosyası yazılamadı. Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
