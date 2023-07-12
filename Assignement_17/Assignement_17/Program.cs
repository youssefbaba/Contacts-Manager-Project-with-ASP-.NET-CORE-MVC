using Assignement_17.Models;
using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using RepositoryLayer;
using ServiceContracts;
using ServiceLayer;

var builder = WebApplication.CreateBuilder(args);

// To add controllers and views as services
builder.Services.AddControllersWithViews();

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