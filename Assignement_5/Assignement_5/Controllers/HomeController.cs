using Assignement_5.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignement_5.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        [Route("/order")]    
        public IActionResult Index([Bind(nameof(Order.OrderDate), nameof(Order.InvoicePrice), nameof(Order.Products))]Order order)
        {
            if (!ModelState.IsValid)
            {
                string errors = string.Join("\n", ModelState.Values.SelectMany(property => property.Errors)
                      .Select(error => error.ErrorMessage).ToList());
                return BadRequest(errors);
            }
            Random randomNumber = new Random();
            int orderNumber = randomNumber.Next(1, 100000);
            return Json(new { orderNumber });   
        }
    }
}
