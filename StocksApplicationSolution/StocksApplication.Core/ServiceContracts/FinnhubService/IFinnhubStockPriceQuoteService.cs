
namespace StocksApplication.Core.ServiceContracts.FinnhubService
{
    public interface IFinnhubStockPriceQuoteService
    {
        Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);
    }
}
