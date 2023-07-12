using StocksApplication.Core.DTO;

namespace StocksApplication.Core.ServiceContracts.StocksService
{
    public interface IBuyOrdersService
    {
        Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest);

        Task<List<BuyOrderResponse>> GetBuyOrders();
    }
}
