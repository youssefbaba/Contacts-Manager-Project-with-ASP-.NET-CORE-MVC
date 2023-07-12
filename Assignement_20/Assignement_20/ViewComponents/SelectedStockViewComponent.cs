using Assignement_20.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assignement_20.ViewComponents
{
    public class SelectedStockViewComponent : ViewComponent
    {

        // Fields
        private readonly IFinnhubService _finnhubService;

        // Constructors
        public SelectedStockViewComponent(IFinnhubService finnhubService)
        {
            _finnhubService = finnhubService;
        }

        // Methods
        public async Task<IViewComponentResult> InvokeAsync(string? stockSymbol)
        {
            // Your logic to fetch data or perform any necessary operations
            StockTrade stockTrade = new StockTrade();

            if (stockSymbol != null)
            {
                var companyProfile = await _finnhubService.GetCompanyProfile(stockSymbol);
                var stockPriceQuote = await _finnhubService.GetStockPriceQuote(stockSymbol);

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
