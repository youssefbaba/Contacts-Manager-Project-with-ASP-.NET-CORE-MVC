
namespace StocksApplication.Core.ServiceContracts.FinnhubService
{
    public interface IFinnhubSearchStocksService
    {
        Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch);
    }
}
