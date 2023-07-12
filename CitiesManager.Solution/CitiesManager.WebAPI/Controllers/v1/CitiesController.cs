using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CitiesManager.Core.Domain.Entities;
using CitiesManager.Infrastructure.DatabaseContext;
using Microsoft.IdentityModel.Tokens;

namespace CitiesManager.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
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
        /// To get list of cities (Including CityID and CityName) from Cities table
        /// </summary>
        /// <returns>To return list of cities (Including CityID and CityName) available in Cities table</returns>
        [HttpGet]
        //[Produces("application/xml")]  // Response body content-type as application/xml
        //public async Task<IActionResult> GetAllCities()
        public async Task<ActionResult<IEnumerable<City>>> GetAllCities() // I'm 100 % sure that method will return only ObjectResult, so i need to use ObjectResult<T> as return type
        {
            var cities = await _dbContext.Cities.OrderBy(temp => temp.CityName).ToListAsync();
            //return Ok(cities);  // Automatically it will be converted into JSON format
            return cities;   // Automatically it will be converted into JSON format
        }

        // GET: api/Cities/9F49BB31-89C5-4D4A-A20D-A1FA50F603A1
        [HttpGet("{cityID}")]
        //public async Task<IActionResult> GetCityByCityID(Guid cityID)
        public async Task<ActionResult<City>> GetCityByCityID(Guid cityID)  // I'm 100 % sure that method will return only ObjectResult, so i need to use ObjectResult<T> as return type
        {
            var city = await _dbContext.Cities.FirstOrDefaultAsync(temp => temp.CityID == cityID);

            if (city == null)
            {
                if (!_dbContext.Cities.IsNullOrEmpty())
                {
                    // To report the error to the client
                    return Problem(detail: "Invalid cityID parameter", statusCode: 400, title: "City Search");  // Creates an object of ProblemDetails and convert the same to JSON format

                    //return BadRequest(); //Response.StatusCode = StatusCodes.Status400BadRequest
                }
                return Problem(detail: "Cities table is empty", statusCode: 404, title: "City Search");  // Creates an object of ProblemDetails and convert the same to JSON format

                //return NotFound(); //Response.StatusCode = StatusCodes.Status404NotFound
            }

            //return Ok(city);   // Automatically it will be converted into JSON format
            return city;   // Automatically it will be converted into JSON format

        }

        // PUT: api/Cities/9F49BB31-89C5-4D4A-A20D-A1FA50F603A1
        [HttpPut("{cityID}")]
        public async Task<IActionResult> PutCity(Guid cityID, [Bind(nameof(City.CityID), nameof(City.CityName))] City city)  // we can add this [Bind] attribute to avoid overposting operation
        {
            if (cityID != city.CityID)
            {
                return BadRequest(); //Response.StatusCode = StatusCodes.Status400BadRequest
            }

            var existingCity = await _dbContext.Cities.FirstOrDefaultAsync(temp => temp.CityID == cityID);

            if (existingCity == null)
            {
                if (!_dbContext.Cities.IsNullOrEmpty())
                {
                    return BadRequest();
                }
                return NotFound();
            }

            existingCity.CityName = city.CityName;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(cityID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Response.StatusCode = StatusCodes.Status204NoContent;  (operation completed successfully and return value is empty) 
        }

        // POST: api/Cities
        [HttpPost]
        //[Consumes("application/xml")]  // Request body content-type as application/xml
        //public async Task<IActionResult> PostCity([Bind(nameof(City.CityID), nameof(City.CityName))] City city)
        public async Task<ActionResult<City>> PostCity([Bind(nameof(City.CityID), nameof(City.CityName))] City city)
        {
            /*
             // automatically done by API Controller
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            */

            _dbContext.Cities.Add(city);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetCityByCityID", new { cityID = city.CityID }, city);
            // "GetCityByCityID", new { cityID = city.CityID } => it generates location response header = (Get): https://localhost:7030/api/Cities/9363c529-868d-4d02-922a-bf8203acb8b1
            // city it is the newly added city that will be returned as response body


            //var currentRequestUrl = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}");
            //return Created(new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}"), city);
            // new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}") => currentRequestUrl => it generates location response header = (Get): https://localhost:7030/api/Cities/9363c529-868d-4d02-922a-bf8203acb8b1
            // city it is the newly added city that will be returned as response body
        }

        // DELETE: api/Cities/9F49BB31-89C5-4D4A-A20D-A1FA50F603A1
        [HttpDelete("{cityID}")]
        public async Task<IActionResult> DeleteCity(Guid cityID)
        {
            var city = await _dbContext.Cities.FirstOrDefaultAsync(temp => temp.CityID == cityID);
            if (city == null)
            {
                if (!_dbContext.Cities.IsNullOrEmpty())
                {
                    return BadRequest();
                }
                return NotFound();
            }

            _dbContext.Cities.Remove(city);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        // Local Function
        private bool CityExists(Guid cityID)
        {
            return (_dbContext.Cities?.Any(e => e.CityID == cityID)).GetValueOrDefault();
        }
    }
}
