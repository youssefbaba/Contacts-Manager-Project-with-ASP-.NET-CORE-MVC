using Autofac;
using Autofac.Extensions.DependencyInjection;
using ServiceContracts;
using ServiceLayer;

var builder = WebApplication.CreateBuilder(args);

// To enable Autofac instead IoC container
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddControllersWithViews();

// To add service in Autofac
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<WeatherService>().As<IWeatherService>().InstancePerLifetimeScope();  // equivalent to AddScoped
});

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();   

app.Run();
