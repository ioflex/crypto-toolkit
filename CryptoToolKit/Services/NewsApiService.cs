using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoToolKit.Services
{
    public class NewsApiService
    {

        public HttpClient Client { get; set; }

        public NewsApiService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://newsapi.org/v2/");
            client.DefaultRequestHeaders.Add("X-Api-Key", "af402fb386a04966b4c438dccdb1dcb1");

            this.Client = client;
        }
    }
}
