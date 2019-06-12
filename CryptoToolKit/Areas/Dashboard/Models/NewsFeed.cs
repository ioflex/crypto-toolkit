using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoToolKit.Areas.Dashboard.Models
{
    public class NewsFeed
    {
        public NewsFeed(IEnumerable<Article> articles)
        {
            this.Articles = articles;
        }

        public IEnumerable<Article> Articles { get; set; }

        public int Count => Articles?.Count() ?? 0;
    }
}
