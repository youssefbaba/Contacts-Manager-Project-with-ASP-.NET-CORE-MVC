
using ServiceContracts.DTO;

namespace ServiceContracts.StocksService
{
    public interface IBuyOrdersService
    {
        Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest);

        Task<List<BuyOrderResponse>> GetBuyOrders();
    }
}
