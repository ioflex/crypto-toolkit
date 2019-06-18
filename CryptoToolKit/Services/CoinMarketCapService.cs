using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            

            this.Client = client;
        }

        /// <summary>
        /// TODO: Method Documentation
        /// TODO: Check HTTP Status code on response
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="symbol"></param>
        /// <param name="toSymbols"></param>
        /// <returns></returns>
        public async Task<object> ConvertPriceAsync(double amount, string symbol, string[] toSymbols)
        {
            var convert = string.Empty;
            // TODO: To Symbol string conversion could be better, add support for user default crypto / fiat values.
            if (toSymbols != null)
            {
                for (var i = 0; i < toSymbols.Length; i++)
                {
                    if (i.Equals(0))
                        convert += toSymbols[i];
                    else
                        convert += $",{toSymbols[i]}";
                }
            }
            else
            {
                convert = "USD";
            }

            var response =
                await this.Client.GetAsync($"tools/price-conversion?amount={amount}&symbol={symbol}&convert={convert}");

            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                var content = await reader.ReadToEndAsync();
                var contentObj = JObject.Parse(content);

                var targetSymbol = (string) contentObj["data"]["symbol"];
                var targetAmount = (double) contentObj["data"]["amount"];
                var updated = (string) contentObj["data"]["last_updated"];
                var luResult = DateTime.TryParse(updated, out var lastUpdated);

                var conversions = new List<object>();
                foreach (var sym in toSymbols)
                {
                    var name = sym;
                    var price = (double) contentObj["data"]["quote"][sym]["price"];
                    var _ = (string) contentObj["data"]["quote"][sym]["last_updated"];
                    var result = DateTime.TryParse(_, out var lu);
                    conversions.Add(new{name,price,lastUpdated=result ? lu : DateTime.MinValue});
                }

                return new
                {
                    name = targetSymbol,
                    amount = targetAmount,
                    lastUpdated = luResult ? lastUpdated : DateTime.MinValue,
                    conversions = conversions
                };
            }
        }

        /// <summary>
        /// TODO: Method Documentation
        /// TODO: Check HTTP Status code on response
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
        /// TODO: Check HTTP Status code on response
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
