using RepositoryContracts;
using ServiceContracts.FinnhubService;

namespace ServiceLayer.FinnhubService
{
    public class FinnhubSearchStocksService : IFinnhubSearchStocksService
    {
        // Fields
        private readonly IFinnhubRepository _finnhubRepository;


        // Constructors
        public FinnhubSearchStocksService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }

        // Methods
        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
        {
            return await _finnhubRepository.SearchStocks(stockSymbolToSearch);
        }
    }
}
