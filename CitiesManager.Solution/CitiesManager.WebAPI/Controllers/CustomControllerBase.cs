using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CitiesManager.WebAPI.Controllers
{
    // Web API controller by default tends to take (Request) and to send (Response) only Json type

    [ApiController] // Specifies that these action methods are going to be web API action methods but not regular MVC controller action methods
                    // Enforces the developer to use attribute routing without fail (to use route attribute either on controller or action method)
                    // Automatically returns bad request result in case of any errors found in the model state
                    // I don't need to add [FromBody] attribute to make model binding , it will be made automatically

    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    public class CustomControllerBase : ControllerBase  // Web API controller
    {
    }
}
