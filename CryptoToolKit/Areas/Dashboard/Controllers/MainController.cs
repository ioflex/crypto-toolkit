using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using CryptoToolKit.Controllers;
using CryptoToolKit.Services;

namespace CryptoToolKit.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class MainController : BaseController
    {
        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="cmcService"></param>
        /// <param name="naService"></param>
        public MainController(CoinMarketCapService cmcService, NewsApiService naService)
        {
            this.Initialize(cmcService, naService);
        }

        /// <summary>
        /// Returns the Main.cshtml view
        /// </summary>
        /// <returns></returns>
        public IActionResult Main()
        {
            return View();
        }

        /// <summary>
        /// Returns the amount specified by <paramref name="amount"/> of the symbol specified by <paramref name="symbol"/>
        /// in the specified set of symbols <paramref name="toSymbols"/>
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="symbol"></param>
        /// <param name="toSymbols"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> ConvertPrice(double amount, string symbol, string[] toSymbols)
        {
            try
            {
                var priceConversion = await this.CoinMarketCapService.ConvertPriceAsync(amount, symbol, toSymbols);
                return OkJson(priceConversion);
            }
            catch (Exception e)
            {
                // TODO: Document Exception
                return ServerExceptionJson(e);
            }
        }

        /// <summary>
        /// TODO: Controller Action Documentation
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> CurrentListings()
        {
            try
            {
                var listings = await this.CoinMarketCapService.GetListingsAsync();
                return OkJson(listings);
            }
            catch (Exception e)
            {
                // TODO: Document Exception
                return ServerExceptionJson(e);
            }
        }

        /// <summary>
        /// Returns the market information for specified crypto symbol, in specified fiat, at a specific point in time.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="fiat"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> MarketInformation(string symbol, string fiat)
        {
            try
            {
                var ticker = await this.CoinMarketCapService.GetTickerAsync(symbol, fiat);
                return OkJson(ticker);
            }
            catch (Exception e)
            {
                // TODO: Document Exception
                return ServerExceptionJson(e);
            }
        }

        /// <summary>
        /// TODO: Controller Action Documentation
        /// </summary>
        /// <param name="category"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<JsonResult> NewsFeed(string category, string keyword)
        {
            try
            {
                var newsFeed = await this.NewsApiService.GetNewsFeedAsync(category, keyword);
                return OkJson(newsFeed);
            }
            catch (Exception e)
            {
                // TODO: Document Exception
                return ServerExceptionJson(e);
            }
        }
    }
}