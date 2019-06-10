using System;
using CryptoToolKit.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace CryptoToolKit.Controllers
{
    public abstract class BaseController : Controller
    {
        // TODO: Current User settings object that is populated on controller instantiation.

        /// <summary>
        /// TODO: Property Documentation
        /// </summary>
        protected CoinMarketCapService CoinMarketCapService = null;

        /// <summary>
        /// TODO: Property Documentation
        /// </summary>
        protected NewsApiService NewsApiService = null;

        /// <summary>
        /// TODO: Method Documentation
        /// </summary>
        /// <param name="coinMarketCapService"></param>
        protected void Initialize(CoinMarketCapService coinMarketCapService)
        {
            this.CoinMarketCapService = coinMarketCapService;
        }

        /// <summary>
        /// TODO: Method Documentation
        /// </summary>
        /// <param name="newsApiService"></param>
        protected void Initialize(NewsApiService newsApiService)
        {
            this.NewsApiService = newsApiService;
        }

        /// <summary>
        /// TODO: Method Documentation
        /// </summary>
        /// <param name="coinMarketCapService"></param>
        /// <param name="newsApiService"></param>
        protected void Initialize(CoinMarketCapService coinMarketCapService, NewsApiService newsApiService)
        {
            this.CoinMarketCapService = coinMarketCapService;
            this.NewsApiService = newsApiService;
        }

        /// <summary>
        /// TODO: Method Documentation
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected JsonResult OkJson(string message)
        {
            if (Response != null)
                Response.StatusCode = StatusCodes.Status200OK;

            return this.Json(new { result = message, time = DateTime.Now });
        }

        /// <summary>
        /// TODO: Method Documentation
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected JsonResult OkJson(object data)
        {
            if (Response != null)
                Response.StatusCode = StatusCodes.Status200OK;

             return this.Json(new { result = data , time = DateTime.Now});
        }

        /// <summary>
        /// TODO: Method Documentation
        /// TODO: Database Logging?
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected JsonResult BadRequestJson(string message)
        {
            if (Response != null)
                Response.StatusCode = StatusCodes.Status400BadRequest;

            return this.Json(new { result = message, time = DateTime.Now });
        }

        /// <summary>
        /// TODO: Method Documentation
        /// TODO: Database Logging!
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        protected JsonResult ServerExceptionJson(Exception exception)
        {
            if (Response != null)
                Response.StatusCode = StatusCodes.Status500InternalServerError;

            return this.Json(new { result = exception.Message, time = DateTime.Now });
        }
    }
}