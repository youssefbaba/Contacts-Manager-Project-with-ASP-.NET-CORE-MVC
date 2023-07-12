using Microsoft.AspNetCore.Mvc;

namespace EnvironmentsDemo.Controllers
{
    public class HomeController : Controller
    {
        // Fields
        private readonly IWebHostEnvironment _webHostEnvironment;

        // Constructors
        public HomeController(IWebHostEnvironment webHostEnvironment) // Constructor Injection
        {
            _webHostEnvironment = webHostEnvironment;
        }


        // Methods
        [Route("/")]
        //[Route("some-route")]
        public IActionResult Index()
        {
            if (_webHostEnvironment.IsDevelopment())
            {
                // Some code here ...
            }

            ViewBag.CurrentEnvironment = _webHostEnvironment.EnvironmentName; 
            ViewBag.AbsolutePath = _webHostEnvironment.ContentRootPath; 
            return View();
        }

        [Route("some-route")]
        public IActionResult Other()
        {
            return View();
        }
    }
}
