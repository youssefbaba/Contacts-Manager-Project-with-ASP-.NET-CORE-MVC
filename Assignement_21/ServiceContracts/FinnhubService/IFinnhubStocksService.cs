
namespace ServiceContracts.FinnhubService
{
    public interface IFinnhubStocksService
    {
        Task<List<Dictionary<string, string>>?> GetStocks();
    }
}
