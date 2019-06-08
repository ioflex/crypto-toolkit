using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CryptoToolKit.Models;
using CryptoToolKit.Services;

namespace CryptoToolKit.Controllers
{
    public class HomeController : Controller
    {
        private readonly CoinMarketCapService _cmcService;

        public HomeController(CoinMarketCapService cmcService)
        {
            this._cmcService = cmcService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<JsonResult> CurrentCryptoPrice(string asset, string fiat)
        {
            try
            {
                var result = await this._cmcService.GetTicker(asset, fiat);

                //var target = tickerObjects.Where(kv => kv.Key.Trim().ToLower().Equals("price_usd"))
                //                         .Select(kv => kv.Value)
                //                         .FirstOrDefault();

                var price = Math.Round(Convert.ToDouble(result), 2);
                return this.Json(new {price});
            }
            catch (Exception e)
            {
                // TODO: Document Exception
                return this.Json(new {error = e.Message});
            }
        }
    }
}
