using ConfigurationDemo.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Supply an object of WeatherAPIOptions (with 'WeatherAPI' section) as a service
builder.Services.Configure<WeatherAPIOptions>(
    builder.Configuration.GetSection("WeatherAPI")
);

//To add MyCustomConfiguration.json as configuration source
builder.Host.ConfigureAppConfiguration(
    (hostingContext, config) =>
    {
        config.AddJsonFile("MyCustomConfiguration.json", optional:true, reloadOnChange: true);
    }
);

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

/*
app.UseEndpoints(endpoints =>
{
    endpoints.Map("/config", async context =>
    {
        await context.Response.WriteAsync(app.Configuration["MyKey"] + "\n");
        await context.Response.WriteAsync(app.Configuration.GetValue<string>("MyKey") + "\n");
        await context.Response.WriteAsync(app.Configuration.GetValue<int>("x", 10) + "\n");

    });
});
*/

app.MapControllers();

app.Run();
