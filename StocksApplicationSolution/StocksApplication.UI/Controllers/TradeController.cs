using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;
using StocksApplication.Core.DTO;
using StocksApplication.Core.ServiceContracts.FinnhubService;
using StocksApplication.Core.ServiceContracts.StocksService;
using StocksApplication.UI.Filters.ActionFilter;
using StocksApplication.UI.ViewModels;

namespace StocksApplication.UI.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        //Fields
        private readonly IFinnhubCompanyProfileService _finnhubCompanyProfileService;
        private readonly IFinnhubStockPriceQuoteService _finnhubStockPriceQuoteService;
        private readonly TradingOptions _tradingOptions;
        private readonly IConfiguration _configuration;
        private readonly IBuyOrdersService _buyOrdersService;
        private readonly ISellOrdersService _sellOrdersService;
        private readonly ILogger<TradeController> _logger;

        // Constructors
        public TradeController(IOptions<TradingOptions> tradingOptions,
        IFinnhubCompanyProfileService finnhubCompanyProfileService,
        IFinnhubStockPriceQuoteService finnhubStockPriceQuoteService,
        IConfiguration configuration,
        IBuyOrdersService buyOrdersService,
        ISellOrdersService sellOrdersService,
        ILogger<TradeController> logger
            )
        {
            _tradingOptions = tradingOptions.Value;
            _finnhubCompanyProfileService = finnhubCompanyProfileService;
            _finnhubStockPriceQuoteService = finnhubStockPriceQuoteService;
            _configuration = configuration;
            _sellOrdersService = sellOrdersService;
            _buyOrdersService = buyOrdersService;
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

            Dictionary<string, object>? dictionaryResponseCompanyProfile = await _finnhubCompanyProfileService.GetCompanyProfile(stockSymbol);
            Dictionary<string, object>? dictionaryResponseStockPriceQuote = await _finnhubStockPriceQuoteService.GetStockPriceQuote(stockSymbol);

            StockTrade stockTrade = new StockTrade()
            {
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
            await _buyOrdersService.CreateBuyOrder(orderRequest);

            return RedirectToAction(nameof(Orders));
        }

        [Route("[action]")]
        [HttpPost]
        [TypeFilter(typeof(CreateOrderActionFilter))]
        public async Task<IActionResult> SellOrder(SellOrderRequest orderRequest)
        {
            await _sellOrdersService.CreateSellOrder(orderRequest);

            return RedirectToAction("Orders");
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> Orders()
        {
            Orders orders = new Orders()
            {
                BuyOrders = await _buyOrdersService.GetBuyOrders(),
                SellOrders = await _sellOrdersService.GetSellOrders()
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
                BuyOrders = await _buyOrdersService.GetBuyOrders(),
                SellOrders = await _sellOrdersService.GetSellOrders()
            };

            // Return view as pdf
            return new ViewAsPdf(viewName: "OrdersPDF", model: orders, viewData: null);
        }
    }
}
