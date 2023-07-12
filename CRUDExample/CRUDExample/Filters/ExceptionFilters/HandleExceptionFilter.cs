using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUDExample.Filters.ExceptionFilters
{
    public class HandleExceptionFilter : IAsyncExceptionFilter
    {
        // Fields
        private readonly ILogger<HandleExceptionFilter> _logger;
        private readonly IHostEnvironment _hostEnvironment;

        // Constructors
        public HandleExceptionFilter(ILogger<HandleExceptionFilter> logger,
            IHostEnvironment hostEnvironment
            )
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        // Methods
        public Task OnExceptionAsync(ExceptionContext context)
        {
            // To Do: Exception Handling logic
            _logger.LogError("Exception filter {FilterName}.{MethodName}\n{ExceptionType}\n{ExceptionMessage}",
                nameof(HandleExceptionFilter), nameof(OnExceptionAsync), context.Exception.GetType().ToString(), context.Exception.Message);

            if (_hostEnvironment.IsDevelopment())
            {
                context.Result = new ContentResult() { Content = context.Exception.Message, StatusCode = 500 };
            }

            return Task.CompletedTask;
        }
    }
}
