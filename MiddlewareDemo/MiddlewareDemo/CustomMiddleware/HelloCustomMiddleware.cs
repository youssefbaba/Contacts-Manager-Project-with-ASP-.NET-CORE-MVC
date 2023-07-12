
namespace MiddlewareDemo.CustomMiddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class HelloCustomMiddleware
    {
        private readonly RequestDelegate _next;

        public HelloCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // before logic
            await httpContext.Response.WriteAsync("Custom Conventional Middleware class - starts \n");
            if (httpContext.Request.Query.ContainsKey("firstName") && httpContext.Request.Query.ContainsKey("lastName"))
            {
                string fullName = httpContext.Request.Query["firstName"][0] + " " + httpContext.Request.Query["lastName"][0];
                await httpContext.Response.WriteAsync($"Hello {fullName}\n");
            }

             await _next(httpContext);

            // after logic
            await httpContext.Response.WriteAsync("Custom Conventional Middleware class - ends \n");

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HelloCustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseHelloCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HelloCustomMiddleware>();
        }
    }
}
