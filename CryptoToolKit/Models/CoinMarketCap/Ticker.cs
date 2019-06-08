using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Newtonsoft.Json;

namespace CryptoToolKit.Models.CoinMarketCap
{
    public class Ticker
    {
        [JsonProperty("data")]
        public TickerItem Data { get; set; }
    }

    public class TickerItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("cmc_rank")]
        public string Rank { get; set; }

        [JsonProperty("num_market_pairs")]
        public int NumberOfPairs { get; set; }

        [JsonProperty("circulating_supply")]
        public long CirculatingSupply { get; set; }

        [JsonProperty("total_supply")]
        public long TotalSupply { get; set; }

        [JsonProperty("max_supply")]
        public long MaxSupply { get; set; }

        [JsonProperty("last_update")]
        public DateTime LastUpdated { get; set; }

        [JsonProperty("date_added")]
        public DateTime DateAdded { get; set; }

        [JsonProperty("tags")]
        public IEnumerable<string> Tags { get; set; }

        [JsonProperty("platform")]
        public string Platform { get; set; }

        [JsonProperty("quote")]
        public Quote CurrentQuote { get; set; }

    }

    public class Quote
    {
        [JsonProperty("USD")]
        public Usd USD { get; set; }

        [JsonProperty("BTC")]
        public Btc BTC { get; set; }
    }

    public class Btc
    {
        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("volume_24h")]
        public long Volume { get; set; }

        [JsonProperty("percent_change_1h")]
        public double HourPercentChange { get; set; }

        [JsonProperty("percent_change_24h")]
        public double DayPercentChange { get; set; }

        [JsonProperty("percent_change_7d")]
        public double WeekPercentChange { get; set; }

        [JsonProperty("last_updated")]
        public double MarketCapitalization { get; set; }
    }

    public class Usd
    {
        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("volume_24h")]
        public long Volume { get; set; }

        [JsonProperty("percent_change_1h")]
        public double HourPercentChange { get; set; }

        [JsonProperty("percent_change_24h")]
        public double DayPercentChange { get; set; }

        [JsonProperty("percent_change_7d")]
        public double WeekPercentChange { get; set; }

        [JsonProperty("last_updated")]
        public double MarketCapitalization { get; set; }
    }
}
