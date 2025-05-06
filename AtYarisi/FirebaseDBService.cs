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
            firebaseClient = new FirebaseClient(databaseUrl);
        }

        public async Task WriteDataAsync(string path, object data)
        {
            await firebaseClient
                .Child(path)
                .PutAsync(data);
        }

        public async Task<string> AddDataAsync(string path, object data)
        {
            var result = await firebaseClient
                .Child(path)
                .PostAsync(data);
            return result.Key;
        }

        public async Task<T> ReadDataAsync<T>(string path) where T : class
        {
            var snapshot = await firebaseClient
                .Child(path)
                .OnceAsync<T>();
            return snapshot.FirstOrDefault()?.Object;
        }

    }
}
