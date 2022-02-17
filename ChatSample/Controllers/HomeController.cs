using BackEndChallenge.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace BackendChallenge.Controllers
{
    public class HomeController : Controller
    {
        static HttpClient client = new HttpClient();
        private string apiUrl = "http://api.currencylayer.com/live?access_key=3aa9ffd97951da7e6bc9696c17129bbd&currencies=USD,AUD,CAD,PLN,MXN&format=1";

        [HttpPost]
        public async Task<ConversionResponse> GetConversion()
        {
            var myDeserializedClass = new ConversionResponse();

            try
            {
                using (HttpResponseMessage response = await client.GetAsync(apiUrl))
                {
                    var data = await response.Content.ReadAsStringAsync();
                    
                    return JsonConvert.DeserializeObject<ConversionResponse>(data);
                }
            }
            catch (Exception ex)
            {
                return new ConversionResponse { success = false };
            }
            
        }
    }
}
