using CRUDExample.Filters.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUDExample.Filters.ResourceFilters
{
    public class ImpactShortCircuitingResourceFilter : IResourceFilter
    {
        // Fields
        private readonly ILogger<ImpactShortCircuitingActionFilter> _logger;

        // Constructors
        public ImpactShortCircuitingResourceFilter(ILogger<ImpactShortCircuitingActionFilter> logger)
        {
            _logger = logger;
        }

        // Methods
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            // To Do: before logic here
            _logger.LogInformation("before logic of {FilterName}.{MethodName} method", nameof(ImpactShortCircuitingResourceFilter), nameof(OnResourceExecuting));
            //context.Result = new ContentResult() { Content = "Test for short-circuiting in ResourceFilre" };
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // To Do: after logic here
            _logger.LogInformation("after logic of {FilterName}.{MethodName} method", nameof(ImpactShortCircuitingResourceFilter), nameof(OnResourceExecuted));
        }
    }
}
