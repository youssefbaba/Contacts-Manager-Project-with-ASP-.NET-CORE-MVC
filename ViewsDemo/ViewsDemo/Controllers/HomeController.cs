using Microsoft.AspNetCore.Mvc;

namespace ViewsDemo.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        [Route("home")]
        public IActionResult Index()
        {
            return View(); // Views/Home/Index.cshtml
            //return View("home"); // Views/Home/home.cshtml
            //return new ViewResult() { ViewName = "home"};   //  Views/Home/home.cshtml
        }
    }
}
