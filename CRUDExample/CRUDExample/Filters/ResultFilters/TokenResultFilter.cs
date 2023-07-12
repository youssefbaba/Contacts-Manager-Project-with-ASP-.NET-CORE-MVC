using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUDExample.Filters.ResultFilters
{
    public class TokenResultFilter : IAsyncResultFilter
    {
        // Fields
        private readonly ILogger<TokenResultFilter> _logger;

        // Consructors
        public TokenResultFilter(ILogger<TokenResultFilter> logger)
        {
            _logger = logger;
        }

        // Methods
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            // To Do: before logic here
            _logger.LogInformation("before logic of {FilterName}.{MethodName} method", nameof(TokenResultFilter), nameof(OnResultExecutionAsync));
            context.HttpContext.Response.Cookies.Append("Auth-Key", "A100");

            await next();

            // To Do: after logic here
            _logger.LogInformation("after logic of {FilterName}.{MethodName} method", nameof(TokenResultFilter), nameof(OnResultExecutionAsync));
        }
    }
}
