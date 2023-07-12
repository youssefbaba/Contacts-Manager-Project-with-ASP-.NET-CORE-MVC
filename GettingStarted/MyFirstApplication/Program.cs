var builder = WebApplication.CreateBuilder(args); // WebApplicationBuilder loads Configuration Settings , Environment Settings , Services (built-in and user defined services)

// to configure builder

var app = builder.Build(); // To create an instance of our web application

// to configure Http pipeline (Middleware) and routes

app.MapGet("/", () => "Hello Youssef Baba!");

app.Run(); // To start the server

