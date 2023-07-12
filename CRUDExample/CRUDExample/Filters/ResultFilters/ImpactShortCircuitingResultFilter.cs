using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUDExample.Filters.ResultFilters
{
    public class ImpactShortCircuitingResultFilter : IResultFilter
    {
        // Fields
        private readonly ILogger<ImpactShortCircuitingResultFilter> _logger;

        // Constructors
        public ImpactShortCircuitingResultFilter(ILogger<ImpactShortCircuitingResultFilter> logger)
        {
            _logger = logger;
        }

        // Methods
        public void OnResultExecuting(ResultExecutingContext context)
        {
            // To Do: before logic here
            _logger.LogInformation("before logic of {FilterName}.{MethodName} method", nameof(ImpactShortCircuitingResultFilter), nameof(OnResultExecuting));
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            // To Do: after logic here
            _logger.LogInformation("after logic of {FilterName}.{MethodName} method", nameof(ImpactShortCircuitingResultFilter), nameof(OnResultExecuted));
        }
    }
}
