using Microsoft.AspNetCore.Mvc;
using ModelBindingDemo.Models;

namespace ModelBindingDemo.Controllers
{
    public class HomeController : Controller
    {
        [Route("bookstore/{isloggedin?}/{bookid?}")]
        // Url /bookstore?isloggedin=true&bookid=1
        public IActionResult Index([FromRoute] bool? isloggedin, [FromRoute] int? bookid, Book book)
        {
            // isloggedin should be supplied and valid
            if (!isloggedin.HasValue)
            {
                return BadRequest("isloggedin is not supplied or empty or invalid");
            }

            // User must be authenticated
            if (!isloggedin.Value)
            {
                return Unauthorized("User must be authenticated");
            }

            // Book id should be supplied and valid
            if (!bookid.HasValue)
            {
                return BadRequest("Book id is not supplied or empty or invalid");
            }

            // Book id should be between 0 and 1000
            if (bookid < 0 || bookid > 1000)
            {
                return NotFound("Book id should be between 1 and 1000");
            }

            return Content($"{book}", "text/plain");
        }
    }
}

