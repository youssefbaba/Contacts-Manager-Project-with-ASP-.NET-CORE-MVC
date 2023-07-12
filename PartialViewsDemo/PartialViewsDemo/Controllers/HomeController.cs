using Microsoft.AspNetCore.Mvc;
using PartialViewsDemo.Models;

namespace PartialViewsDemo.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("about")]
        public IActionResult About()
        {
            return View();
        }

        [Route("programming-languages")]
        public IActionResult ProgrammingLanguages()
        {
            ListModel listModel = new ListModel()
            {
                ListTitle = "Programming Languages List",
                ListItems = new List<string>()
                {
                    "C#",
                    "Java",
                    "Python",
                    "Go",
                    "TypeScript"
                }
            };
            return PartialView(viewName: "_ListPartialView", model: listModel);
        }
    }
}
