using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace CryptoToolKit.Services
{
    /// <summary>
    /// TODO: Class Documentation
    /// </summary>
    public class CoinMarketCapService
    {
        /// <summary>
        /// TODO: Property Documentation
        /// </summary>
        public HttpClient Client { get; set; }

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="client"></param>
        public CoinMarketCapService(HttpClient client)
        {
            // *** Coin Market Cap API base url ***
            client.BaseAddress = new Uri("https://pro-api.coinmarketcap.com/v1/");

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", "d409d0cd-4277-464b-8542-2fd3ed7bf7e6");

            this.Client = client;
        }

        /// <summary>
        /// TODO: Method Documentation
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public async Task<double> GetTicker(string symbol, string fiat)
        {
            var response = await Client.GetAsync($"cryptocurrency/quotes/latest?symbol={symbol}");

            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                var content = await reader.ReadToEndAsync();

                var contentObj = JObject.Parse(content);

                var price = (double) contentObj["data"][symbol]["quote"][fiat]["price"];

                return price;
            }
        }

        //public async Task<IEnumerable<object>> GetListings()
        //{
        //    var response = await Client.GetAsync("")
        //}
    }
}
