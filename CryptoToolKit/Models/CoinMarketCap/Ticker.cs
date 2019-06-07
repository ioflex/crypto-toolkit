using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoToolKit.Models.CoinMarketCap
{
    public class Ticker
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public int Rank { get; set; }

        public double PriceUsd { get; set; }

        public double PriceBtc { get; set; }

        /// <summary>
        /// 24 Hour market volume.
        /// </summary>
        public double MarketVolume { get; set; }


        public double MarketCapUsd { get; set; }
    }
}
