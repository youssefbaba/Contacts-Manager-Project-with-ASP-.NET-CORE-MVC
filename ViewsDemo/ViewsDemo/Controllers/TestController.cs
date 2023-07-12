using Microsoft.AspNetCore.Mvc;
using ViewsDemo.Models;

namespace ViewsDemo.Controllers
{
    public class TestController : Controller
    {
        [Route("/test")]
        public IActionResult Index()
        {
            /*
            ViewData["appTitle"] = "Asp Dot Net Core Demo App";  // ViewData => Dictionary<string, object?>
            ViewData["people"] = GetData().ToList();
            */
            ViewBag.appTitle = "Asp Dot Net Core Demo App";  // ViewBag => dynamic
            /*
            ViewBag.people = GetData().ToList();
            */
            List<Person> people = GetData().ToList();
            return View(viewName: "Index", model: people);
        }

        [Route("person-details/{name}")]
        public IActionResult Details(string? name)
        {
            ViewBag.pageTitle = "Person Details";
            if (name == null)
            {
                return Content("Person name can't be null"); ;
            }
            if (GetData().Count() == 0)
            {
                return Content("Data source is empty");
            }
            Person? matchingPerson = GetData().FirstOrDefault(temp => string.Equals(temp.PersonName, name, StringComparison.OrdinalIgnoreCase));
            if (matchingPerson == null)
            {
                return Content("No element matches the condition");
            }
            return View(model: matchingPerson); // View/Test/Details.cshtml
        }

        [Route("person-with-product")]
        public IActionResult PersonWithProduct()
        {
            Person person = new Person() { PersonName = "William Bernard", DateOfBirth = Convert.ToDateTime("1990-06-25"), PersonGender = Gender.Male };
            Product product = new Product() { ProductId = 101, ProductName = "Laptop", Price = 1200 };

            PersonAndProductWrapperModel viewModel = new PersonAndProductWrapperModel()
            {
                PersonData = person,
                ProductData = product
            };

            return View(model: viewModel);
        }

        [Route("test/all-products")]
        public IActionResult All()
        {
            return View();
            // Views/Test/All.cshtml 
            // Views/Shared/All.cshtml
        }


        // Local Function
        public static IEnumerable<Person> GetData()
        {
            return new List<Person>()
            {
              new Person(){PersonName = "Adam Lambert", DateOfBirth = Convert.ToDateTime("1996-01-05"), PersonGender = Gender.Male},
              new Person(){PersonName = "Sara Smith", DateOfBirth = Convert.ToDateTime("1992-06-15"), PersonGender = Gender.Female},
              new Person(){PersonName = "John Doe", DateOfBirth = null, PersonGender = Gender.Other}
            };
        }
    }
}
