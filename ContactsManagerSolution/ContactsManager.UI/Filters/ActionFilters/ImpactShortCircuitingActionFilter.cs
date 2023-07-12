using Microsoft.AspNetCore.Mvc.Filters;

namespace ContactsManager.UI.Filters.ActionFilters
{
    public class ImpactShortCircuitingActionFilter : IActionFilter
    {
        // Fields
        private readonly ILogger<ImpactShortCircuitingActionFilter> _logger;

        // Constructors
        public ImpactShortCircuitingActionFilter(ILogger<ImpactShortCircuitingActionFilter> logger)
        {
            _logger = logger;
        }

        // Methods
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // To Do: before logic here
            _logger.LogInformation("before logic of {FilterName}.{MethodName} method", nameof(ImpactShortCircuitingActionFilter), nameof(OnActionExecuting));
            //context.Result = new ContentResult() { Content = "Test For Impact in ActionFilter" }; 
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // To Do: after logic here
            _logger.LogInformation("after logic of {FilterName}.{MethodName} method", nameof(ImpactShortCircuitingActionFilter), nameof(OnActionExecuted));
        }
    }
}
