using ContactsManager.Core.DTO;
using ContactsManager.Core.ServiceContracts;
using ContactsManager.UI.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ContactsManager.UI.Filters.ActionFilters
{
    public class PersonCreateAndEditPostActionFilter : IAsyncActionFilter
    {
        // Fields
        private readonly ICountriesGetterService _countriesGetterService;
        private readonly ILogger<PersonCreateAndEditPostActionFilter> _logger;

        // Constructors
        public PersonCreateAndEditPostActionFilter(ICountriesGetterService countriesGetterService,
            ILogger<PersonCreateAndEditPostActionFilter> logger
            )
        {
            _countriesGetterService = countriesGetterService;
            _logger = logger;
        }

        // Methods

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // To Do: before logic here
            _logger.LogInformation("before logic of {FilterName}.{MethodName} method", nameof(PersonCreateAndEditPostActionFilter), nameof(OnActionExecutionAsync));
            if (context.Controller is PersonsController personsController)
            {
                if (!personsController.ModelState.IsValid)
                {
                    personsController.ViewBag.Errors = personsController.ModelState.Values.SelectMany(value => value.Errors)
                        .Select(error => error.ErrorMessage)
                        .ToList();

                    List<CountryResponse> countries = await _countriesGetterService.GetAllCountries();
                    personsController.ViewBag.Countries = countries;

                    if (context.ActionArguments.ContainsKey("personRequest"))
                    {
                        var personRequest = context.ActionArguments["personRequest"];
                        context.Result = personsController.View(personRequest); // short-circuits or skips the subsequent filters or action method
                    }
                }
                else
                {
                    await next(); // calls the subsequent filters or action method
                }
            }
            else
            {
                await next(); // calls the subsequent filters or action method
            }
        }
    }
}
