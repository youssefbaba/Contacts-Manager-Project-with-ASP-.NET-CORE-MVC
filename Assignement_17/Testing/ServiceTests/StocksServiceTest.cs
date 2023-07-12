using AutoFixture;
using Entities;
using FluentAssertions;
using Moq;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceLayer;

namespace Testing.ServiceTests
{
    public class StocksServiceTest
    {
        // Fields
        private readonly IFixture _fixture;

        private readonly Mock<IStocksRepository> _stocksRepositoryMock;
        private readonly IStocksRepository _stocksRepository;

        private readonly IStocksService _stocksService;

        // Constructors
        public StocksServiceTest()
        {
            _fixture = new Fixture();

            // To mock repository
            _stocksRepositoryMock = new Mock<IStocksRepository>();
            _stocksRepository = _stocksRepositoryMock.Object;

            _stocksService = new StocksService(_stocksRepository);
        }

        // Methods

        #region CreateBuyOrder

        // When we supply BuyOrderRequest as null, it should throw ArgumentNullException.
        [Fact]
        public async Task CreateBuyOrder_BuyOrderIsNull_ToBeArgumentNullException()
        {
            // Arrange 
            BuyOrderRequest? buyOrderRequest = null;

            // Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        // When we supply buyOrderQuantity as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Fact]
        public async Task CreateBuyOrder_BuyOrderQuantityIsLessThan1_ToBeArgumentException()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.DateAndTimeOfOrder, new DateTime(2020, 05, 25))
                .With(temp => temp.Quantity, (uint)0)
                .With(temp => temp.Price, 9000)
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When we supply buyOrderQuantity as 100001 (as per the specification, maximum is 100000), it should throw ArgumentException.
        [Fact]
        public async Task CreateBuyOrder_BuyOrderQuantityIsGreaterThan100000_ToBeArgumentException()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.DateAndTimeOfOrder, new DateTime(2020, 05, 25))
                .With(temp => temp.Quantity, (uint)120000)
                .With(temp => temp.Price, 9000)
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When we supply buyOrderPrice as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Fact]
        public async Task CreateBuyOrder_BuyOrderPriceIsLessThan1_ToBeArgumentException()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.DateAndTimeOfOrder, new DateTime(2020, 05, 25))
                .With(temp => temp.Quantity, (uint)90000)
                .With(temp => temp.Price, 0)
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When we supply buyOrderPrice as 10001 (as per the specification, maximum is 10000), it should throw ArgumentException.
        [Fact]
        public async Task CreateBuyOrder_BuyOrderPriceIsGreaterThan10000_ToBeArgumentException()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.DateAndTimeOfOrder, new DateTime(2020, 05, 25))
                .With(temp => temp.Quantity, (uint)90000)
                .With(temp => temp.Price, 20000)
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When we supply stock symbol=null (as per the specification, stock symbol can't be null), it should throw ArgumentException.
        [Fact]
        public async Task CreateBuyOrder_StockSymbolIsNull_ToBeArgumentException()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.StockSymbol, null as string)
                .With(temp => temp.DateAndTimeOfOrder, new DateTime(2020, 05, 25))
                .With(temp => temp.Quantity, (uint)90000)
                .With(temp => temp.Price, 9000)
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When we supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) - (as per the specification, it should be equal or newer date than 2000-01-01), it should throw ArgumentException.
        [Fact]
        public async Task CreateBuyOrder_DateAndTimeOfOrderOlderThan2000_ToBeArgumentException()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.DateAndTimeOfOrder, new DateTime(1999, 05, 25))
                .With(temp => temp.Quantity, (uint)90000)
                .With(temp => temp.Price, 9000)
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        //  If you supply all valid values, it should be successful and return an object of BuyOrderResponse type with auto-generated BuyOrderID (guid).
        [Fact]
        public async Task CreateBuyOrder_ProperBuyOrderDetails_ToBeSuccessful()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.DateAndTimeOfOrder, new DateTime(2020, 05, 25))
                .With(temp => temp.Quantity, (uint)90000)
                .With(temp => temp.Price, 9000)
                .Create();

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            BuyOrderResponse expectedBuyOrderResponse = buyOrder.ToBuyOrderResponse();

            // To mock CreateBuyOrder method
            _stocksRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
                .ReturnsAsync(buyOrder);

            // Act
            BuyOrderResponse actualBuyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);
            expectedBuyOrderResponse.BuyOrderID = actualBuyOrderResponse.BuyOrderID;

            // Assert
            actualBuyOrderResponse.BuyOrderID.Should().NotBe(Guid.Empty);
            actualBuyOrderResponse.Should().BeEquivalentTo(expectedBuyOrderResponse);
        }
        #endregion

        #region CreateSellOrder

        // When we supply SellOrderRequest as null, it should throw ArgumentNullException..
        [Fact]
        public async Task CreateSellOrder_SellOrderIsNull_ToBeArgumentNullException()
        {
            // Arrange 
            SellOrderRequest? sellOrderRequest = null;

            // Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        // WWhen we supply sellOrderQuantity as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Fact]
        public async Task CreateSellOrder_SellOrderQuantityIsLessThan1_ToBeArgumentException()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.DateAndTimeOfOrder, new DateTime(2020, 05, 25))
                .With(temp => temp.Quantity, (uint)0)
                .With(temp => temp.Price, 9000)
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When we supply sellOrderQuantity as 100001 (as per the specification, maximum is 100000), it should throw ArgumentException.
        [Fact]
        public async Task CreateSellOrder_SellOrderQuantityIsGreaterThan100000_ToBeArgumentException()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.DateAndTimeOfOrder, new DateTime(2020, 05, 25))
                .With(temp => temp.Quantity, (uint)120000)
                .With(temp => temp.Price, 9000)
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();

        }

        // When we supply sellOrderPrice as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Fact]
        public async Task CreateSellOrder_SellOrderPriceIsLessThan1_ToBeArgumentException()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.DateAndTimeOfOrder, new DateTime(2020, 05, 25))
                .With(temp => temp.Quantity, (uint)90000)
                .With(temp => temp.Price, 0)
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();

        }

        // When we supply sellOrderPrice as 10001 (as per the specification, maximum is 10000), it should throw ArgumentException.
        [Fact]
        public async Task CreateSellOrder_SellOrderPriceIsGreaterThan10000_ToBeArgumentException()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.DateAndTimeOfOrder, new DateTime(2020, 05, 25))
                .With(temp => temp.Quantity, (uint)90000)
                .With(temp => temp.Price, 20000)
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When we supply stock symbol=null (as per the specification, stock symbol can't be null), it should throw ArgumentException.
        [Fact]
        public async Task CreateSellOrder_StockSymbolIsNull_ToBeArgumentException()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                 .With(temp => temp.StockSymbol, null as string)
                 .With(temp => temp.DateAndTimeOfOrder, new DateTime(2020, 05, 25))
                 .With(temp => temp.Quantity, (uint)90000)
                 .With(temp => temp.Price, 9000)
                 .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When we supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) - (as per the specification, it should be equal or newer date than 2000-01-01), it should throw ArgumentException.
        [Fact]
        public async Task CreateSellOrder_DateAndTimeOfOrderOlderThan2000_ToBeArgumentException()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                 .With(temp => temp.DateAndTimeOfOrder, new DateTime(1999, 05, 25))
                 .With(temp => temp.Quantity, (uint)90000)
                 .With(temp => temp.Price, 9000)
                 .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        //  If we supply all valid values, it should be successful and return an object of SellOrderResponse type with auto-generated SellOrderID (guid).
        [Fact]
        public async Task CreateSellOrder_ProperSellOrderDetails_ToBeSuccessful()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = _fixture.Build<SellOrderRequest>()
                 .With(temp => temp.DateAndTimeOfOrder, new DateTime(2020, 05, 25))
                 .With(temp => temp.Quantity, (uint)90000)
                 .With(temp => temp.Price, 9000)
                 .Create();

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            SellOrderResponse expectedSellOrderResponse = sellOrder.ToSellOrderResponse();

            _stocksRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
                .ReturnsAsync(sellOrder);

            // Act
            SellOrderResponse actualSellOrderResponse = await _stocksService.CreateSellOrder(sellOrderRequest);
            expectedSellOrderResponse.SellOrderID = actualSellOrderResponse.SellOrderID;

            // Assert
            actualSellOrderResponse.SellOrderID.Should().NotBe(Guid.Empty);
            actualSellOrderResponse.Should().BeEquivalentTo(expectedSellOrderResponse);
        }
        #endregion

        #region GetBuyOrders

        // When we invoke this method, by default, the returned list should be empty.
        [Fact]
        public async Task GetBuyOrders_BuyOrdersIsEmpty_ToBeEmptyList()
        {
            // Arrange
            _stocksRepositoryMock.Setup(temp => temp.GetBuyOrders())
                .ReturnsAsync(new List<BuyOrder>());

            // Act
            List<BuyOrderResponse> actualBuyOrderResponses = await _stocksService.GetBuyOrders();

            // Assert
            actualBuyOrderResponses.Should().BeEmpty();
        }

        // When you first add few buy orders using CreateBuyOrder() method; and then invoke GetAllBuyOrders() method; the returned list should contain all the same buy orders.
        [Fact]
        public async Task GetBuyOrders_WithFewBuyOrders_ToBeSuccessful()
        {
            // Arrange
            BuyOrder buyOrderOne = _fixture.Create<BuyOrder>();
            BuyOrder buyOrderTwo = _fixture.Create<BuyOrder>();

            List<BuyOrder> buyOrders = new List<BuyOrder>() { buyOrderOne, buyOrderTwo };

            List<BuyOrderResponse> expectedBuyOrderResponseList = buyOrders.Select(temp => temp.ToBuyOrderResponse()).ToList();

            _stocksRepositoryMock.Setup(temp => temp.GetBuyOrders())
                .ReturnsAsync(buyOrders);

            // Act
            List<BuyOrderResponse> actualBuyOrderResponseList = await _stocksService.GetBuyOrders();

            // Assert
            actualBuyOrderResponseList.Should().BeEquivalentTo(expectedBuyOrderResponseList);
        }
        #endregion

        #region GetSellOrders
        // When we invoke this method, by default, the returned list should be empty.
        [Fact]
        public async Task GetSellOrders_SellOrdersIsEmpty_ToBeEmptyList()
        {
            // Arrange
            _stocksRepositoryMock.Setup(temp => temp.GetSellOrders())
                .ReturnsAsync(new List<SellOrder>());

            // Act
            List<SellOrderResponse> actualSellOrderResponseList = await _stocksService.GetSellOrders();

            // Assert
            actualSellOrderResponseList.Should().BeEmpty();

        }

        // When you first add few sell orders using CreateSellOrder() method; and then invoke GetAllSellOrders() method; the returned list should contain all the same sell orders.
        [Fact]
        public async Task GetSellOrders_WithFewSellOrders_ToBeSuccessful()
        {
            // Arrange
            SellOrder sellOrderOne = _fixture.Create<SellOrder>();
            SellOrder sellOrderTwo = _fixture.Create<SellOrder>();

            List<SellOrder> sellOrders = new List<SellOrder>() { sellOrderOne, sellOrderTwo };

            List<SellOrderResponse> expectedSellOrderResponseList = sellOrders.Select(temp => temp.ToSellOrderResponse()).ToList();

            _stocksRepositoryMock.Setup(temp => temp.GetSellOrders())
                .ReturnsAsync(sellOrders);

            // Act
            List<SellOrderResponse> actualSellOrderResponseList = await _stocksService.GetSellOrders();

            // Assert
            actualSellOrderResponseList.Should().BeEquivalentTo(expectedSellOrderResponseList);
        }
        #endregion

    }
}
