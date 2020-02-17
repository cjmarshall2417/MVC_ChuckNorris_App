using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Chuck_Norris.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Newtonsoft.Json;

namespace Chuck_Norris.Controllers
{
    public class HomeController : Controller
    {
        public HttpClient httpClient = new HttpClient();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        //This method accesses the api using the given URL and returns the JSON value returned from the API request
        public async Task<JsonResult> MakeAPIRequest(string URL)
        {
            var response = await httpClient.GetAsync(URL);
            string responseBody = await response.Content.ReadAsStringAsync();
            var returnValue = Json(responseBody);
            return returnValue;
        }

        public async Task<JsonResult> GetRandomJoke()
        {
            return await MakeAPIRequest("https://api.chucknorris.io/jokes/random");
        }

        public async Task<JsonResult> SearchForJoke(string search)
        {
            return await MakeAPIRequest("https://api.chucknorris.io/jokes/search?query=" + search);
        }

        public async Task<JsonResult> GetCategories()
        {
            return await MakeAPIRequest("https://api.chucknorris.io/jokes/categories");
        }

        public async Task<JsonResult> GetCategoryJoke(string selectedCategory)
        {
            return await MakeAPIRequest("https://api.chucknorris.io/jokes/random?category=" + selectedCategory);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
