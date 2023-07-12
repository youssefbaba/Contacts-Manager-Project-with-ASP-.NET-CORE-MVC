using StocksApplication.Core.Domain.Entities;
using StocksApplication.Core.Domain.RepositoryContracts;
using StocksApplication.Core.DTO;
using StocksApplication.Core.Helpers;
using StocksApplication.Core.ServiceContracts.StocksService;

namespace StocksApplication.Core.Services.StocksService
{
    public class BuyOrdersService : IBuyOrdersService
    {
        // Fields
        private readonly IStocksRepository _stocksRepository;

        // Constructors
        public BuyOrdersService(IStocksRepository stocksRepository)
        {
            _stocksRepository = stocksRepository;
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

            await _stocksRepository.CreateBuyOrder(buyOrder);

            return buyOrder.ToBuyOrderResponse();
        }

        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            return (await _stocksRepository.GetBuyOrders())
                  .OrderByDescending(buyOrder => buyOrder.DateAndTimeOfOrder)
                  .Select(buyOrder => buyOrder.ToBuyOrderResponse())
                  .ToList();
        }
    }
}
