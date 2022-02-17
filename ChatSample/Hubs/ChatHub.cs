using BackEndChallenge.Models;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace BackendChallenge.Hubs
{
    public class ChatHub : Hub
    {
        static HttpClient client = new HttpClient();
        private string apiUrl = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=USD";

        public async Task SendMessage()
        {
            var myDeserializedClass = new List<Crypto>();

            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                var data = await response.Content.ReadAsStringAsync();
                myDeserializedClass = JsonConvert.DeserializeObject<List<Crypto>>(data);
            }
            catch (Exception ex) { }

            var database = new MongoClient("mongodb://localhost:27017/test");
            var db = database.GetDatabase("Logs");
            var coll = db.GetCollection<Logs>("Logs");
            coll.InsertOneAsync(new Logs { Date = DateTime.Now, Crypto = myDeserializedClass });

            await Clients.All.SendAsync("ReceiveMessage", myDeserializedClass);
        }
    }
}
