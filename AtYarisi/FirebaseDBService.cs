using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;

namespace AtYarisi
{
    public class FirebaseDBService
    {
        private readonly FirebaseClient firebaseClient;
        public FirebaseDBService(string databaseUrl)
        {
            // Firebase veritabanı URL'si ile FirebaseClient oluşturulur.
            firebaseClient = new FirebaseClient(databaseUrl);
        }

        // Belirtilen path'e veriyi yazmak için kullanılan asenkron metot.
        // Veri, belirtilen path'teki mevcut verinin üzerine yazılır.
        public async Task WriteDataAsync(string path, object data)
        {
            await firebaseClient
                .Child(path) // Verinin yazılacağı path belirtilir.
                .PutAsync(data); // Veri, belirtilen path'e yazılır.
        }

        // Belirtilen path'e yeni veri eklemek için kullanılan asenkron metot.
        // Veri, benzersiz bir anahtar ile belirtilen path'e eklenir.
        // Eklenen verinin anahtarı (key) döndürülür.
        public async Task<string> AddDataAsync(string path, object data)
        {
            var result = await firebaseClient
                .Child(path) // Verinin ekleneceği path belirtilir.
                .PostAsync(data); // Veri, belirtilen path'e eklenir ve benzersiz bir anahtar oluşturulur.
            return result.Key; // Eklenen verinin anahtarı döndürülür.
        }

        // Belirtilen path'ten veriyi okumak için kullanılan asenkron metot.
        // Veri, belirtilen path'ten okunur ve belirtilen türde (T) bir nesneye dönüştürülür.
        // Eğer belirtilen path'te veri yoksa, varsayılan bir değer veya null döndürülür.
        public async Task<T> ReadDataAsync<T>(string path) where T : class
        {
            var snapshot = await firebaseClient
                .Child(path) // Verinin okunacağı path belirtilir.
                .OnceAsync<T>(); // Veri, belirtilen path'ten bir kez okunur ve T türüne dönüştürülür.
            return snapshot.FirstOrDefault()?.Object; // Okunan verilerden ilk nesne döndürülür, yoksa null döndürülür.
        }

        // "atlar" path'indeki tüm atları bir Dictionary olarak almak için kullanılan asenkron metot.
        // Dictionary'nin anahtarı (key) atın benzersiz ID'si, değeri ise At nesnesidir.
        public async Task<Dictionary<string, At>> GetAllHorsesDictionaryAsync()
        {
            var snapshot = await firebaseClient
                .Child("atlar") // "atlar" path'inden veri okunur.
                .OnceAsync<At>(); // Veri, At türünde okunur.

            // Gelen veriler bir Dictionary'ye dönüştürülür.
            // Dictionary'nin anahtarı (item.Key) atın ID'si, değeri (item.Object) ise At nesnesidir.
            return snapshot.ToDictionary(item => item.Key, item => item.Object);
        }

        // Belirtilen at ID'sine sahip atın bilgilerini güncellemek için kullanılan asenkron metot.
        // Veri, belirtilen path'teki mevcut verinin üzerine yazılır.
        public async Task UpdateHorseAsync(string atId, At guncellenmisAt)
        {
            await firebaseClient
                .Child($"atlar/{atId}") // Güncellenecek atın path'i belirtilir.
                .PutAsync(guncellenmisAt); // Yeni at bilgileri belirtilen path'e yazılır.
        }

        // Belirtilen at ID'sine sahip atı silmek için kullanılan asenkron metot.
        public async Task DeleteHorseAsync(string atId)
        {
            await firebaseClient
                .Child($"atlar/{atId}") // Silinecek atın path'i belirtilir.
                .DeleteAsync(); // Belirtilen path'teki veri silinir.
        }
    }
}
