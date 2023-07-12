using Microsoft.AspNetCore.Mvc;
using ModelValidationDemo.CustomModelBinders;
using ModelValidationDemo.Models;


namespace ModelValidationDemo.Controllers
{
    public class HomeController : Controller
    {
        [Route("register")]
        //public IActionResult Index([Bind(nameof(Person.PersonName), nameof(Person.Email), nameof(Person.Password), nameof(Person.ConfirmPassword))] Person person)
        //public IActionResult Index([FromBody] Person person)
        //public IActionResult Index([FromBody][ModelBinder(BinderType = typeof(PersonModelBinder))] Person person)
        public IActionResult Index(Person person, [FromHeader(Name = "User-Agent")] string userAgent)
        {

            if (!ModelState.IsValid)
            {
                string errors = string.Join("\n", ModelState.Values.SelectMany(modelProperty => modelProperty.Errors
                .Select(error => error.ErrorMessage)
                .ToList()));
                return BadRequest(errors);
            }

            // Old way
            //string userAgent = ControllerContext.HttpContext.Request.Headers["User-Agent"];

            return Content($"{person}User Agent: {userAgent}", "text/plain");
        }
    }
}
