namespace Assignement_17.Models
{
    public class TradingOptions
    {

        public string? Top25PopularStocks { get; set; }

        public uint DefaultOrderQuantity { get; set; }

#nullable disable
        public string DefaultStockSymbol { get; set; }
#nullable restore

    }
}
