using Assignement_19.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;

namespace Assignement_19.Controllers
{
    [Route("[controller]")]
    public class StocksController : Controller
    {
        // Fields
        private readonly IFinnhubService _finnhubService;
        private readonly TradingOptions _tradingOptions;

        // Constructors
        public StocksController(IFinnhubService finnhubService,
            IOptions<TradingOptions> tradingOptions)
        {
            _finnhubService = finnhubService;
            _tradingOptions = tradingOptions.Value;
        }

        // Methods

        [Route("[action]/{stock?}")]
        [HttpGet]
        public async Task<IActionResult> Explore(string? stock, bool showAll = false)
        {
            // To fetch list of all available stocks in the market
            List<Dictionary<string, string>>? availableStocks = await _finnhubService.GetStocks();

            List<Stock> listOfStocks = new List<Stock>();

            if (availableStocks != null)
            {
                if (!showAll && _tradingOptions.Top25PopularStocks != null)
                {
                    // Popular Stocks
                    string[]? popularStocks = _tradingOptions.Top25PopularStocks.Split(',').ToArray();
                    if (popularStocks != null)
                    {
                        availableStocks = availableStocks.Where(temp => popularStocks.Contains(Convert.ToString(temp["symbol"]))).ToList();
                    }
                }
                listOfStocks = availableStocks
                               .Select(temp => new Stock() { StockName = Convert.ToString(temp["description"]), StockSymbol = Convert.ToString(temp["symbol"]) }).ToList();
            }

            ViewBag.Stock = stock;

            return View(listOfStocks);
        }
    }
}
