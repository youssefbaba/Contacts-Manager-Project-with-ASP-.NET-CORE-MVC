using ServiceLayer;
using StocksApp.Models;

var builder = WebApplication.CreateBuilder(args);

// To enable controllers and views as services
builder.Services.AddControllersWithViews();

// To add HttpClient as service
builder.Services.AddHttpClient();

// To add MyService as service to IoC container
builder.Services.AddScoped<FinnhubService>();

// Supply an object of TradingOptions (with 'TradingOptions' section) as a service
builder.Services.Configure<TradingOptions>(
    builder.Configuration.GetSection("TradingOptions")
);

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();
