using Microsoft.AspNetCore.WebUtilities;

namespace Assignement_2
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LoginMiddleware
    {
        private readonly RequestDelegate _next;

        public LoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Method == "POST" && httpContext.Request.Path == "/")
            {
                string? email = default;
                string? password = default;

                StreamReader streamReader = new StreamReader(httpContext.Request.Body);
                string bodyString = await streamReader.ReadToEndAsync();
                var requestBodyDictionary = QueryHelpers.ParseQuery(bodyString);

                // email
                if (requestBodyDictionary.ContainsKey("email"))
                {
                    email = requestBodyDictionary["email"][0];
                }
                else
                {
                    httpContext.Response.StatusCode = 400;
                    await httpContext.Response.WriteAsync("Invalid input of 'email'\n");
                }

                // password
                if (requestBodyDictionary.ContainsKey("password"))
                {
                    password = requestBodyDictionary["password"][0];
                }
                else
                {
                    if (httpContext.Response.StatusCode == 200)
                    {
                        httpContext.Response.StatusCode = 400;
                    }
                    await httpContext.Response.WriteAsync("Invalid input of 'password'\n");
                }

                // validation of email and password

                if (httpContext.Response.StatusCode == 200)
                {
                    if (email == "admin@example.com" && password == "admin1234")
                    {
                        await httpContext.Response.WriteAsync("Successful login\n");
                    }
                    else
                    {
                        httpContext.Response.StatusCode = 400;
                        await httpContext.Response.WriteAsync("Invalid login\n");
                    }
                }
            }

            else
            {
                await _next(httpContext);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LoginMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoginMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoginMiddleware>();
        }
    }
}
