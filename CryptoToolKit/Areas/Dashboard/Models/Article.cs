using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using VaderSharp;

namespace CryptoToolKit.Areas.Dashboard.Models
{
    public class Article
    {
        public Article(string source, string author, string title, string description, string url, string published,
            string content)
        {
            this.Source = source.Trim();
            this.Author = author.Trim();
            this.Title = title.Trim();
            this.Description = description.Trim();
            this.Url = url.Trim();

            var isDate = DateTime.TryParse(published, out var date);
            this.Published = isDate ? date : DateTime.MinValue;
            this.Content = Sanitize(content);
            Analyze();
        }

        public string Source { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public DateTime Published { get; set; }

        public string Content { get; set; }

        private double _positive;

        public double Positive => Math.Round(Math.Abs(this._positive), 3);

        private double _negative;

        public double Negative => Math.Round(Math.Abs(this._negative), 3);

        private double _neutral;

        public double Neutral => Math.Round(Math.Abs(this._neutral), 3);

        private double _compound;

        public double Compound => Math.Round(Math.Abs(this._compound), 3);

        private void Analyze()
        {
            var analyzer = new SentimentIntensityAnalyzer();
            var results = analyzer.PolarityScores(this.Content);

            this._positive = results.Positive;
            this._negative = results.Negative;
            this._neutral = results.Neutral;
            this._compound = results.Compound;
        }

        private string Sanitize(string raw)
        {
            return string.IsNullOrWhiteSpace(raw)
                ? string.Empty
                : Regex.Replace(raw, @"(@[A-Za-z0-9]+)|([^0-9A-Za-z \t])|(\w+:\/\/\S+)", " ");
        }
    }
}
