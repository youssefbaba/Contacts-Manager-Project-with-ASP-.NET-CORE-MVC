using ContactsManager.Core.Domain.IdentityEntities;
using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.ServiceContracts;
using ContactsManager.Core.Services;
using ContactsManager.Infrastructure.DatabaseContext;
using ContactsManager.Infrastructure.Repositories;
using ContactsManager.UI.Filters.ActionFilters;
using ContactsManager.UI.Filters.ResultFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsManager.UI.StartupExtensions
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

                // To add antiForgeryToken globaly
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); // Only for Post action method

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

            // To Add Identity as service into IoC container
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                    {
                        options.Password.RequiredLength = 8; // the minimum number of characters that are required in password
                        options.Password.RequireNonAlphanumeric = true; //  at least one non-alphanumeric character (@,#,$,...) is required in password
                        options.Password.RequireUppercase = true;  // at least one uppercase character is required in password
                        options.Password.RequireLowercase = true;  // at least one lowercase character is required in password
                        options.Password.RequireDigit = true; // at least one digit is required in password
                        options.Password.RequiredUniqueChars = 5;  // the minimum number of distinct characters that are required in password

                    })

                    .AddEntityFrameworkStores<ApplicationDbContext>()

                    .AddDefaultTokenProviders()

                    .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()

                    .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

            // To add Authorization policy as service
            services.AddAuthorization(options =>
            {
                // Enforces authorization policy (user must be authenticated) for all action methods
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

                // To create custom authorization policies
                options.AddPolicy("NotAuthorized", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        return !(context.User.Identity != null && context.User.Identity.IsAuthenticated);
                    });
                });
            });

            // If the user not logged-in it should redirect him into login page to login
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Account/Login");
                options.AccessDeniedPath = new PathString("/Account/AccessDenied");
            });

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
