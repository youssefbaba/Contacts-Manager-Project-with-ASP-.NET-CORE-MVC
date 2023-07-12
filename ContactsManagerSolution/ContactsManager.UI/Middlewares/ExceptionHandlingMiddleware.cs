using Serilog;

namespace ContactsManager.UI.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IDiagnosticContext _diagnosticContext;

        public ExceptionHandlingMiddleware(RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger, IDiagnosticContext diagnosticContext)
        {
            _next = next; // Represents the subsequent middleware
            _logger = logger;
            _diagnosticContext = diagnosticContext;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exp)
            {
                if (exp.InnerException != null)
                {
                    _logger.LogError("ExceptionType: {ExceptionType} - ExceptionMessage: {ExcepptionMessage}", exp.InnerException.GetType().ToString(), exp.InnerException.Message);
                }
                else
                {
                    _logger.LogError("ExceptionType: {ExceptionType} - ExceptionMessage: {ExcepptionMessage}", exp.GetType().ToString(), exp.Message);
                }

                /*
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsync("Error Occured, Please try again");
                */

                throw;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlingMiddlwareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddlware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
