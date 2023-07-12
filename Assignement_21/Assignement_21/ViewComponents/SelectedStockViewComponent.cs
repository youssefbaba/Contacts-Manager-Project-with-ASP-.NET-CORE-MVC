using Assignement_21.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.FinnhubService;

namespace Assignement_21.ViewComponents
{
    public class SelectedStockViewComponent : ViewComponent
    {

        // Fields
        private readonly IFinnhubCompanyProfileService _finnhubCompanyProfileService;
        private readonly IFinnhubStockPriceQuoteService _finnhubStockPriceQuoteService;

        // Constructors
        public SelectedStockViewComponent(
        IFinnhubCompanyProfileService finnhubCompanyProfileService,
        IFinnhubStockPriceQuoteService finnhubStockPriceQuoteService)
        {
            _finnhubCompanyProfileService = finnhubCompanyProfileService;
            _finnhubStockPriceQuoteService = finnhubStockPriceQuoteService;
        }

        // Methods
        public async Task<IViewComponentResult> InvokeAsync(string? stockSymbol)
        {
            // Your logic to fetch data or perform any necessary operations
            StockTrade stockTrade = new StockTrade();

            if (stockSymbol != null)
            {
                var companyProfile = await _finnhubCompanyProfileService.GetCompanyProfile(stockSymbol);
                var stockPriceQuote = await _finnhubStockPriceQuoteService.GetStockPriceQuote(stockSymbol);

                if (companyProfile != null && stockPriceQuote != null)
                {
                    stockTrade.StockName = Convert.ToString(companyProfile["name"]);
                    stockTrade.StockSymbol = Convert.ToString(companyProfile["ticker"]);
                    stockTrade.Price = Convert.ToDouble(stockPriceQuote["c"].ToString());
                    ViewBag.FinnhubIndustry = Convert.ToString(companyProfile["finnhubIndustry"]);
                    ViewBag.Logo = Convert.ToString(companyProfile["logo"]);
                    ViewBag.Exchange = Convert.ToString(companyProfile["exchange"]);
                }
            }

            // Return the view along with any model data
            return View("ShowSelectedStock", stockTrade);
        }
    }

}
