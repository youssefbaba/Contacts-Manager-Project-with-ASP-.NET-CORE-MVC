using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceLayer;

namespace Testing
{
    public class StocksServiceTest
    {
        // Fields
        private readonly IStocksService _stocksService;

        // Constructors
        public StocksServiceTest()
        {
            _stocksService = new StocksService(new StockMarketDbContext(new DbContextOptionsBuilder<StockMarketDbContext>().Options));
        }

        // Methods

        #region CreateBuyOrder

        // When we supply BuyOrderRequest as null, it should throw ArgumentNullException.
        [Fact]
        public async Task CreateBuyOrder_BuyOrderRequestIsNull()
        {
            // Arrange 
            BuyOrderRequest? buyOrderRequest = null;

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                // Act
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        // When we supply buyOrderQuantity as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Fact]
        public async Task CreateBuyOrder_BuyOrderQuantityIsLessThan1()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
            {
                Quantity = 0
            };

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        // When we supply buyOrderQuantity as 100001 (as per the specification, maximum is 100000), it should throw ArgumentException.
        [Fact]
        public async Task CreateBuyOrder_BuyOrderQuantityIsGreaterThan100000()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
            {
                Quantity = 100001
            };

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
             {
                 // Act
                 await _stocksService.CreateBuyOrder(buyOrderRequest);
             });
        }

