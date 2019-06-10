﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CryptoToolKit.Models;
using CryptoToolKit.Services;

namespace CryptoToolKit.Controllers
{
    public class HomeController : BaseController
    {
        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="cmcService"></param>
        /// <param name="naService"></param>
        public HomeController(CoinMarketCapService cmcService, NewsApiService naService)
        { 
            // TODO: Implement user settings ex: default fiat currency.
            this.Initialize(cmcService, naService);
        }

        /// <summary>
        /// Returns the Home-Index View.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Returns the Home-About View.
        /// </summary>
        /// <returns></returns>
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        /// <summary>
        /// Returns the Home-Contact View.
        /// </summary>
        /// <returns></returns>
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Returns the Error View.
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
        /// Returns the median price for crypto currency, in specified fiat.
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="fiat"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> CurrentCryptoPrice(string symbol, string fiat)
        {
            try
            {
                var tickerSymbol = await this.CoinMarketCapService.GetTickerAsync(symbol ?? "BTC", fiat);

                return OkJson(tickerSymbol);
            }
            catch (Exception e)
            {
                // TODO: Document Exception
                return ServerExceptionJson(e);
            }
        }

        /// <summary>
        /// TODO: Controller Action Documentations
        /// </summary>
        /// <returns></returns>
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
    }
}