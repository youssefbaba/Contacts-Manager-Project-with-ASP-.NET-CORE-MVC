using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ContactsManager.UI.Filters.AuthorizationFilter
{
    public class TokenAuthorizationFilter : IAsyncAuthorizationFilter
    {
        // Fields
        private readonly ILogger<TokenAuthorizationFilter> _logger;

        // Constructors
        public TokenAuthorizationFilter(ILogger<TokenAuthorizationFilter> logger)
        {
            _logger = logger;
        }

        // Methods
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // To Do: before logic 
            _logger.LogInformation("before logic of {FilterName}.{MethodName} method", nameof(TokenAuthorizationFilter), nameof(OnAuthorizationAsync));
            if (!context.HttpContext.Request.Cookies.ContainsKey("Auth-Key"))
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);  // Short-circuiting
                return Task.CompletedTask;
            }
            if (context.HttpContext.Request.Cookies["Auth-Key"] != "A100")
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}
