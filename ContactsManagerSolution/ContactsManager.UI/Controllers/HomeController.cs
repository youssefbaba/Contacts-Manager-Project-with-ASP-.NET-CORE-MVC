using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManager.UI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [Route("Error")]  //Attribute Routing : /Error
        public IActionResult Error()
        {
            // gets current Exception Message
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature != null && exceptionHandlerPathFeature.Error != null)
            {
                ViewBag.ErrorMessage = exceptionHandlerPathFeature.Error.Message;
            }

            return View(viewName: "_Error");  // Views/Shared/_Error
        }
    }
}
