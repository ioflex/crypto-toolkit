using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CryptoToolKit.Areas.Dashboard.Models;
using Newtonsoft.Json.Linq;

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

        /// <summary>
        /// TODO: Method Documentation
        /// TODO: Check HTTP Status code on response
        /// TODO: Change keyword to string[] to enable multiple keywords?
        /// TODO: Make this method more configurable with options https://newsapi.org/docs/endpoints/top-headlines
        /// </summary>
        /// <param name="category"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<NewsFeed> GetNewsFeedAsync(string category, string keyword)
        {
            var response = await Client.GetAsync($"top-headlines?category={category}&q={keyword}");

            var articles = new List<Article>();
            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                var content = await reader.ReadToEndAsync();
                var contentObj = JObject.Parse(content);
                var articlesArray = JArray.Parse(contentObj["articles"].ToString());

                for (var i = 0; i < articlesArray.Count; i++)
                {
                    var articleObj = JObject.Parse(articlesArray[i].ToString());
                    articles.Add(new Article(
                        articleObj["source"]["name"].ToString(),
                        articleObj["author"].ToString(),
                        articleObj["title"].ToString(),
                        articleObj["description"].ToString(),
                        articleObj["url"].ToString(),
                        articleObj["publishedAt"].ToString(),
                        articleObj["content"].ToString()
                        ));
                }
            }

            return new NewsFeed(articles.OrderByDescending(article => article.Published).ToArray());
        }
    }
}
