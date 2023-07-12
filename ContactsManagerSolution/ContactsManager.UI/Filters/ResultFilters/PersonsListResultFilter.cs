using Microsoft.AspNetCore.Mvc.Filters;

namespace ContactsManager.UI.Filters.ResultFilters
{
    public class PersonsListResultFilter : IAsyncResultFilter
    {
        // Fields
        private readonly ILogger<PersonsListResultFilter> _logger;

        // Constructors
        public PersonsListResultFilter(ILogger<PersonsListResultFilter> logger)
        {
            _logger = logger;
        }

        // Methods
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            // To Do: before logic here
            _logger.LogInformation("before logic of {FilterName}.{MethodName} method", nameof(PersonsListResultFilter), nameof(OnResultExecutionAsync));

            context.HttpContext.Response.OnStarting(() =>
            {
                context.HttpContext.Response.Headers["Last-Modified"] = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                return Task.CompletedTask;
            });

            await next(); // calls the subsequent filters or IActionResult 

            // To Do: after logic here
            _logger.LogInformation("after logic of {FilterName}.{MethodName} method", nameof(PersonsListResultFilter), nameof(OnResultExecutionAsync));
        }
    }
}
