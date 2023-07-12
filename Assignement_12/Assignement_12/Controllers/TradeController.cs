using Assignement_12.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;

namespace Assignement_12.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        // Fields
        private readonly IFinnhubService _finnhubService;
        private readonly TradingOptions _tradingOptions;
        private readonly IConfiguration _configuration;


        // Constructors
        public TradeController(IFinnhubService finnhubService,
            IOptions<TradingOptions> tradingOptions,
            IConfiguration configuration)
        {
            _finnhubService = finnhubService;
            _tradingOptions = tradingOptions.Value;
            _configuration = configuration;
        }

        // Methods
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(_tradingOptions.DefaultStockSymbol))
            {
                _tradingOptions.DefaultStockSymbol = "MSFT";
            }

            Dictionary<string, object>? dictionaryResponseCompanyProfile = (await _finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol))!;
            Dictionary<string, object>? dictionaryResponseStockPriceQuote = (await _finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol))!;

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
