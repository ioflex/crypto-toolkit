using System;

namespace CryptoToolKit.Models.CoinMarketCap
{
    public class TickerSymbol
    {
        private double _dayPercentChange;

        /// <summary>
        /// Current Percent of change within 24hr period at a moment in time.
        /// Rounds to 2 decimal places.
        /// </summary>
        public double DayPercentChange
        {
            get => Math.Round(this._dayPercentChange, 2);

            set => this._dayPercentChange = value;
        }

        private double _price;

        /// <summary>
        /// Current Price of the Symbol at a moment in time.
        /// Rounds to 2 decimal places.
        /// </summary>
        public double Price
        {
            get => Math.Round(this._price, 2);

            set => this._price = value;
        }

        /// <summary>
        /// The Symbol used to represent the coin / token.
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// The moment in time.
        /// </summary>
        public DateTime LastUpdated { get; set; }
    }
}
