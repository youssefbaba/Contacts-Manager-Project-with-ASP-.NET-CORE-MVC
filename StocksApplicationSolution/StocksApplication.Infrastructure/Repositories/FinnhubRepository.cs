using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StocksApplication.Core.Domain.RepositoryContracts;
using System.Text.Json;

namespace StocksApplication.Infrastructure.Repositories
{
    public class FinnhubRepository : IFinnhubRepository
    {
        // Fields
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<FinnhubRepository> _logger;

        // Constructors
        public FinnhubRepository(IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            ILogger<FinnhubRepository> logger
            )
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        // Methods
        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
            _logger.LogInformation("GetCompanyProfile method of FinnhubRepository");
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequest = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}"),
                    Method = HttpMethod.Get,
                };

                HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest);
                Stream stream = httpResponse.Content.ReadAsStream();
                StreamReader streamReader = new StreamReader(stream);
                string response = streamReader.ReadToEnd();
                Dictionary<string, object>? dictionaryResponse = JsonSerializer.Deserialize<Dictionary<string, object>>(response);
                if (dictionaryResponse == null)
                {
                    throw new InvalidOperationException("No response from finnhub service");
                }
                if (dictionaryResponse.ContainsKey("error"))
                {
                    throw new InvalidOperationException(Convert.ToString(dictionaryResponse["error"]));
                }
                return dictionaryResponse;
            }
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            _logger.LogInformation("GetStockPriceQuote method of FinnhubRepository");
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequest = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}"),
                    Method = HttpMethod.Get,
                };

                HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest);
                Stream stream = httpResponse.Content.ReadAsStream();
                StreamReader streamReader = new StreamReader(stream);
                string response = streamReader.ReadToEnd();
                Dictionary<string, object>? dictionaryResponse = JsonSerializer.Deserialize<Dictionary<string, object>>(response);
                if (dictionaryResponse == null)
                {
                    throw new InvalidOperationException("No response from finnhub service");
                }
                if (dictionaryResponse.ContainsKey("error"))
                {
                    throw new InvalidOperationException(Convert.ToString(dictionaryResponse["error"]));
                }
                return dictionaryResponse;
            }
        }

        public async Task<List<Dictionary<string, string>>?> GetStocks()
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequest = new HttpRequestMessage()
                {
                    RequestUri = new Uri($" https://finnhub.io/api/v1/stock/symbol?exchange=US&token={_configuration["FinnhubToken"]}"),
                    Method = HttpMethod.Get,
                };

                HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest);
                Stream stream = httpResponse.Content.ReadAsStream();
                StreamReader streamReader = new StreamReader(stream);
                string response = streamReader.ReadToEnd();
                var listDictionaryResponse = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(response);
                if (listDictionaryResponse != null && listDictionaryResponse.Count > 0)
                {
                    return listDictionaryResponse;
                }
                else
                {
                    throw new InvalidOperationException("No stock symbols found in the response.");
                }
            }
        }

        public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequest = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/search?q={stockSymbolToSearch}&token={_configuration["FinnhubToken"]}"),
                    Method = HttpMethod.Get,
                };

                HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest);
                Stream stream = httpResponse.Content.ReadAsStream();
                StreamReader streamReader = new StreamReader(stream);
                string response = streamReader.ReadToEnd();
                Dictionary<string, object>? dictionaryResponse = JsonSerializer.Deserialize<Dictionary<string, object>>(response);
                if (dictionaryResponse == null)
                {
                    throw new InvalidOperationException("No response from finnhub service");
                }
                if (dictionaryResponse.ContainsKey("error"))
                {
                    throw new InvalidOperationException(Convert.ToString(dictionaryResponse["error"]));
                }
                return dictionaryResponse;
            }
        }
    }
}
