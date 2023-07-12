namespace MiddlewareDemo.CustomMiddleware
{
    public class MyCustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("My custom middleware - starts \n");
            await next(context);
            await context.Response.WriteAsync("My custom middleware - ends \n");
        }
    }
    
    public static class CustomMiddlewareExtension
    {
        // Extension Method
        public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MyCustomMiddleware>(); 
        }
    }

}
