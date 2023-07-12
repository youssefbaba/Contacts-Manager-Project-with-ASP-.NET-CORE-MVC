var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseWhen(
    context => context.Request.Query.ContainsKey("username"),
    app =>
    {
        app.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("before logic middleware branch 1\n");
            await next(context);
            await context.Response.WriteAsync("after logic middleware branch 1\n");
        });
        app.Run(async context =>
        {
            await context.Response.WriteAsync("before logic middleware branch 2\n");
        });
    }
);

app.Run(async context =>
{
    await context.Response.WriteAsync("Hello from middleware at main chain\n");
});

app.Run();
