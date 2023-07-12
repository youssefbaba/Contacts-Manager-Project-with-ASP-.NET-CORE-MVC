using Assignement_20.Filters.ActionFilter;
using Assignement_20.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using ServiceContracts;
using ServiceContracts.DTO;
using System.Data;

namespace Assignement_20.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        //Fields
        private readonly IFinnhubService _finnhubService;
        private readonly TradingOptions _tradingOptions;
        private readonly IConfiguration _configuration;
        private readonly IStocksService _stocksService;
        private readonly ILogger<TradeController> _logger;  

        // Constructors
        public TradeController(IOptions<TradingOptions> tradingOptions,
            IFinnhubService finnhubService,
            IConfiguration configuration,
            IStocksService stocksService,
            ILogger<TradeController> logger
            )
        {
            _tradingOptions = tradingOptions.Value;
            _finnhubService = finnhubService;
            _configuration = configuration;
            _stocksService = stocksService;
            _logger = logger;
        }

        [HttpGet]
        [Route("/")]
        [Route("[action]/{stockSymbol?}")]
        public async Task<IActionResult> Index(string? stockSymbol)
        {
            _logger.LogInformation($"Index action method of TradeController");
            _logger.LogDebug($"stockSymbol: {stockSymbol}");

            if (stockSymbol == null)
            {
                stockSymbol = "MSFT"; 
            }

            Dictionary<string, object>? dictionaryResponseCompanyProfile = (await _finnhubService.GetCompanyProfile(stockSymbol));
            Dictionary<string, object>? dictionaryResponseStockPriceQuote = (await _finnhubService.GetStockPriceQuote(stockSymbol));

            StockTrade stockTrade = new StockTrade() {
                StockSymbol = stockSymbol
            };
            if (dictionaryResponseCompanyProfile != null && dictionaryResponseStockPriceQuote != null)
            {
                stockTrade = new StockTrade()
                {
                    StockSymbol = Convert.ToString(dictionaryResponseCompanyProfile["ticker"]),
                    StockName = Convert.ToString(dictionaryResponseCompanyProfile["name"]),
                    Price = Convert.ToDouble(dictionaryResponseStockPriceQuote["c"].ToString()),
                    Quantity = _tradingOptions.DefaultOrderQuantity 
                };
            }

            ViewBag.FinnhubToken = _configuration["FinnhubToken"];

            _logger.LogInformation($"The end of Index action method of TradeController");
            return View(stockTrade);
        }

        [Route("[action]")]
        [HttpPost]
        [TypeFilter(typeof(CreateOrderActionFilter))]
        public async Task<IActionResult> BuyOrder(BuyOrderRequest orderRequest)
        {
            await _stocksService.CreateBuyOrder(orderRequest);

            return RedirectToAction(nameof(Orders));
        }

        [Route("[action]")]
        [HttpPost]
        [TypeFilter(typeof(CreateOrderActionFilter))]
        public async Task<IActionResult> SellOrder(SellOrderRequest orderRequest)
        {
             await _stocksService.CreateSellOrder(orderRequest);

            return RedirectToAction("Orders");
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> Orders()
        {
            Orders orders = new Orders()
            {
                BuyOrders = await  _stocksService.GetBuyOrders(),
                SellOrders = await _stocksService.GetSellOrders()
            };
            return View(orders);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> OrdersPDF()
        {
            // Get list of orders
            Orders orders = new Orders()
            {
                BuyOrders = await _stocksService.GetBuyOrders(),
                SellOrders = await _stocksService.GetSellOrders()
            };

            // Return view as pdf
            return new ViewAsPdf(viewName: "OrdersPDF", model: orders, viewData: null);
        }
    }
}
