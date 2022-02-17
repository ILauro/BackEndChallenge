using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace BackEndChallenge.wwwroot
{
    public class HomeController : Controller
    {
        static HttpClient client = new HttpClient();

        [HttpPost]
        public  HttpResponseMessage GetConversion()
        {
            string apiUrl = "https://http://apilayer.net/api/live?access_key=3aa9ffd97951da7e6bc9696c17129bbd&currencies=EUR,GBP,CAD,PLN&source=USD&format=1";
            var myDeserializedClass = new ConversionResponse();
            //HttpResponseMessage response = await client.GetAsync(apiUrl);
            //var data = await response.Content.ReadAsStringAsync();
            //myDeserializedClass = JsonConvert.DeserializeObject<ConversionResponse>(data);

            return new HttpResponseMessage() { Content = new StringContent(JsonConvert.SerializeObject(new { Success = myDeserializedClass.success, Data = myDeserializedClass.quotes })) };
        }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Quotes
        {
            public double USDEUR { get; set; }
            public double USDGBP { get; set; }
            public double USDCAD { get; set; }
            public double USDPLN { get; set; }
        }

        public class ConversionResponse
        {
            public bool success { get; set; }
            public string terms { get; set; }
            public string privacy { get; set; }
            public int timestamp { get; set; }
            public string source { get; set; }
            public Quotes quotes { get; set; }
        }
    }
}
