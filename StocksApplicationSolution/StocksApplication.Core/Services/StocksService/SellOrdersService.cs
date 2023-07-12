using StocksApplication.Core.Domain.Entities;
using StocksApplication.Core.Domain.RepositoryContracts;
using StocksApplication.Core.DTO;
using StocksApplication.Core.Helpers;
using StocksApplication.Core.ServiceContracts.StocksService;

namespace StocksApplication.Core.Services.StocksService
{
    public class SellOrdersService : ISellOrdersService
    {
        // Fields
        private readonly IStocksRepository _stocksRepository;

        // Constructors
        public SellOrdersService(IStocksRepository stocksRepository)
        {
            _stocksRepository = stocksRepository;
        }

        // Methods
        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(SellOrderRequest));
            }

            ValidationHelper.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            sellOrder.SellOrderID = Guid.NewGuid();

            await _stocksRepository.CreateSellOrder(sellOrder);

            return sellOrder.ToSellOrderResponse();
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            return (await _stocksRepository.GetSellOrders())
                  .OrderByDescending(sellOrder => sellOrder.DateAndTimeOfOrder)
                  .Select(sellOrder => sellOrder.ToSellOrderResponse())
                  .ToList();
        }
    }
}
