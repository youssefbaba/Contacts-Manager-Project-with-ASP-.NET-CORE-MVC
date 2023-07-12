using Microsoft.AspNetCore.Mvc.Filters;
using StocksApplication.Core.DTO;
using StocksApplication.UI.Controllers;
using StocksApplication.UI.ViewModels;

namespace StocksApplication.UI.Filters.ActionFilter
{
    public class CreateOrderActionFilter : IAsyncActionFilter
    {
        // Fields
        private readonly ILogger<CreateOrderActionFilter> _logger;

        // Constructors
        public CreateOrderActionFilter(ILogger<CreateOrderActionFilter> logger)
        {
            _logger = logger;
        }

        // Methods
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // To Do: before logic here
            _logger.LogInformation("before logic of {FilterName}.{MethodName} method", nameof(CreateOrderActionFilter), nameof(OnActionExecutionAsync));
            if (context.Controller is TradeController tradeController)
            {
                var orderRequest = context.ActionArguments["orderRequest"] as IOrderRequest;
                if (orderRequest != null)
                {
                    orderRequest.DateAndTimeOfOrder = DateTime.Now;

                    // Revalidate
                    tradeController.ModelState.Clear();
                    tradeController.TryValidateModel(orderRequest);

                    // Model Validation
                    if (!tradeController.ModelState.IsValid)
                    {
                        List<string> errors = tradeController.ModelState.Values.SelectMany(value => value.Errors)
                                            .Select(error => error.ErrorMessage)
                                            .ToList();
                        tradeController.ViewBag.Errors = errors;

                        StockTrade stockTrade = new StockTrade()
                        {
                            StockSymbol = orderRequest.StockSymbol,
                            StockName = orderRequest.StockName,
                            Price = orderRequest.Price,
                            Quantity = orderRequest.Quantity
                        };

                        context.Result = tradeController.View(nameof(Index), stockTrade);
                    }
                    else
                    {
                        await next();
                    }
                }
                else
                {
                    await next();
                }
            }
            else
            {
                await next();
            }
        }
    }
}
