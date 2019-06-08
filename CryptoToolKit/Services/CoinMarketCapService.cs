using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CryptoToolKit.Models.CoinMarketCap;
using Newtonsoft.Json.Linq;

namespace CryptoToolKit.Services
{
    /// <summary>
    /// TODO: Class Documentation
    /// </summary>
    public class CoinMarketCapService
    {
        /// <summary>
        /// HttpClient used for web requests.
        /// </summary>
        public HttpClient Client { get; set; }

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="client"><see cref="HttpClient"/></param>
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
        /// <param name="symbol"></param>
        /// <param name="fiat"></param>
        /// <returns></returns>
        public async Task<TickerSymbol> GetTickerAsync(string symbol, string fiat)
        {
            var response = await Client.GetAsync($"cryptocurrency/quotes/latest?symbol={symbol}&convert={fiat}");

            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                var content = await reader.ReadToEndAsync();
                var contentObj = JObject.Parse(content);
                var price = (double) contentObj["data"][symbol]["quote"][fiat]["price"];
                var percentChange = (double)contentObj["data"][symbol]["quote"][fiat]["percent_change_24h"];
                var lastUpdated = (string)contentObj["data"][symbol]["quote"][fiat]["last_updated"];
                var result = DateTime.TryParse(lastUpdated, out var lu);

                return new TickerSymbol
                {
                    DayPercentChange = percentChange,
                    Price = price,
                    LastUpdated = result ? lu : DateTime.Now
                };
            }
        }

        /// <summary>
        /// TODO: Method Documentation
        /// </summary>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<IEnumerable<object>> GetListingsAsync(int start = 1, int limit = 1000)
        {
            var response = await Client.GetAsync($"cryptocurrency/listings/latest?start={start}&limit={limit}");

            var ret = new List<object>();

            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                var content = await reader.ReadToEndAsync();

                var contentObj = JObject.Parse(content);

                var dataArray = JArray.Parse(contentObj["data"].ToString());

                
                for(var i = 0; i < dataArray.Count; i++)
                {
                    var listing = JObject.Parse(dataArray[i].ToString());
                    ret.Add(new { symbol = listing["symbol"], firstDate = listing["first_historical_data"], lastDate = listing["last_historical_data"] });
                }
            }

            return ret.ToArray();
        }
    }
}
