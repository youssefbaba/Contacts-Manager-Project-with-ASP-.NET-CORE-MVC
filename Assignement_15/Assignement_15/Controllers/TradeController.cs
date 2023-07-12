using Assignement_15.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using ServiceContracts.DTO;
using System.Data;

namespace Assignement_15.Controllers
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

            StockTrade stockTrade = new StockTrade() {
                StockSymbol = _tradingOptions.DefaultStockSymbol
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
            return View(stockTrade);
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult BuyOrder(BuyOrderRequest buyOrderRequest)
        {
            buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;

            // Revalidate
            ModelState.Clear();
            TryValidateModel(buyOrderRequest); 

            // Model Validation
            if (!ModelState.IsValid)
            {
                List<string> errors = ModelState.Values.SelectMany(value => value.Errors)
                                    .Select(error => error.ErrorMessage)
                                    .ToList();
                ViewBag.Errors = errors;

                StockTrade stockTrade = new StockTrade()
                {
                    StockSymbol = buyOrderRequest.StockSymbol,
                    StockName = buyOrderRequest.StockName,
                    Price = buyOrderRequest.Price,
                    Quantity = buyOrderRequest.Quantity
                };

                return View(nameof(Index), stockTrade);
            }

            _stocksService.CreateBuyOrder(buyOrderRequest);

            return RedirectToAction(nameof(Orders));
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult SellOrder(SellOrderRequest sellOrderRequest)
        {
            sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;

            // Revalidate
            ModelState.Clear();
            TryValidateModel(sellOrderRequest);

            // Model Validation
            if (!ModelState.IsValid)
            {
                List<string> errors = ModelState.Values.SelectMany(value => value.Errors)
                                    .Select(error => error.ErrorMessage)
                                    .ToList();
                ViewBag.Errors = errors;

                StockTrade stockTrade = new StockTrade()
                {
                    StockSymbol = sellOrderRequest.StockSymbol,
                    StockName = sellOrderRequest.StockName,
                    Price = sellOrderRequest.Price,
                    Quantity = sellOrderRequest.Quantity
                };

                return View("Index", stockTrade);
            }

            _stocksService.CreateSellOrder(sellOrderRequest);

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
    }
}
