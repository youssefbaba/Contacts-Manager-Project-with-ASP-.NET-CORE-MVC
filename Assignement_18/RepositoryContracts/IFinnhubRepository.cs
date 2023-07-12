
namespace RepositoryContracts
{
    public interface IFinnhubRepository
    {
        Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);

        Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);

        Task<List<Dictionary<string, string>>?> GetStocks();

        Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch);
    }

}
