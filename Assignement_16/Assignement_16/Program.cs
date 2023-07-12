using Assignement_16.Models;
using Entities;
using Microsoft.EntityFrameworkCore;
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

// To add DbContext as service
builder.Services.AddDbContext<StockMarketDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
);

var app = builder.Build();

// To enable developer exception page
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// To recognize wkhtmltopdf from Rotativa
Rotativa.AspNetCore.RotativaConfiguration.Setup(
    rootPath: "wwwroot", wkhtmltopdfRelativePath: "Rotativa"
);

// To enable the usage of satatic files (css, js, image, ...)
app.UseStaticFiles();

// To anbale rounting
app.UseRouting();

// to enable attribute rounting for all controllers
app.MapControllers();

app.Run();
