using Microsoft.AspNetCore.Mvc;

namespace LayoutViewsDemo.Controllers
{
    public class ProductsController : Controller
    {
        [Route("products")]
        public IActionResult Index()
        {
            return View();
        }

        // Url: /search-products/1
        [Route("search-products/{productId?}")]
        public IActionResult Search(int? productId)
        {
            ViewBag.productId = productId;
            return View();
        }

        [Route("order-product")]
        public IActionResult Order()
        {
            return View();
        }
    }
}
