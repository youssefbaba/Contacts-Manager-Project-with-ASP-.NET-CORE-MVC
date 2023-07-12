var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); // to add all the controllers as services

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();  // enable routing

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run(); // To start the application
