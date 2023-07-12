var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();


//if (app.Environment.IsDevelopment() || app.Environment.IsStaging())  // app.Environment.IsProduction()
if (app.Environment.IsDevelopment())
{
    // To enable developer exception page
    app.UseDeveloperExceptionPage();
    
}

/*
if (app.Environment.IsEnvironment("Development") || app.Environment.IsEnvironment("Beta"))  // app.Environment.IsEnvironment("Staging"), app.Environment.IsEnvironment("Production") 
{
    // To enable developer exception page
    app.UseDeveloperExceptionPage();
}
*/

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();
