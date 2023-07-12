using ConfigurationDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfigurationDemo.Controllers
{
    public class HomeController : Controller
    {
        // Fields
        private readonly IConfiguration _configuration;
        private readonly WeatherAPIOptions _weatherAPIOptions;


        // Constructors
        public HomeController(IConfiguration configuration,
            IOptions<WeatherAPIOptions> weatherAPIOptions
            )
        {
            _configuration = configuration;
            _weatherAPIOptions = weatherAPIOptions.Value;
        }


        // Methods
        [Route("/")]
        public IActionResult Index()
        {
            //ViewBag.MyKey = _configuration["MyKey"];
            ViewBag.MyKey = _configuration.GetValue<string>("MyKey");
            ViewBag.MyAPIKey = _configuration.GetValue<string>("MyAPIKey", "the default key");
            return View();
        }

        [Route("weather-details")]
        public IActionResult Details()
        {
            /*
            // First way to read the hierarchical configuration
            ViewBag.ClientID = _configuration["WeatherAPI:ClientID"];
            ViewBag.ClientSecret = _configuration["WeatherAPI:ClientSecret"];
            */

            /*
            // Second way to read the hierarchical configuration
            ViewBag.ClientID = _configuration.GetValue<string>("WeatherAPI:ClientID");
            ViewBag.ClientSecret = _configuration.GetValue<string>("WeatherAPI:ClientSecret");
            */

            IConfigurationSection weatherAPISection = _configuration.GetSection("WeatherAPI");
            /*
            // Third way to read the hierarchical configuration
            ViewBag.ClientID = weatherAPISection["ClientID"];
            ViewBag.ClientSecret = weatherAPISection["ClientSecret"];
            */

            /*
            // Fourth way to read the hierarchical configuration
            ViewBag.ClientID = weatherAPISection.GetValue<string>("ClientID");
            ViewBag.ClientSecret = weatherAPISection.GetValue<string>("ClientSecret");
            */

            /*
            // Bind : loads the configuration settings into new object
            WeatherAPIOptions weatherAPIOptions = new WeatherAPIOptions();
            _configuration.GetSection("WeatherAPI").Bind(WeatherAPIOptions);
            ViewBag.ClientID = weatherAPIOptions.ClientID;
            ViewBag.ClientSecret = weatherAPIOptions.ClientSecret;
            */

            /*
            // Get : loads the configuration settings into existing object
            WeatherAPIOptions weatherAPIOptions = _configuration.GetSection("WeatherAPI").Get<WeatherAPIOptions>();
            ViewBag.ClientID = weatherAPIOptions.ClientID;
            ViewBag.ClientSecret = weatherAPIOptions.ClientSecret;
            */

            // Configuration as Service
            ViewBag.ClientID = _weatherAPIOptions.ClientID;
            ViewBag.ClientSecret = _weatherAPIOptions.ClientSecret;


            return View();
        }
    }
}
