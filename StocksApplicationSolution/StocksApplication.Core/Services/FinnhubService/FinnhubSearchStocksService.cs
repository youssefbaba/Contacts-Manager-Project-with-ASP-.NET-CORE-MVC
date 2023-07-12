using StocksApplication.Core.Domain.RepositoryContracts;
using StocksApplication.Core.Exceptions;
using StocksApplication.Core.ServiceContracts.FinnhubService;

namespace StocksApplication.Core.Services.FinnhubService
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
            try
            {
                return await _finnhubRepository.SearchStocks(stockSymbolToSearch);
            }
            catch (Exception exp)
            {
                FinnhubException finnhubException = new FinnhubException("Unable to connect to finnhub", exp);
                throw finnhubException;
            }
        }
    }
}
