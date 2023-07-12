using Microsoft.Extensions.Configuration;
using ServiceContracts;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace ServiceLayer
{
    public class FinnhubService : IFinnhubService
    {

        // Fields
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        // Constructors
        public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {
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
    }
}