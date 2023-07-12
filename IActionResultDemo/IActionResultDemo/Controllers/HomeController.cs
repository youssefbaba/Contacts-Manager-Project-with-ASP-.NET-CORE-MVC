using Microsoft.AspNetCore.Mvc;

namespace IActionResultDemo.Controllers
{
    public class HomeController : Controller
    {
        [Route("bookstore")]
        // Url /bookstore?isloggedin=true&bookid=1
        public IActionResult Index()
        {
            // isloggedin should be supplied
            if (!ControllerContext.HttpContext.Request.Query.ContainsKey("isloggedin"))
            {
                //return BadRequest();
                return BadRequest("isloggedin is not supplied");
                //return StatusCode(400, "isloggedin is not supplied");
            }

            // isloggedin can't be empty
            if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.HttpContext.Request.Query["isloggedin"])))
            {
                //return BadRequest();
                return BadRequest("isloggedin can't be null or empty");
                //return StatusCode(400, "isloggedin can't be null or empty");
            }

            // User must be authenticated
            bool isloggedin = default;
            try
            {
                isloggedin = Convert.ToBoolean(Convert.ToString(ControllerContext.HttpContext.Request.Query["isloggedin"]));
            }
            catch (FormatException)
            {
                isloggedin = false;
            }
            if (!isloggedin)
            {
                //return Unauthorized();
                //return Unauthorized("User must be authenticated");
                return StatusCode(401, "User must be authenticated");
            }

            // Book id should be supplied
            if (!ControllerContext.HttpContext.Request.Query.ContainsKey("bookid"))
            {
                //return BadRequest();
                return BadRequest("Book id is not supplied");
                //return StatusCode(400, "Book id is not supplied");
            }

            // Book id can't be empty
            if (string.IsNullOrEmpty(Convert.ToString(ControllerContext.HttpContext.Request.Query["bookid"])))
            {
                //return BadRequest();
                return BadRequest("Book id can't be null or empty");
                //return StatusCode(400, "Book id can't be null or empty");
            }

            // Book id should be between 1 and 1000
            int bookid = Convert.ToInt32(ControllerContext.HttpContext.Request.Query["bookid"]);
            if (bookid <= 0)
            {
                //return BadRequest();
                return BadRequest("Book id can't be less or equal to zero");
                //return StatusCode(400, "Book id can't be less or equal to zero");
            }
            if (bookid > 1000)
            {
                //return NotFound();
                return NotFound("Book id can't be greater than 1000");
                //return StatusCode(404, "Book id can't be greater than 1000");
            }

            // if user authenticated and book id between 1 and 1000
            //return File("/docs.pdf", "application/pdf");


            // 302 - Found -  RedirectToActionResult
            //return new RedirectToActionResult(actionName:"Books", controllerName:"Store", routeValues: new { id = bookid }); // 302 - Found
            //return RedirectToAction(actionName: "Books", controllerName: "Store", routeValues: new { id = bookid }); // 302 - Found


            // 301 - Moved Permanently -  RedirectToActionResult
            //return new RedirectToActionResult(actionName: "Books", controllerName:"Store",routeValues:new { }, permanent: true); // 301 - Moved Permanently
            //return RedirectToActionPermanent(actionName: "Books", controllerName:"Store", routeValues:new { }) ; // 301 - Moved Permanently


            // 302 - Found -  LocalRedirectResult
            //return new LocalRedirectResult(localUrl: $"/store/books/{bookid}"); // 302 - Found
            //return LocalRedirect(localUrl: $"/store/books/{bookid}"); // 302 - Found

            // 301 - Moved Permanently -  LocalRedirectResult
            //return new LocalRedirectResult(localUrl: $"/store/books/{bookid}", permanent: true); // 301 - Moved Permanently
            //return LocalRedirectPermanent(localUrl: $"/store/books/{bookid}"); // 301 - Moved Permanently


            // 302 - Found -  RedirectResult
            //return new RedirectResult(url:"https://www.google.com/");
            //return Redirect(url:"https://www.google.com/");


            // 301 - Moved Permanent -  RedirectResult
            //return new RedirectResult(url:"https://www.google.com/", permanent: true);
            return RedirectPermanent(url:"https://www.google.com/");

        }
    }
}
