using Assignement_21.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using ServiceContracts.FinnhubService;

namespace Assignement_21.Controllers
{
    [Route("[controller]")]
    public class StocksController : Controller
    {
        // Fields
        private readonly IFinnhubStocksService _finnhubStocksService;
        private readonly TradingOptions _tradingOptions;

        // Constructors
        public StocksController(IFinnhubStocksService finnhubStocksService,
            IOptions<TradingOptions> tradingOptions)
        {
            _finnhubStocksService = finnhubStocksService;
            _tradingOptions = tradingOptions.Value;
        }

        // Methods

        [Route("[action]/{stock?}")]
        [HttpGet]
        public async Task<IActionResult> Explore(string? stock, bool showAll = false)
        {
            // To fetch list of all available stocks in the market
            List<Dictionary<string, string>>? availableStocks = await _finnhubStocksService.GetStocks();

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
