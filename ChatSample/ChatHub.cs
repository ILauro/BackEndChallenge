using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub

    {
        static HttpClient client = new HttpClient();

        public async Task SendMessage()
        {
            string apiUrl = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=USD";
            var myDeserializedClass = new List<Root>();
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            var data = await response.Content.ReadAsStringAsync();
            myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(data);

            var database = new MongoClient("mongodb://localhost:27017/test");
            var db = database.GetDatabase("Logs");
            var coll = db.GetCollection<Logs>("Logs");
            coll.InsertOneAsync(new Logs { Date = DateTime.Now, Crypto = myDeserializedClass });

            await Clients.All.SendAsync("ReceiveMessage", myDeserializedClass);
        }


        public class Logs
        {
            public DateTime Date { get; set; }
            public List<Root> Crypto { get; set; }
        }

        public class Roi
        {
            public double times { get; set; }
            public string currency { get; set; }
            public double percentage { get; set; }
        }

        public class Root
        {
            public string id { get; set; }
            public string symbol { get; set; }
            public string name { get; set; }
            public string image { get; set; }
            public double current_price { get; set; }
            public object market_cap { get; set; }
            public int market_cap_rank { get; set; }
            public long? fully_diluted_valuation { get; set; }
            public object total_volume { get; set; }
            public double high_24h { get; set; }
            public double low_24h { get; set; }
            public double price_change_24h { get; set; }
            public double price_change_percentage_24h { get; set; }
            public object market_cap_change_24h { get; set; }
            public double market_cap_change_percentage_24h { get; set; }
            public double circulating_supply { get; set; }
            public double? total_supply { get; set; }
            public double? max_supply { get; set; }
            public double ath { get; set; }
            public double ath_change_percentage { get; set; }
            public DateTime ath_date { get; set; }
            public double atl { get; set; }
            public double atl_change_percentage { get; set; }
            public DateTime atl_date { get; set; }
            public Roi roi { get; set; }
            public DateTime last_updated { get; set; }
        }
    }
}
