using StocksApplication.Core.Domain.RepositoryContracts;
using StocksApplication.Core.Exceptions;
using StocksApplication.Core.ServiceContracts.FinnhubService;

namespace StocksApplication.Core.Services.FinnhubService
{
    public class FinnhubStocksService : IFinnhubStocksService
    {
        // Fields
        private readonly IFinnhubRepository _finnhubRepository;

        // Constructors
        public FinnhubStocksService(IFinnhubRepository finnhubRepository)
        {
            _finnhubRepository = finnhubRepository;
        }

        // Methods
        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
            try
            {
                return await _finnhubRepository.GetStocks();
            }
            catch (Exception exp)
            {
                FinnhubException finnhubException = new FinnhubException("Unable to connect to finnhub", exp);
                throw finnhubException;
            }
        }
    }
}
