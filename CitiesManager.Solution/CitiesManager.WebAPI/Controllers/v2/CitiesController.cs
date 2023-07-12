using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CitiesManager.Infrastructure.DatabaseContext;

namespace CitiesManager.WebAPI.Controllers.v2
{
    [ApiVersion("2.0")]
    //public class CitiesController : Controller  // MVC Controller
    //public class CitiesController : ControllerBase  // Web API Controller
    public class CitiesController : CustomControllerBase
    {
        // Fields
        private readonly ApplicationDbContext _dbContext;

        // Constructors
        public CitiesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Methods

        // GET: api/Cities
        /// <summary>
        /// To get list of cities name from Cities table
        /// </summary>
        /// <returns>To return list of cities name that are available in Cities table</returns>
        [HttpGet]
        //[Produces("application/xml")]  // Response body content-type as application/xml
        //public async Task<IActionResult> GetAllCities()
        public async Task<ActionResult<IEnumerable<string?>>> GetAllCities() // I'm 100 % sure that method will return only ObjectResult, so i need to use ObjectResult<T> as return type
        {
            var cities = await _dbContext.Cities.OrderBy(temp => temp.CityName).Select(temp => temp.CityName).ToListAsync();
            //return Ok(cities);  // Automatically it will be converted into JSON format
            return cities;   // Automatically it will be converted into JSON format
        }

    }
}
