using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUDExample.Filters.ResourceFilters
{
    public class FeatureDisabledResourceFilter : IAsyncResourceFilter
    {
        // Fields
        private readonly ILogger<FeatureDisabledResourceFilter> _logger;
        private readonly bool _isDisabled;

        // Constructors
        public FeatureDisabledResourceFilter(ILogger<FeatureDisabledResourceFilter> logger, bool isDisabled = true)
        {
            _logger = logger;
            _isDisabled = isDisabled;
        }

        // Methods
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            // To Do: before logic here
            _logger.LogInformation("before logic of {FilterName}.{MethodName} method", nameof(FeatureDisabledResourceFilter), nameof(OnResourceExecutionAsync));
            _logger.LogDebug("IsDisabled: {IsDisabledField}", _isDisabled);

            if (_isDisabled)
            {
                //context.Result = new NotFoundResult(); // 404 - Not Found

                context.Result = new StatusCodeResult(501);  // 501 - Not Implemented
            }
            else
            {
                await next();  // calls the subsequent filters or action method 

                // To Do: after logic here
                _logger.LogInformation("after logic of {FilterName}.{MethodName} method", nameof(FeatureDisabledResourceFilter), nameof(OnResourceExecutionAsync));
            }
        }
    }
}
