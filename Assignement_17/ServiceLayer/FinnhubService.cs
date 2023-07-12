using Microsoft.Extensions.Configuration;
using RepositoryContracts;
using ServiceContracts;
using System.Text.Json;

namespace ServiceLayer
{
    public class FinnhubService : IFinnhubService
    {

        // Fields
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IFinnhubRepository _finnhubRepository;

        // Constructors
        public FinnhubService(IHttpClientFactory httpClientFactory,
            IConfiguration configuration, IFinnhubRepository finnhubRepository)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _finnhubRepository = finnhubRepository;
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            return await _finnhubRepository.GetCompanyProfile(stockSymbol);
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            return await _finnhubRepository.GetStockPriceQuote(stockSymbol);
        }

        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
            return await _finnhubRepository.GetStocks();
        }

        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
        {
            return await _finnhubRepository.SearchStocks(stockSymbolToSearch);
        }
    }
}