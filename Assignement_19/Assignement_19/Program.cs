using Assignement_19.Models;
using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using RepositoryLayer;
using Serilog;
using ServiceContracts;
using ServiceLayer;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// To add controllers and views as services
builder.Services.AddControllersWithViews();

/*
// Configuration for built-in logging framework
builder.Host.ConfigureLogging(loggingProvider =>
{
    loggingProvider.ClearProviders();  // To clear all logging providers
    loggingProvider.AddConsole();
    loggingProvider.AddDebug();
    loggingProvider.AddEventLog();

});
*/

// Configuration for Serilog
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(context.Configuration)  // Reads configuration form appsettings.json and assign it to Serilog 
    .ReadFrom.Services(services);  // Reads our current services and makes the available into Serilog
});

// To add HttpClient as service
builder.Services.AddHttpClient();

// To add IFinnhubService into IoC container as service
builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.AddScoped<IFinnhubRepository, FinnhubRepository>();

// To add IStocksService into IoC container as service
builder.Services.AddScoped<IStocksService, StocksService>();
builder.Services.AddScoped<IStocksRepository, StocksRepository>();

// To supply an object of TradingOptions (with 'TradingOptions' section) as a service
builder.Services.Configure<TradingOptions>(
    builder.Configuration.GetSection("TradingOptions")
);

// To add DbContext as service
builder.Services.AddDbContext<StockMarketDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
);

var app = builder.Build();

/*
// To Add Loggin message from different log levels
app.Logger.LogDebug("debug-message");
app.Logger.LogInformation("information-message");
app.Logger.LogWarning("Warning-message");
app.Logger.LogError("Error-message");
app.Logger.LogCritical("Critical-message");
*/

// To enable developer exception page
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

if (!builder.Environment.IsEnvironment("Test"))
{
    // To recognize wkhtmltopdf from Rotativa
    Rotativa.AspNetCore.RotativaConfiguration.Setup(
        rootPath: "wwwroot", wkhtmltopdfRelativePath: "Rotativa"
    );
}


// To enable the usage of satatic files (css, js, image, ...)
app.UseStaticFiles();

// To anbale rounting
app.UseRouting();

// to enable attribute rounting for all controllers
app.MapControllers();

app.Run();

public partial class Program { }  // make auto-generated Program class accessible programmatically