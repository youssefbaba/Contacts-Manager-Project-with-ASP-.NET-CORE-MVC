using Serilog;

namespace Assignement_20.Middlewares
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
            _next = next;  // Represents the subsequent middlewares
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
                    _logger.LogInformation("ExceptionType: {ExceptionType} - ExceptionMessage: {ExceptionMessage}", exp.InnerException.GetType().ToString(), exp.InnerException.Message);
                }
                else
                {
                    _logger.LogInformation("ExceptionType: {ExceptionType} - ExceptionMessage: {ExceptionMessage}", exp.GetType().ToString(), exp.Message);
                }

                throw;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
