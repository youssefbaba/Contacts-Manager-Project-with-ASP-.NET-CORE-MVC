using Microsoft.Extensions.Logging;
using StocksApplication.Core.Domain.RepositoryContracts;
using StocksApplication.Core.Exceptions;
using StocksApplication.Core.ServiceContracts.FinnhubService;

namespace StocksApplication.Core.Services.FinnhubService
{
    public class FinnhubCompanyProfileService : IFinnhubCompanyProfileService
    {
        // Fields
        private readonly IFinnhubRepository _finnhubRepository;
        private readonly ILogger<FinnhubCompanyProfileService> _logger;

        // Constructors
        public FinnhubCompanyProfileService(IFinnhubRepository finnhubRepository,
            ILogger<FinnhubCompanyProfileService> logger)
        {
            _finnhubRepository = finnhubRepository;
            _logger = logger;
        }

        // Methods
        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            try
            {
                _logger.LogInformation("GetCompanyProfile method of FinnhubService");
                return await _finnhubRepository.GetCompanyProfile(stockSymbol);
            }
            catch (Exception exp)
            {
                FinnhubException finnhubException = new FinnhubException("Unable to connect to finnhub", exp);
                throw finnhubException;
            }
        }
    }
}
