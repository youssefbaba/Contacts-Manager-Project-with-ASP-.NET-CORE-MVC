using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args); // WebApplicationBuilder loads Configuration Settings , Environment Settings , Services (built-in and user defined services)

// to configure builder

var app = builder.Build(); // To create an instance of our web application

// to configure Http pipeline (Middleware) and routes

app.Run(async (HttpContext context) =>
{
    StreamReader streamReader = new StreamReader(context.Request.Body);
    string body = await streamReader.ReadToEndAsync();
    Dictionary<string, StringValues> requestBody = QueryHelpers.ParseQuery(body);

    if (requestBody.ContainsKey("fullName") && requestBody.ContainsKey("address"))
    {
        foreach (KeyValuePair<string, StringValues> keyValuePair in requestBody)
        {
            foreach (string value in keyValuePair.Value)
            {
                await context.Response.WriteAsync($"<p>{value}</p>\n");
            }
        }
    }

});

app.Run(); // To start the server

