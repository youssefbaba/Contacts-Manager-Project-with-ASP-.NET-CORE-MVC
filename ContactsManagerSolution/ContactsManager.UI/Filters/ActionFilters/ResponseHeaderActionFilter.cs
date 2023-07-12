using Microsoft.AspNetCore.Mvc.Filters;

namespace ContactsManager.UI.Filters.ActionFilters
{
    // Parameterized ActionFilter
    public class ResponseHeaderActionFilter : IAsyncActionFilter, IOrderedFilter
    {
        // Fields
        private readonly ILogger<ResponseHeaderActionFilter> _logger;

        // Properties
#nullable disable
        public string Key { get; set; }

        public string Value { get; set; }

        public int Order { get; set; }
#nullable restore

        // Constructors
        public ResponseHeaderActionFilter(ILogger<ResponseHeaderActionFilter> logger)
        {
            _logger = logger;
        }

        // Methods
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // TO DO: before logic here
            _logger.LogInformation("before logic of {FilterName}.{MethodName} method", nameof(ResponseHeaderActionFilter), nameof(OnActionExecutionAsync));

            await next();

            // TO DO: after logic here      // Default Order:  1:Method => 2:Controller => 3:Global
            _logger.LogInformation("after logic of {FilterName}.{MethodName} method", nameof(ResponseHeaderActionFilter), nameof(OnActionExecutionAsync));
            context.HttpContext.Response.Headers[Key] = Value;
        }
    }
}
