using Assignement_12.Models;
using ServiceContracts;
using ServiceLayer;

var builder = WebApplication.CreateBuilder(args);

// To add Controllers and Views as services
builder.Services.AddControllersWithViews();

// To add HttpClient as service
builder.Services.AddHttpClient();

// To add FinnhubService into Ioc container as service
builder.Services.AddScoped<IFinnhubService, FinnhubService>();

// To supply an object of TradingOptions (with 'TradingOptions' section) as a service
builder.Services.Configure<TradingOptions>(
    builder.Configuration.GetSection("TradingOptions")
);

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();
