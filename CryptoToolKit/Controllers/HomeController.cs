using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

using CryptoToolKit.Models;

namespace CryptoToolKit.Controllers
{
    public class HomeController : BaseController
    {
        /// <summary>
        /// CTOR
        /// </summary>
        public HomeController()
        { 

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
    }
}
