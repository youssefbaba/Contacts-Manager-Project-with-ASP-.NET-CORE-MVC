using Microsoft.AspNetCore.Mvc;
using ViewsDemo.Models;

namespace ViewsDemo.Controllers
{
    public class ProductsController : Controller
    {
        [Route("all-products")]
        public IActionResult All()
        {
            return View();  
            // Views/Products/All.cshtml 
            // Views/Shared/All.cshtml
        }

        [Route("products-details")]
        public IActionResult Details()
        {
            return View();
        }
    }
}