        // When we supply buyOrderPrice as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Fact]
        public async Task CreateBuyOrder_BuyOrderPriceIsLessThan1()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
            {
                Price = 0
            };

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
             {
                 // Act
                 await _stocksService.CreateBuyOrder(buyOrderRequest);
             });
        }

        // When we supply buyOrderPrice as 10001 (as per the specification, maximum is 10000), it should throw ArgumentException.
        [Fact]
        public async Task CreateBuyOrder_BuyOrderPriceIsGreaterThan10000()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
            {
                Price = 10001
            };

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
             {
                 // Act
                 await _stocksService.CreateBuyOrder(buyOrderRequest);
             });
        }

        // When we supply stock symbol=null (as per the specification, stock symbol can't be null), it should throw ArgumentException.
        [Fact]
        public async Task CreateBuyOrder_StockSymbolIsNull()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
            {
                StockSymbol = null
            };

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
             {
                 // Act
                 await _stocksService.CreateBuyOrder(buyOrderRequest);
             });
        }

        // When we supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) - (as per the specification, it should be equal or newer date than 2000-01-01), it should throw ArgumentException.
        [Fact]
        public async Task CreateBuyOrder_DateAndTimeOfOrderOlderThan2000()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
            {
                DateAndTimeOfOrder = Convert.ToDateTime("1999-12-31")
            };

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
             {
                 // Act
                 await _stocksService.CreateBuyOrder(buyOrderRequest);
             });
        }

        //  If you supply all valid values, it should be successful and return an object of BuyOrderResponse type with auto-generated BuyOrderID (guid).
        [Fact]
        public async Task CreateBuyOrder_ProperBuyOrderRequestDetails()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest = new BuyOrderRequest()
            {
                StockSymbol = "GOOG",
                StockName = "Google",
                DateAndTimeOfOrder = new DateTime(2005, 02, 15),
                Quantity = 10,
                Price = 333.45
            };

            // Act
            BuyOrderResponse buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);
            List<BuyOrderResponse> buyOrderResponses = await _stocksService.GetBuyOrders();

            // Assert
            Assert.True(buyOrderResponse.BuyOrderID != Guid.Empty);
            Assert.Contains(buyOrderResponse, buyOrderResponses);
        }
        #endregion


        #region CreateSellOrder

        // When we supply SellOrderRequest as null, it should throw ArgumentNullException..
        [Fact]
        public async Task CreateSellOrder_SellOrderRequestIsNull()
        {
            // Arrange 
            SellOrderRequest? sellOrderRequest = null;

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
             {
                 // Act
                 await _stocksService.CreateSellOrder(sellOrderRequest);
             });
        }

        // WWhen we supply sellOrderQuantity as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Fact]
        public async Task CreateSellOrder_SellOrderQuantityIsLessThan1()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = new SellOrderRequest()
            {
                Quantity = 0
            };

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
             {
                 // Act
                 await _stocksService.CreateSellOrder(sellOrderRequest);
             });
        }

        // When we supply sellOrderQuantity as 100001 (as per the specification, maximum is 100000), it should throw ArgumentException.
        [Fact]
        public async Task CreateSellOrder_SellOrderQuantityIsGreaterThan100000()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = new SellOrderRequest()
            {
                Quantity = 100001
            };

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
             {
                 // Act
                 await _stocksService.CreateSellOrder(sellOrderRequest);
             });
        }

        // When we supply sellOrderPrice as 0 (as per the specification, minimum is 1), it should throw ArgumentException.
        [Fact]
        public async Task CreateSellOrder_SellOrderPriceIsLessThan1()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = new SellOrderRequest()
            {
                Price = 0
            };

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
              {
                  // Act
                  await _stocksService.CreateSellOrder(sellOrderRequest);
              });
        }

        // When we supply sellOrderPrice as 10001 (as per the specification, maximum is 10000), it should throw ArgumentException.
        [Fact]
        public async Task CreateSellOrder_SellOrderPriceIsGreaterThan10000()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = new SellOrderRequest()
            {
                Price = 10001
            };

            // Assert
           await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        // When we supply stock symbol=null (as per the specification, stock symbol can't be null), it should throw ArgumentException.
        [Fact]
        public async Task CreateSellOrder_StockSymbolIsNull()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = null
            };

            // Assert
           await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        // When we supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) - (as per the specification, it should be equal or newer date than 2000-01-01), it should throw ArgumentException.
        [Fact]
        public async Task CreateSellOrder_DateAndTimeOfOrderOlderThan2000()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = new SellOrderRequest()
            {
                DateAndTimeOfOrder = Convert.ToDateTime("1999-12-31")
            };

            // Assert
          await  Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        //  If we supply all valid values, it should be successful and return an object of SellOrderResponse type with auto-generated SellOrderID (guid).
        [Fact]
        public async Task CreateSellOrder_ProperSellOrderRequestDetails()
        {
            // Arrange
            SellOrderRequest sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = "GOOG",
                StockName = "Google",
                DateAndTimeOfOrder = new DateTime(2005, 02, 15),
                Quantity = 10,
                Price = 333.45
            };

            // Act
            SellOrderResponse sellOrderResponse = await _stocksService.CreateSellOrder(sellOrderRequest);
            List<SellOrderResponse> sellOrderResponses = await _stocksService.GetSellOrders();

            // Assert
            Assert.True(sellOrderResponse.SellOrderID != Guid.Empty);
            Assert.Contains(sellOrderResponse, sellOrderResponses);
        }
        #endregion

        #region GetBuyOrders

        // When we invoke this method, by default, the returned list should be empty.
        [Fact]
        public async Task GetBuyOrders_BuyOrdersIsEmpty()
        {
            // Act
            List<BuyOrderResponse> buyOrderResponses = await _stocksService.GetBuyOrders();

            // Assert
            Assert.Empty(buyOrderResponses);
        }

        // When you first add few buy orders using CreateBuyOrder() method; and then invoke GetAllBuyOrders() method; the returned list should contain all the same buy orders.
        [Fact]
        public async Task GetBuyOrders_FullOfBuyOrders()
        {
            List<BuyOrderRequest> buyOrderRequests = new List<BuyOrderRequest>()
            {
                new BuyOrderRequest()
                {
                    StockSymbol = "GOOG",
                    StockName = "Google",
                    DateAndTimeOfOrder = new DateTime(2005, 02, 15),
                    Quantity = 10,
                    Price = 333.45
                },
                new BuyOrderRequest()
                {
                    StockSymbol = "MSFT",
                    StockName = "Microsoft",
                    DateAndTimeOfOrder = new DateTime(2002, 01, 06),
                    Quantity = 15,
                    Price = 124.25
                }
            };

            List<BuyOrderResponse> buyOrderResponses = new List<BuyOrderResponse>();
            foreach (var buyOrderRequest in buyOrderRequests)
            {
                buyOrderResponses.Add(await _stocksService.CreateBuyOrder(buyOrderRequest));
            }

            // Act
            List<BuyOrderResponse> actualBuyOrderResponses = await _stocksService.GetBuyOrders();

            // Assert
            foreach (var buyOrderResponse in buyOrderResponses)
            {
                Assert.Contains(buyOrderResponse, actualBuyOrderResponses);
            }
        }
        #endregion

        #region GetSellOrders
        // When we invoke this method, by default, the returned list should be empty.
        [Fact]
        public async Task GetSellOrders_SellOrdersIsEmpty()
        {
            // Act
            List<SellOrderResponse> sellOrderResponses = await _stocksService.GetSellOrders();

            // Assert
            Assert.Empty(sellOrderResponses);
        }

        // When you first add few sell orders using CreateSellOrder() method; and then invoke GetAllSellOrders() method; the returned list should contain all the same sell orders.
        [Fact]
        public async Task GetSellOrders_FullOfSellOrders()
        {
            List<SellOrderRequest> sellOrderRequests = new List<SellOrderRequest>()
            {
                new SellOrderRequest()
                {
                    StockSymbol = "GOOG",
                    StockName = "Google",
                    DateAndTimeOfOrder = new DateTime(2005, 02, 15),
                    Quantity = 10,
                    Price = 333.45
                },
                new SellOrderRequest()
                {
                    StockSymbol = "MSFT",
                    StockName = "Microsoft",
                    DateAndTimeOfOrder = new DateTime(2002, 01, 06),
                    Quantity = 15,
                    Price = 124.25
                }
            };

            List<SellOrderResponse> sellOrderResponses = new List<SellOrderResponse>();
            foreach (var sellOrderRequest in sellOrderRequests)
            {
                sellOrderResponses.Add(await _stocksService.CreateSellOrder(sellOrderRequest));
            }

            // Act
            List<SellOrderResponse> actualSellOrderResponses = await _stocksService.GetSellOrders();

            // Assert
            foreach (var sellOrderResponse in sellOrderResponses)
            {
                Assert.Contains(sellOrderResponse, actualSellOrderResponses);
            }
        }
        #endregion

    }
}
