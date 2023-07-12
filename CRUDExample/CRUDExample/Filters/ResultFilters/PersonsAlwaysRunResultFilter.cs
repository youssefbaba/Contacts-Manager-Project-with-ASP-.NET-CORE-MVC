using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUDExample.Filters.ResultFilters
{
    public class PersonsAlwaysRunResultFilter : IAlwaysRunResultFilter
    {
        // Fields
        private readonly ILogger<PersonsAlwaysRunResultFilter> _logger;

        // Constructors
        public PersonsAlwaysRunResultFilter(ILogger<PersonsAlwaysRunResultFilter> logger)
        {
            _logger = logger;
        }

        // Methods
        public void OnResultExecuting(ResultExecutingContext context)
        {
            // To skip its execution from Index action method
            if (context.Filters.OfType<SkipFilter>().Any())
            {
                return;
            }

            // To Do: before logic here
            _logger.LogInformation("before logic of {FilterName}.{MethodName} method", nameof(PersonsAlwaysRunResultFilter), nameof(OnResultExecuting));
            context.HttpContext.Response.OnStarting(() =>
            {
                context.HttpContext.Response.Headers["Z-Custom-Key"] = "Z-Custom-Value";
                return Task.CompletedTask;
            });
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            // To skip its execution from Index action method
            if (context.Filters.OfType<SkipFilter>().Any())
            {
                return;
            }

            // To Do: after logic here
            _logger.LogInformation("after logic of {FilterName}.{MethodName} method", nameof(PersonsAlwaysRunResultFilter), nameof(OnResultExecuted));
        }
    }
}
