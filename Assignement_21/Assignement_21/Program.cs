using Assignement_21.Middlewares;
using Assignement_21.StartupExtensions;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Configuration for Serilog
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(context.Configuration)  // Reads configuration form appsettings.json and assign it to Serilog 
    .ReadFrom.Services(services);  // Reads our current services and makes the available into Serilog
});

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

// To enable developer exception page
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else  // For other Environment than Development
{
    app.UseExceptionHandler("/Home/Error");  // Built-in Exception Handling Middleware
    app.UseExceptionHandlingMiddleware();  // User-Defined Exception Handling Middleware
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