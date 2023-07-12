using CRUDExample.Filters.ActionFilters;
using CRUDExample.Filters.ResultFilters;
using Entities;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using RepositoryLayer;
using ServiceContracts;
using ServiceLayer;

namespace CRUDExample
{
    public static class ConfigureServicesExtension
    {
        // Create an extension method to configure services (Add service to IoC container or delete it )
        public static IServiceCollection ConfigureServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            // To add Controllers and Views as Services and Make filter as Global-Level filter
            services.AddControllersWithViews(options =>
            {
                //options.Filters.Add<ResponseHeaderActionFilter>(order: 2);  // First way to add Global-level filter

                /*
                ServiceProvider serviceProvider = builder.Services.BuildServiceProvider(); // Not Rocommended
                ILogger<ResponseHeaderActionFilter>? logger = serviceProvider.GetRequiredService<ILogger<ResponseHeaderActionFilter>>();
                */

                var builder = WebApplication.CreateBuilder();
                var application = builder.Build();
                var logger = application.Services.GetService<ILogger<ResponseHeaderActionFilter>>();
                if (logger != null)
                {
                    options.Filters.Add(new ResponseHeaderActionFilter(logger) { Key = "X-Custom-Key-From-Global", Value = "X-Custom-Value-From-Global", Order = 2 });  // Second way to add Global-level filter
                }
            });

            // To add IPersonsService into IoC countainer as sservice
            services.AddScoped<IPersonsGetterService, PersonsGetterServiceWithFewExcelFields>();
            //services.AddScoped<IPersonsGetterService, PersonsGetterServiceChild>();
            services.AddScoped<PersonsGetterService, PersonsGetterService>();
            services.AddScoped<IPersonsAdderService, PersonsAdderService>();
            services.AddScoped<IPersonsSorterService, PersonsSorterService>();
            services.AddScoped<IPersonsUpdaterService, PersonsUpdaterService>();
            services.AddScoped<IPersonsDeleterService, PersonsDeleterService>();
            services.AddScoped<IPersonsRepository, PersonsRepository>();

            // To add ICountriesService into IoC countainer as service
            services.AddScoped<ICountriesGetterService, CountriesGetterService>();
            services.AddScoped<ICountriesAdderService, CountriesAdderService>();
            services.AddScoped<ICountriesUploadFromExcelService, CountriesUploadFromExcelService>();
            services.AddScoped<ICountriesRepository, CountriesRepository>();

            // To add PersonsListResultFilter filter into IoC countainer as service
            services.AddTransient<PersonsListResultFilter>();
            services.AddTransient<ResponseHeaderActionFilter>();

            // To add DbContext as service (by default is Scoped service)
            services.AddDbContext<ApplicationDbContext>
            (
                options =>
                {
                    //options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);  // First way
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));  // Second way
                }
            );

            // To add HttpLogging as service 
            services.AddHttpLogging(options =>
            {
                //options.LoggingFields = HttpLoggingFields.RequestHeaders | HttpLoggingFields.ResponseHeaders;
                //options.LoggingFields = HttpLoggingFields.RequestProperties | HttpLoggingFields.ResponsePropertiesAndHeaders;
                options.LoggingFields = HttpLoggingFields.RequestPropertiesAndHeaders | HttpLoggingFields.ResponsePropertiesAndHeaders;
            });

            return services;
        }
    }
}
