using MiddlewareDemo.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<MyCustomMiddleware>();

var app = builder.Build();

// middleware 1
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("before logic middleware One\n");
    await next(context);  // Optional (you may / may not forward the request to the subsequent middleware)
    await context.Response.WriteAsync("after logic middleware One\n");
});

// middleware 2
//app.UseMiddleware<MyCustomMiddleware>();
//app.UseMyCustomMiddleware();   // middleware extension method 
app.UseHelloCustomMiddleware();  // Custom Conventional Middleware class it is most recommended in real world projects 
// middleware 3
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("before logic middleware three\n");
    //await next(context);  // Optional (you may / may not forward the request to the subsequent middleware)
    await context.Response.WriteAsync("after logic middleware three\n");
});

// middleware 4
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("before logic middleware four\n");
});

app.Run();
