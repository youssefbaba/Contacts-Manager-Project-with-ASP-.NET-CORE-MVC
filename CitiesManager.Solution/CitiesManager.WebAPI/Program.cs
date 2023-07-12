using CitiesManager.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json")); // customs content-type of response body to be only application/json format
    options.Filters.Add(new ConsumesAttribute("application/json")); // customs content-type of request body to be only application/json format
}).AddXmlSerializerFormatters();

// To enable API versioning
builder.Services.AddApiVersioning(config =>
{
    // Reads the version number of Web API from request url (route parameter) Eg: https://localhost:7030/api/v1/cities
    config.ApiVersionReader = new UrlSegmentApiVersionReader();

    // Reads the version number of web API from request query string (query string parameter called 'api-version') Eg: https://localhost:7030/api/cities?api-version=2.0
    //config.ApiVersionReader = new QueryStringApiVersionReader();

    // Reads the version number of web API from request header (request header called 'api-version') Eg: api-version:2.0
    //config.ApiVersionReader = new HeaderApiVersionReader("api-version"); 

    // to handle a default API version
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
});

// To add ApplicationDbContext as service into IoC container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// To add Swagger as service
builder.Services.AddEndpointsApiExplorer();  // It enables swagger to read metadata (HTTP method, URL, attributes, etc, ...) of endpoints (Web API action methods) => Generates description for all endpoints
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));

    // to configure swagger documentation for each version
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Cities Web API V1", Version = "1.0" });
    options.SwaggerDoc("v2", new OpenApiInfo { Title = "Cities Web APi V2", Version = "2.0" });


}); // It configures swagger to generate documentation for API's endpoints => Generates OpenAPI specification

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";  // v1
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.

// To enable HTTPS
app.UseHsts();  
app.UseHttpsRedirection();

// To enable swagger explicitly
app.UseSwagger(); // Creates endpoint for swagger.json
app.UseSwaggerUI(options =>
{
    // To configure Swagger UI for each API version
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "1.0");
    options.SwaggerEndpoint("/swagger/v2/swagger.json", "2.0");

}); // Creates swagger UI for testing all web API action methods


app.UseAuthorization();

// To enable attribute routing for all controllers
app.MapControllers();

app.Run();
