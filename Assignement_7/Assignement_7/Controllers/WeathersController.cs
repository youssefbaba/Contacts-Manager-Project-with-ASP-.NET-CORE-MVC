using Assignement_7.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignement_7.Controllers
{
    public class WeathersController : Controller
    {

        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            List<CityWeather> viewModel = GetData().ToList();

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
            CityWeather? viewModel = GetData().FirstOrDefault(temp => string.Equals(temp.CityUniqueCode, cityCode, StringComparison.OrdinalIgnoreCase));
            if (viewModel == null)
            {
                ViewBag.ErrorMessage = "No element matches the condition";
                return View(); // model = null
            }
            return View(model: viewModel);
        }

        // Local Function
        private static IEnumerable<CityWeather> GetData()
        {
            return new List<CityWeather>()
            {
                new CityWeather()
                {
                    CityUniqueCode = "LDN", CityName = "London", DateAndTime = Convert.ToDateTime("2030-01-01 8:00"),  TemperatureFahrenheit = 33
                },
                new CityWeather()
                {
                    CityUniqueCode = "NYC", CityName = "New York", DateAndTime = Convert.ToDateTime("2030-01-01 3:00"),  TemperatureFahrenheit = 60
                },
                new CityWeather()
                {
                    CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = Convert.ToDateTime("2030-01-01 9:00"),  TemperatureFahrenheit = 82
                }
            };
        }
    }
}
