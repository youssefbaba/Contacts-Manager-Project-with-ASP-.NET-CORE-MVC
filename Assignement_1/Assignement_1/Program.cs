var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    if (context.Request.Method == "GET" && context.Request.Path == "/")
    {
        int firstNumber = 0;
        int secondNumber = 0;
        int result = 0;

        // firstNumber
        if (context.Request.Query.ContainsKey("firstNumber"))
        {
            string firstNumberString = context.Request.Query["firstNumber"][0];
            if (!string.IsNullOrEmpty(firstNumberString))
            {
                firstNumber = Convert.ToInt32(firstNumberString);  // 0 or correspending value
            }
            else
            {
                if (context.Response.StatusCode == 200)
                {
                    context.Response.StatusCode = 400;
                }
                await context.Response.WriteAsync("Invalid input for 'firstNumber'\n");
            }
        }
        else
        {
            if (context.Response.StatusCode == 200)
            {
                context.Response.StatusCode = 400;
            }
            await context.Response.WriteAsync("Invalid input for 'firstNumber'\n");
        }


        // secondNumber
        if (context.Request.Query.ContainsKey("secondNumber"))
        {
            string secondNumberString = context.Request.Query["secondNumber"][0];
            if (!string.IsNullOrEmpty(secondNumberString))
            {
                secondNumber = Convert.ToInt32(secondNumberString);  // 0 or correspending value
            }
            else
            {
                if (context.Response.StatusCode == 200)
                {
                    context.Response.StatusCode = 400;
                }
                await context.Response.WriteAsync("Invalid input for 'secondNumber'\n");
            }
        }
        else
        {
            if (context.Response.StatusCode == 200)
            {
                context.Response.StatusCode = 400;
            }
            await context.Response.WriteAsync("Invalid input for 'secondNumber'\n");
        }

        // operation
        if (context.Request.Query.ContainsKey("operation"))
        {
            string operation = context.Request.Query["operation"][0];
            if (!string.IsNullOrEmpty(operation))
            {
                switch (operation)
                {
                    case "add":
                        result = firstNumber + secondNumber;
                        break;
                    case "sub":
                        result = firstNumber - secondNumber;
                        break;
                    case "mult":
                        result = firstNumber * secondNumber;
                        break;
                    case "div":
                        result = firstNumber / secondNumber;
                        break;
                    case "mod":
                        result = firstNumber % secondNumber;
                        break;
                    default:
                        if (context.Response.StatusCode == 200)
                        {
                            context.Response.StatusCode = 400;
                        }
                        await context.Response.WriteAsync("Invalid input for 'operation'\n");
                        break;
                }

            }
            else
            {
                if (context.Response.StatusCode == 200)
                {
                    context.Response.StatusCode = 400;
                }
                await context.Response.WriteAsync("Invalid input for 'operation'\n");
            }
        }
        else
        {
            if (context.Response.StatusCode == 200)
            {
                context.Response.StatusCode = 400;
            }
            await context.Response.WriteAsync("Invalid input for 'operation'\n");
        }

        if (context.Response.StatusCode == 200)
        {
            await context.Response.WriteAsync($"{result}");
        }

    }
});

app.Run();
