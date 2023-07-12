using Microsoft.Extensions.Logging;
using StocksApplication.Core.Domain.RepositoryContracts;
using StocksApplication.Core.Exceptions;
using StocksApplication.Core.ServiceContracts.FinnhubService;

namespace StocksApplication.Core.Services.FinnhubService
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
            try
            {
                _logger.LogInformation("GetStockPriceQuote method of FinnhubService");
                return await _finnhubRepository.GetStockPriceQuote(stockSymbol);
            }
            catch (Exception exp)
            {
                FinnhubException finnhubException = new FinnhubException("Unable to connect to finnhub", exp);
                throw finnhubException;
            }
        }
    }
}
