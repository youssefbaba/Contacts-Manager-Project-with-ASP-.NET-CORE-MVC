using CRUDExample.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace CRUDExample.Filters.ActionFilters
{
    public class PersonsListActionFilter : IAsyncActionFilter , IOrderedFilter
    {
        // Fields
        private readonly ILogger<PersonsListActionFilter> _logger;

        // Properties
        public int Order { get; set; }

        // Constructors
        public PersonsListActionFilter(ILogger<PersonsListActionFilter> logger, int order)
        {
            _logger = logger;
            Order = order;
        }

        // Methods
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // To do: add before logic here
            _logger.LogInformation("Before logic of {FilterName}.{MethodName} method", nameof(PersonsListActionFilter), nameof(OnActionExecutionAsync));
            _logger.LogDebug("Order: {OrderProperty}", Order);

            // To send action method arguments form OnActionExecuting method to OnActionExecuted method
            context.HttpContext.Items["arguments"] = context.ActionArguments;

            if (context.ActionArguments.ContainsKey("searchBy"))
            {
                string? searchBy = Convert.ToString(context.ActionArguments["searchBy"]);

                // validate the searchBy parameter value
                if (!string.IsNullOrEmpty(searchBy))
                {
                    List<string> searchByOptions = new List<string>()
                    {
                        nameof(PersonResponse.PersonName),
                        nameof(PersonResponse.Email),
                        nameof(PersonResponse.DateOfBirth),
                        nameof(PersonResponse.Gender),
                        nameof(PersonResponse.CountryID),
                        nameof(PersonResponse.Address)
                    };

                    // reset the serachBy parameter value
                    if (!searchByOptions.Contains(searchBy))
                    {
                        _logger.LogDebug("{serachByArgument} actual value is {searchBy}", nameof(searchBy), searchBy);
                        context.ActionArguments["searchBy"] = nameof(PersonResponse.PersonName);
                        searchBy = Convert.ToString(context.ActionArguments["searchBy"]);
                        _logger.LogDebug("{serachByArgument} updated value is {searchBy}", nameof(searchBy), searchBy);
                    }
                }
            }

            await next(); // calls the subsequence filter or action method

            // To do: add after logic here
            _logger.LogInformation("After logic of {FilterName}.{MethodName} method", nameof(PersonsListActionFilter), nameof(OnActionExecutionAsync));
            _logger.LogDebug("Order: {OrderProperty}", Order);

            PersonsController? personsController = context.Controller as PersonsController;

            if (personsController != null)
            {
                IDictionary<string, object?>? parameters = context.HttpContext.Items["arguments"] as IDictionary<string, object?>;

                if (parameters != null)
                {
                    if (parameters.ContainsKey("searchBy"))
                    {
                        personsController.ViewData["CurrentSearchBy"] = Convert.ToString(parameters["searchBy"]);
                    }
                    if (parameters.ContainsKey("searchString"))
                    {
                        personsController.ViewData["CurrentSearchString"] = Convert.ToString(parameters["searchString"]);
                    }
                    if (parameters.ContainsKey("sortBy"))
                    {
                        personsController.ViewData["CurrentSortBy"] = Convert.ToString(parameters["sortBy"]);
                    }
                    else
                    {
                        personsController.ViewData["CurrentSortBy"] = nameof(PersonResponse.PersonName);
                    }
                    if (parameters.ContainsKey("sortOrder"))
                    {
                        personsController.ViewData["CurrentSortOrder"] = Convert.ToString(parameters["sortOrder"]);
                    }
                    else
                    {
                        personsController.ViewData["CurrentSortOrder"] = SortOrderOptions.ASC.ToString();
                    }
                }
                personsController.ViewData["SearchFields"] = new Dictionary<string, string>()
                {
                    {"Person Name", nameof(PersonResponse.PersonName)},
                    {"Email", nameof(PersonResponse.Email)},
                    {"Date Of Birth", nameof(PersonResponse.DateOfBirth)},
                    {"Gender", nameof(PersonResponse.Gender)},
                    {"Country", nameof(PersonResponse.CountryID)},
                    {"Address", nameof(PersonResponse.Address)}
                };
            }
        }
    }
}
