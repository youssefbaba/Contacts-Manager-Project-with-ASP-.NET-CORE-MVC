using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceLayer.Helpers;

namespace ServiceLayer
{
    public class StocksService : IStocksService
    {
        // Fields
        private readonly List<BuyOrder> _buyOrders;
        private readonly List<SellOrder> _sellOrders;

        // Constructors
        public StocksService()
        {
            _buyOrders = new List<BuyOrder>();
            _sellOrders = new List<SellOrder>();    
        }

        // Methods

        private BuyOrderResponse BuyOrderToBuyOrderResponse(BuyOrder buyOrder)
        {
            BuyOrderResponse buyOrderResponse = buyOrder.ToBuyOrderResponse();
            buyOrderResponse.TradeAmount = buyOrderResponse.Price * buyOrderResponse.Quantity;
            return buyOrderResponse;
        }

        private SellOrderResponse SellOrderToSellOrderResponse(SellOrder sellOrder)
        {
            SellOrderResponse sellOrderResponse = sellOrder.ToSellOrderResponse();
            sellOrderResponse.TradeAmount = sellOrderResponse.Price * sellOrderResponse.Quantity;
            return sellOrderResponse;
        }

        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(BuyOrderRequest));
            }

            ValidationHelper.ModelValidation(buyOrderRequest);

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            buyOrder.BuyOrderID = Guid.NewGuid();

            _buyOrders.Add(buyOrder);

            await Task.Delay(0);
            return  BuyOrderToBuyOrderResponse(buyOrder);
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

            _sellOrders.Add(sellOrder);

            await Task.Delay(0);
            return SellOrderToSellOrderResponse(sellOrder);
        }

        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            await Task.Delay(0);
            return _buyOrders.Select(buyOrder => BuyOrderToBuyOrderResponse(buyOrder))
                .ToList();
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            await Task.Delay(0);
            return _sellOrders.Select(sellOrder => SellOrderToSellOrderResponse(sellOrder))
                .ToList();
        }
    }
}
