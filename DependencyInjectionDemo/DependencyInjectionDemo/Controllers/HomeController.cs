using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ServiceContracts;
using ServiceLayer;

namespace DependencyInjectionDemo.Controllers
{
    public class HomeController : Controller
    {

        // Fields
        private readonly ICitiesService _citiesServiceOne;
        private readonly ICitiesService _citiesServiceTwo;
        private readonly ICitiesService _citiesServiceThree;
        //private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILifetimeScope _lifetimeScope;


        // Constructors
        public HomeController(ICitiesService citiesServiceOne,
            ICitiesService citiesServiceTwo,
            ICitiesService citiesServiceThree,
            //IServiceScopeFactory serviceScopeFactory,
            ILifetimeScope lifetimeScope
            ) // Constructor Injection
        {
            // Create object of CitiesService class
            _citiesServiceOne = citiesServiceOne;
            _citiesServiceTwo = citiesServiceTwo;
            _citiesServiceThree = citiesServiceThree;
            //_serviceScopeFactory = serviceScopeFactory;
            _lifetimeScope = lifetimeScope;
        }


        // Methods
        [Route("/")]
        //public IActionResult Index([FromServices] ICitiesService _citiesService)   // Method Injection
        public IActionResult Index()   // Method Injection
        {
            // invoke the service layer
            List<string> cities = _citiesServiceOne.GetCities();
            ViewBag.InstanceId_CitiesServiceOne = _citiesServiceOne.ServiceInstanceId;
            ViewBag.InstanceId_CitiesServiceTwo = _citiesServiceTwo.ServiceInstanceId;
            ViewBag.InstanceId_CitiesServiceThree = _citiesServiceThree.ServiceInstanceId;

            // Create childscope
            //using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            using (ILifetimeScope scope = _lifetimeScope.BeginLifetimeScope())
            {
                // inject CitiesService
                //ICitiesService citiesService = scope.ServiceProvider.GetRequiredService<ICitiesService>();
                ICitiesService citiesService = scope.Resolve<ICitiesService>();

                // Db work 

                ViewBag.InstanceId_CitiesServiceInChildScope = citiesService.ServiceInstanceId;

            }// end of the scope, it calls CitiesService.Dispose() automatically

            return View(model: cities);
        }

    }
}
