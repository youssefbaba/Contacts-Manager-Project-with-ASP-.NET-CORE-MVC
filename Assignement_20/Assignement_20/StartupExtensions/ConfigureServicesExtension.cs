using Assignement_20.Models;
using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using RepositoryLayer;
using ServiceContracts;
using ServiceLayer;

namespace Assignement_20.StartupExtensions
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
            services.AddScoped<IFinnhubService, FinnhubService>();
            services.AddScoped<IFinnhubRepository, FinnhubRepository>();

            // To add IStocksService into IoC container as service
            services.AddScoped<IStocksService, StocksService>();
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
