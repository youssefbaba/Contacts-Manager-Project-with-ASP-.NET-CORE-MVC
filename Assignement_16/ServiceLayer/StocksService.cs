using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceLayer.Helpers;

namespace ServiceLayer
{
    public class StocksService : IStocksService
    {
        // Fields
        private readonly StockMarketDbContext _db;

        // Constructors
        public StocksService(StockMarketDbContext db)
        {
            _db = db;
        }

        // Methods

        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(BuyOrderRequest));
            }

            ValidationHelper.ModelValidation(buyOrderRequest);

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            buyOrder.BuyOrderID = Guid.NewGuid();

            _db.BuyOrders.Add(buyOrder);
            await _db.SaveChangesAsync();

            return buyOrder.ToBuyOrderResponse();
        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(SellOrderRequest));
            }

            ValidationHelper.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            sellOrder.SellOrderID = Guid.NewGuid();

            _db.SellOrders.Add(sellOrder);
            await _db.SaveChangesAsync();

            return sellOrder.ToSellOrderResponse();
        }

        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            List<BuyOrder> buyOrders = await _db.BuyOrders.ToListAsync();

            return buyOrders
                  .OrderByDescending(buyOrder => buyOrder.DateAndTimeOfOrder)
                  .Select(buyOrder => buyOrder.ToBuyOrderResponse())
                  .ToList();
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            List<SellOrder> sellOrders = await _db.SellOrders.ToListAsync();
            return sellOrders
                  .OrderByDescending(sellOrder => sellOrder.DateAndTimeOfOrder)
                  .Select(sellOrder => sellOrder.ToSellOrderResponse())
                  .ToList();
        }
    }
}
