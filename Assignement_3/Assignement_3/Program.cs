using Assignement_3;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

DataSource dataSource = new DataSource();
Dictionary<int, string> countriesDictionary = dataSource.GetData();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/countries", async context =>
    {
        foreach (KeyValuePair<int, string> country in countriesDictionary)
        {
            await context.Response.WriteAsync($"{country.Key}, {country.Value}\n");
        }
    });

    endpoints.MapGet("/countries/{countryID:int:range(1,100)}", async context =>
    {
        int countryId = Convert.ToInt32(context.Request.RouteValues["countryID"]);
        if (countriesDictionary.ContainsKey(countryId))
        {
            string countryName = countriesDictionary[countryId];
            await context.Response.WriteAsync($"{countryName}\n");
        }
        else
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync($"[No Country]\n");
        }
    });
    endpoints.MapGet("/countries/{countryID:int:min(100)}", async context =>
    {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync($"The CountryID should be between 1 and 100\n");
    });
});


app.Run(async context =>
    await context.Response.WriteAsync($"No route matches with {context.Request.Path}")
);

app.Run();
