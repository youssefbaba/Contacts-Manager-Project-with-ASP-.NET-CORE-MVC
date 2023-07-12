using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.FinnhubService;

namespace ServiceLayer.FinnhubService
{
    public class FinnhubStockPriceQuoteService : IFinnhubStockPriceQuoteService
    {
        // Fields
        private readonly IFinnhubRepository _finnhubRepository;
        private readonly ILogger<FinnhubStockPriceQuoteService> _logger;

        // Constructors
        public FinnhubStockPriceQuoteService(IFinnhubRepository finnhubRepository,
            ILogger<FinnhubStockPriceQuoteService> logger)
        {
            _finnhubRepository = finnhubRepository;
            _logger = logger;
        }

        // Methods
        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            _logger.LogInformation("GetStockPriceQuote method of FinnhubService");
            return await _finnhubRepository.GetStockPriceQuote(stockSymbol);
        }

    }
}
