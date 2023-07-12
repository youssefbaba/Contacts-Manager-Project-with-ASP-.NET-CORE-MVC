using Entities;

namespace ServiceContracts.DTO
{
    public class BuyOrderResponse
    {
        // Properties

        public Guid BuyOrderID { get; set; }

        public string? StockSymbol { get; set; }

        public string? StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        public uint Quantity { get; set; }

        public double Price { get; set; }

        public double TradeAmount { get; set; }

        // Methods
        public override bool Equals(object? obj)
        {
            if ((obj == null) || (obj.GetType() != typeof(BuyOrderResponse)))
            {
                return false;
            }
            BuyOrderResponse buyOrderResponse = (BuyOrderResponse)obj;
            return buyOrderResponse.BuyOrderID == BuyOrderID &&
                buyOrderResponse.StockSymbol == StockSymbol &&
                buyOrderResponse.StockName == StockName &&
                buyOrderResponse.DateAndTimeOfOrder == DateAndTimeOfOrder &&
                buyOrderResponse.Quantity == Quantity &&
                buyOrderResponse.Price == Price &&
                buyOrderResponse.TradeAmount == TradeAmount;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class BuyOrderExtensions
    {
        public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
        {
            return new BuyOrderResponse()
            {
                BuyOrderID = buyOrder.BuyOrderID,
                StockSymbol = buyOrder.StockSymbol,
                StockName = buyOrder.StockName,
                DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
                Quantity = buyOrder.Quantity,
                Price = buyOrder.Price
                // TradeAmount
            };
        }
    }
}
