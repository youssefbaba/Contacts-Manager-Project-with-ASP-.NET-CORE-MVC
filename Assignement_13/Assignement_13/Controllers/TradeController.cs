using Assignement_13.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;

namespace Assignement_13.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        //Fields
        private readonly IFinnhubService _finnhubService;
        private readonly TradingOptions _tradingOptions;
        private readonly IConfiguration _configuration;
        private readonly IStocksService _stocksService;

        // Constructors
        public TradeController(IOptions<TradingOptions> tradingOptions,
            IFinnhubService finnhubService,
            IConfiguration configuration,
            IStocksService stocksService
            )
        {
            _tradingOptions = tradingOptions.Value;
            _finnhubService = finnhubService;
            _configuration = configuration;
            _stocksService = stocksService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Index()
        {
            if (_tradingOptions.DefaultStockSymbol == null)
            {
                _tradingOptions.DefaultStockSymbol = "MSFT"; 
            }

            Dictionary<string, object>? dictionaryResponseCompanyProfile = (await _finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol));
            Dictionary<string, object>? dictionaryResponseStockPriceQuote = (await _finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol));

            StockTrade stockTrade = new StockTrade() { StockSymbol = _tradingOptions.DefaultStockSymbol };
            if (dictionaryResponseCompanyProfile != null && dictionaryResponseStockPriceQuote != null)
            {
                stockTrade = new StockTrade()
                {
                    StockSymbol = Convert.ToString(dictionaryResponseCompanyProfile["ticker"]),
                    StockName = Convert.ToString(dictionaryResponseCompanyProfile["name"]),
                    Price = Convert.ToDouble(dictionaryResponseStockPriceQuote["c"].ToString())
                };
            }

            ViewBag.FinnhubToken = _configuration["FinnhubToken"];
            return View(stockTrade);
        }
    }
}
