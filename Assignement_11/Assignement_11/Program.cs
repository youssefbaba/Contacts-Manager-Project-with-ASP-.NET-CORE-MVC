using Assignement_11.Models;

var builder = WebApplication.CreateBuilder(args);

// To add controllers and views a services
builder.Services.AddControllersWithViews();

// To supply an object of SocialMediaLinksOptions (with 'SocialMediaLinks' section) as a service
builder.Services.Configure<SocialMediaLinksOptions>(
    builder.Configuration.GetSection("SocialMediaLinks")
);

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();
