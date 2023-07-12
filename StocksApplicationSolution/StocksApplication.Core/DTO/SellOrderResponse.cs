using StocksApplication.Core.Domain.Entities;

namespace StocksApplication.Core.DTO
{
    public class SellOrderResponse
    {

        // Properties
        public Guid SellOrderID { get; set; }

        public string? StockSymbol { get; set; }

        public string? StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        public uint Quantity { get; set; }

        public double Price { get; set; }

        public double TradeAmount { get; set; }

        // Methods
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != typeof(SellOrderResponse))
            {
                return false;
            }
            SellOrderResponse sellOrderResponse = (SellOrderResponse)obj;
            return sellOrderResponse.SellOrderID == SellOrderID &&
                sellOrderResponse.StockSymbol == StockSymbol &&
                sellOrderResponse.StockName == StockName &&
                sellOrderResponse.DateAndTimeOfOrder == DateAndTimeOfOrder &&
                sellOrderResponse.Quantity == Quantity &&
                sellOrderResponse.Price == Price &&
                sellOrderResponse.TradeAmount == TradeAmount;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class SellOrderExtensions
    {
        public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
        {
            return new SellOrderResponse()
            {
                SellOrderID = sellOrder.SellOrderID,
                StockSymbol = sellOrder.StockSymbol,
                StockName = sellOrder.StockName,
                DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
                Quantity = sellOrder.Quantity,
                Price = sellOrder.Price,
                TradeAmount = sellOrder.Price * sellOrder.Quantity
            };
        }
    }
}
