using System;
using System.Diagnostics;
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
        public async Task<JsonResult> CurrentCryptoPrice()
        {
            try
            {
                var result = await this._cmcService.GetTicker("bitcoin");
                return this.Json(result);
            }
            catch (Exception e)
            {
                // TODO: Document Exception
                return this.Json(new {error = e.Message});
            }
        }
    }
}
