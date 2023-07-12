using RoutingDemo.CustomConstraints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("months", typeof(MonthsCustomConstraints));
});

var app = builder.Build();

// Enables routing and select appropriate end point based on http method and url path
app.UseRouting();

// Creating end points
app.UseEndpoints(endpoints =>
{
    // add yout end points
    endpoints.Map("/files/{filename}.{extension}", async context =>
    {
        string? fileName = Convert.ToString(context.Request.RouteValues["filename"]);
        string? fileExtension = Convert.ToString(context.Request.RouteValues["extension"]);
        await context.Response.WriteAsync($"In files - {fileName}.{fileExtension}");
    });

    // Default parameters
    //endpoints.Map("/employee/profile/{employeename:minlength(3):maxlength(7)=john}", async context =>
    endpoints.Map("/employee/profile/{employeename:alpha:length(3,7)=john}", async context =>
    {
        string? employeeName = Convert.ToString(context.Request.RouteValues["employeename"]);
        await context.Response.WriteAsync($"In Employee profile : {employeeName}");
    });

    // Optional parameters
    //endpoints.Map("/products/details/{id:int:min(1):max(1000)?}", async context =>
    endpoints.Map("/products/details/{id:int:range(1,1000)?}", async context =>
    {
        if (context.Request.RouteValues.ContainsKey("id"))
        {
            int id = Convert.ToInt32(context.Request.RouteValues["id"]);
            await context.Response.WriteAsync($"Product details : {id}");
        }
        else
        {
            await context.Response.WriteAsync($"Product details : is is not supplied");
        }
    });

    // Eg : daily-digest-report/{reportdate}

    endpoints.Map("/daily-digest-report/{reportdate:datetime}", async context =>
    {
        DateTime reportDate = Convert.ToDateTime(context.Request.RouteValues["reportdate"]);
        await context.Response.WriteAsync($"In daily-digest-report : {reportDate.ToString("yyyy/MM/dd")}");
    });

    // Eg: cities/cityid
    endpoints.Map("/cities/{cityid:guid}", async context =>
    {
        Guid result = default;
        if (Guid.TryParse(Convert.ToString(context.Request.RouteValues["cityid"]), out result))
        {
            await context.Response.WriteAsync($"City information - {result}");
        }
    });

    // Eg: country/{phonenumber}
    endpoints.Map("/country/{phonenumber:int:length(9)}", async context =>
    {
        int phoneNumber = Convert.ToInt32(context.Request.RouteValues["phonenumber"]);
        await context.Response.WriteAsync($"Country phone - {phoneNumber}");
    });

    // Eg: sales-report/2024/january
    // Eg: sales-report/2030/january
    endpoints.Map("sales-report/{year:int:min(1900)}/{month:months}", async context =>
    {
        int year = Convert.ToInt32(context.Request.RouteValues["year"]);
        string? month = Convert.ToString(context.Request.RouteValues["month"]);
        await context.Response.WriteAsync($"sales-report - {year}/{month}");
    });

    // Eg: sales-report/2024/january
    endpoints.Map("sales-report/2024/january", async context =>
    {
        await context.Response.WriteAsync($"sales report  exclusively for 2024/january");
    });

});

app.Run(async context =>
{
    await context.Response.WriteAsync($"No route matched at {context.Request.Path}");
});

app.Run();
