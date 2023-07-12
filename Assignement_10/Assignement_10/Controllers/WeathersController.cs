using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace Assignement_10.Controllers
{
    public class WeathersController : Controller
    {
        // Fields
        private readonly IWeatherService _weatherService;


        // Constructors
        public WeathersController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            List<CityWeather> viewModel = _weatherService.GetWeatherDetails();

            return View(model: viewModel);
        }

        [HttpGet]
        [Route("/weather/{cityCode?}")]
        public IActionResult City(string? cityCode)
        {
            if (string.IsNullOrEmpty(cityCode))
            {
                ViewBag.ErrorMessage = "City code can't be null";
                return View(); // model = null
            }
            CityWeather? viewModel = _weatherService.GetWeatherByCityCode(cityCode);
            if (viewModel == null)
            {
                ViewBag.ErrorMessage = "No element matches the condition";
                return View(); // model = null
            }
            return View(model: viewModel);
        }
    }
}
