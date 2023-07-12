using AutoFixture;
using Moq;
using ServiceContracts;
using Assignement_21.Controllers;
using Microsoft.Extensions.Options;
using Assignement_21.Models;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Newtonsoft.Json;
using ServiceContracts.FinnhubService;

namespace Testing.ControllerTests
{
    public class StocksControllerTest
    {
        // Fields

        private readonly StocksController _stocksController;

        private readonly Mock<IFinnhubStocksService> _finnhubStocksServiceMock;
        private readonly IFinnhubStocksService _finnhubStocksService;

        private readonly IOptions<TradingOptions> _tradingOptions;

        // Constructors

        public StocksControllerTest()
        {

            _finnhubStocksServiceMock = new Mock<IFinnhubStocksService>();
            _finnhubStocksService = _finnhubStocksServiceMock.Object;

            _tradingOptions = Options.Create(new TradingOptions() { DefaultOrderQuantity = 100, Top25PopularStocks = "AAPL,MSFT,AMZN,TSLA,GOOGL,GOOG,NVDA,BRK.B,META,UNH,JNJ,JPM,V,PG,XOM,HD,CVX,MA,BAC,ABBV,PFE,AVGO,COST,DIS,KO" });

            _stocksController = new StocksController(_finnhubStocksService, _tradingOptions);
        }

        // Method

        #region Explore
        [Fact]
        public async Task Explore_ToReturnExploreViewWithStocksList()
        {
            // Arrange
            string json = @"[
                  {
                    ""currency"": ""USD"",
                    ""description"": ""UAN POWER CORP"",
                    ""displaySymbol"": ""UPOW"",
                    ""figi"": ""BBG000BGHYF2"",
                    ""mic"": ""OTCM"",
                    ""symbol"": ""UPOW"",
                    ""type"": ""Common Stock""
                  },
                  {
                    ""currency"": ""USD"",
                    ""description"": ""APPLE INC"",
                    ""displaySymbol"": ""AAPL"",
                    ""figi"": ""BBG000B9Y5X2"",
                    ""mic"": ""XNGS"",
                    ""symbol"": ""AAPL"",
                    ""type"": ""Common Stock""
                  },
                  {
                    ""currency"": ""USD"",
                    ""description"": ""EXCO TECHNOLOGIES LTD"",
                    ""displaySymbol"": ""EXCOF"",
                    ""figi"": ""BBG000JHDDS8"",
                    ""mic"": ""OOTC"",
                    ""symbol"": ""EXCOF"",
                    ""type"": ""Common Stock""
                  }
            ]";
            List<Dictionary<string, string>>? stocks = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(json);

            _finnhubStocksServiceMock.Setup(temp => temp.GetStocks())
                .ReturnsAsync(stocks);

            List<Stock> expectedStocksList = stocks!.Select(temp => new Stock() { StockName = Convert.ToString(temp["description"]), StockSymbol = Convert.ToString(temp["symbol"]) }).ToList();

            // Act
            IActionResult Result = await _stocksController.Explore(null, true);

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(Result);
            viewResult.ViewData.Model.Should().BeAssignableTo<IEnumerable<Stock>>();
            viewResult.ViewData.Model.Should().BeEquivalentTo(expectedStocksList);

        }
        #endregion 
    }
}
