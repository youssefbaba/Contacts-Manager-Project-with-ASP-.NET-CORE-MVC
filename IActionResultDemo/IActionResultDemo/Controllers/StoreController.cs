using Microsoft.AspNetCore.Mvc;

namespace IActionResultDemo.Controllers
{
    public class StoreController : Controller
    {
        [Route("/store/books/{id}")]
        public IActionResult Books()
        {
            int bookid = Convert.ToInt32(ControllerContext.HttpContext.Request.RouteValues["id"]);
            return Content($"<h1>Book Store - {bookid}</h1>", "text/html");  // 200 - Ok
        }
    }
}
