using Autofac;
using Autofac.Extensions.DependencyInjection;
using ServiceContracts;
using ServiceLayer;

var builder = WebApplication.CreateBuilder(args);

// To enable Autofac instead IoC container
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddControllersWithViews();

// builder.Services = IoC container
// To add service to IoC container
/*
builder.Services.Add(
    new ServiceDescriptor(serviceType: typeof(ICitiesService),
      implementationType:  typeof(CitiesService),
      // lifetime: ServiceLifetime.Transient // Per Injection
       lifetime: ServiceLifetime.Scoped // Per Scope (Browser Request)
      // lifetime: ServiceLifetime.Singleton // For entire lifetime of the application
));
*/

//builder.Services.AddTransient<ICitiesService, CitiesService>();
//builder.Services.AddScoped<ICitiesService, CitiesService>();
//builder.Services.AddSingleton<ICitiesService, CitiesService>();

// To add service in Autofac
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    //containerBuilder.RegisterType<CitiesService>().As<ICitiesService>().InstancePerDependency();  // equivalent to AddTransient
    containerBuilder.RegisterType<CitiesService>().As<ICitiesService>().InstancePerLifetimeScope();  // equivalent to AddScoped
    //containerBuilder.RegisterType<CitiesService>().As<ICitiesService>().SingleInstance();  // equivalent to AddSingleton
});

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();


