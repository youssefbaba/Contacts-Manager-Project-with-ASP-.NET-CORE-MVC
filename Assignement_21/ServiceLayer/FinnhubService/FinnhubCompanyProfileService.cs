using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts.FinnhubService;

namespace ServiceLayer.FinnhubService
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
            _logger.LogInformation("GetCompanyProfile method of FinnhubService");
            return await _finnhubRepository.GetCompanyProfile(stockSymbol);
        }
    }
}
