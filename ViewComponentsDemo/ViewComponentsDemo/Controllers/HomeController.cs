using Microsoft.AspNetCore.Mvc;
using ViewComponentsDemo.Models;

namespace ViewComponentsDemo.Controllers
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

        [Route("contact")]
        public IActionResult Contact()
        {
            return View();
        }


        [Route("load-team")]
        public IActionResult LoadTeam()
        {
            PersonGridModel personGridModel = new PersonGridModel()
            {
                GridTitle = "Team",
                People = new List<Person>()
                {
                    new Person(){ PersonName = "Daniel", JobTitle = "Developer"},
                    new Person(){ PersonName = "Thomas", JobTitle = "Designer"},
                    new Person(){ PersonName = "Benjamin", JobTitle = "Manager"}
                }
            };
            return ViewComponent(componentName: "Grid", arguments: new { gridModel  = personGridModel });
        }
    }
}
