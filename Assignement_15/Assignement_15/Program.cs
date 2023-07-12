using Assignement_15.Models;
using ServiceContracts;
using ServiceLayer;

var builder = WebApplication.CreateBuilder(args);

// To add controllers and views as services
builder.Services.AddControllersWithViews();

// To add HttpClient as service
builder.Services.AddHttpClient();

// To add IFinnhubService into IoC container as service
builder.Services.AddScoped<IFinnhubService, FinnhubService>();

// To add IStocksService into IoC container as service
builder.Services.AddScoped<IStocksService, StocksService>();

// To supply an object of TradingOptions (with 'TradingOptions' section) as a service
builder.Services.Configure<TradingOptions>(
    builder.Configuration.GetSection("TradingOptions")
);

var app = builder.Build();

// To enable the usage of satatic files (css, js, image, ...)
app.UseStaticFiles();

// To anbale rounting
app.UseRouting();

// to enable attribute rounting for all controllers
app.MapControllers();

app.Run();
