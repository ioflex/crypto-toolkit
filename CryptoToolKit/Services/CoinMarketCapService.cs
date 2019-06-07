using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
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
            client.BaseAddress = new Uri("https://api.coinmarketcap.com/v1/");

            client.DefaultRequestHeaders.Add("Accept", "application/json");

            this.Client = client;
        }

        /// <summary>
        /// TODO: Method Documentation
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public async Task<IEnumerable<object>> GetTicker(string asset)
        {
            var response = await Client.GetAsync($"ticker/{asset}");

            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                var content = await reader.ReadToEndAsync();

                var contentArray = JArray.Parse(content);

                var ret = new List<object>();
                foreach (var obj in contentArray.Children<JObject>())
                {
                    foreach (var prop in obj.Properties())
                    {
                        var name = prop.Name;
                        var value = prop.Value.ToString();
                        ret.Add(new { name, value });
                    }
                }

                return ret.ToArray();
            }
        }
    }
}
