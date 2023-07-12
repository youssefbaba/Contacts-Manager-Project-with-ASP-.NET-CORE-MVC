using StocksApplication.Core.DTO;

namespace StocksApplication.Core.ServiceContracts.StocksService
{
    public interface ISellOrdersService
    {
        Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest);

        Task<List<SellOrderResponse>> GetSellOrders();
    }
}
