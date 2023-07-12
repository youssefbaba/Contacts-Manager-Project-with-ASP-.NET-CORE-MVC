using Microsoft.EntityFrameworkCore;
using StocksApplication.Core.Domain.RepositoryContracts;
using StocksApplication.Core.ServiceContracts.FinnhubService;
using StocksApplication.Core.ServiceContracts.StocksService;
using StocksApplication.Core.Services.FinnhubService;
using StocksApplication.Core.Services.StocksService;
using StocksApplication.Infrastructure.DatabaseContext;
using StocksApplication.Infrastructure.Repositories;
using StocksApplication.UI.ViewModels;

namespace StocksApplication.UI.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        // To Create extension to configure services
        public static IServiceCollection ConfigureServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            // To add controllers and views as services
            services.AddControllersWithViews();

            // To add HttpClient as service
            services.AddHttpClient();

            // To add IFinnhubService into IoC container as service
            services.AddScoped<IFinnhubCompanyProfileService, FinnhubCompanyProfileService>();
            services.AddScoped<IFinnhubSearchStocksService, FinnhubSearchStocksService>();
            services.AddScoped<IFinnhubStockPriceQuoteService, FinnhubStockPriceQuoteService>();
            services.AddScoped<IFinnhubStocksService, FinnhubStocksService>();
            services.AddScoped<IFinnhubRepository, FinnhubRepository>();

            // To add IStocksService into IoC container as service
            services.AddScoped<IBuyOrdersService, BuyOrdersService>();
            services.AddScoped<ISellOrdersService, SellOrdersService>();
            services.AddScoped<IStocksRepository, StocksRepository>();

            // To supply an object of TradingOptions (with 'TradingOptions' section) as a service
            services.Configure<TradingOptions>(
                configuration.GetSection("TradingOptions")
            );

            // To add DbContext as service
            services.AddDbContext<StockMarketDbContext>(
                options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                }
            );

            return services;
        }
    }
}
