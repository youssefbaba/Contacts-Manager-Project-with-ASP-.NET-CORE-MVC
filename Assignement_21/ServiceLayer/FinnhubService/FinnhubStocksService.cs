using RepositoryContracts;
using ServiceContracts.FinnhubService;

namespace ServiceLayer.FinnhubService
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
            return await _finnhubRepository.GetStocks();
        }
    }
}
