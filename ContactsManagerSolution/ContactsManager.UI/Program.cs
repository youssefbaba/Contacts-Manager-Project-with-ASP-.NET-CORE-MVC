using ContactsManager.UI.Middlewares;
using ContactsManager.UI.StartupExtensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configurations to enable Serilog Framework
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(context.Configuration) // Read configuration settings from appsettings.json
    .ReadFrom.Services(services); // Read our current app's services and make them available to Serilog
});

// Extension method to configure services into it
builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

// To enable developer exception page
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else  // For another Environment than Development
{
    app.UseExceptionHandler("/Error");  // Built-in Exception Handling Middlware
    app.UseExceptionHandlingMiddlware();  // User-defined Exception Handling Middlware 
}

// To enable HTTPS 
app.UseHsts();
app.UseHttpsRedirection();

// To enable the End-Point Completion Log
app.UseSerilogRequestLogging();

// To enable HttpLogging
app.UseHttpLogging();

if (!builder.Environment.IsEnvironment("Test"))
{
    // To recognize wkhtmltopdf from Rotativa
    Rotativa.AspNetCore.RotativaConfiguration.Setup(
        rootPath: "wwwroot", wkhtmltopdfRelativePath: "Rotativa"
    );
}

// To add static files
app.UseStaticFiles();

// To enable routing
app.UseRouting(); // Identifying action method based on the route

app.UseAuthentication(); // To read the Identity cookie (checks if the user is authenticated or not )

app.UseAuthorization(); // To evalute the access permission of particular user (checks if the user has permission to access to a specific resource)

// To enable attribute routing for the controllers
app.MapControllers(); // Execute the filter pipline (Action + filters)

// To enable conventional routing
app.UseEndpoints(endpoints =>
{
    // conventional routing for Admin area
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}");  // Example1 : Admin/Home/Details  , Example2 : Admin

    // conventional routing for regular user
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action}/{id?}");
});

app.Run();

public partial class Program { }  // make the auto-generated Program class accessible programmatically 