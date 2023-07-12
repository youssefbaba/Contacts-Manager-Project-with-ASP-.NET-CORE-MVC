using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceLayer;
using StocksApp.Models;

namespace StocksApp.Controllers
{
    public class HomeController : Controller
    {
        // Fields
        private readonly FinnhubService _finnhubService;
        private readonly TradingOptions _tradingOptions;

        // Constructors
        public HomeController(FinnhubService finnhubService,
            IOptions<TradingOptions> tradingOptions
            )
        {
            _finnhubService = finnhubService;
            _tradingOptions = tradingOptions.Value;
        }


        // Methods
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            if (_tradingOptions.DefaultStockSymbol == null)
            {
                _tradingOptions.DefaultStockSymbol = "MSFT";
            }

            Dictionary<string, object> responseDictionary = (await _finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol))!;

            Stock stock = new Stock()
            {
                StockSymbol = _tradingOptions.DefaultStockSymbol,
                CurrentPrice = Convert.ToDouble(responseDictionary["l"].ToString()),
                HighestPrice = Convert.ToDouble(responseDictionary["h"].ToString()),
                LowestPrice = Convert.ToDouble(responseDictionary["l"].ToString()),
                OpenPrice = Convert.ToDouble(responseDictionary["o"].ToString()),
            };

            return View(model: stock);
        }
    }
}
