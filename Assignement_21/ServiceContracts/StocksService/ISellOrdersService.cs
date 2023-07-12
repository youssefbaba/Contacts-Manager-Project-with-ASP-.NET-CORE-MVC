
using ServiceContracts.DTO;

namespace ServiceContracts.StocksService
{
    public interface ISellOrdersService
    {
        Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest);

        Task<List<SellOrderResponse>> GetSellOrders();
    }
}
