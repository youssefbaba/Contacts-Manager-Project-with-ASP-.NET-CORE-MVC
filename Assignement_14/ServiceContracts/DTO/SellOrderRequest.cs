using Entities;
using ServiceContracts.CustomValidators;
using System.ComponentModel.DataAnnotations;


namespace ServiceContracts.DTO
{
    public class SellOrderRequest
    {

        // Methods

        [Display(Name = "Stock Symbol")]
        [Required(ErrorMessage = "{0} can't be blank")]
        public string? StockSymbol { get; set; }

        [Display(Name = "Stock Name")]
        [Required(ErrorMessage = "{0} can't be blank")]
        public string? StockName { get; set; }

        [MinimumDateAndTimeOfOrder("2000-01-01")]
        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000, ErrorMessage = "{0} should be between ${1} and ${2}")]
        public uint Quantity { get; set; }


        [Range(1, 10000, ErrorMessage = "{0} should be between ${1} and ${2}")]
        public double Price { get; set; }

        // Methods

        public SellOrder ToSellOrder()
        {
            return new SellOrder()
            {
                StockSymbol = StockSymbol,
                StockName = StockName,
                DateAndTimeOfOrder = DateAndTimeOfOrder,
                Quantity = Quantity,
                Price = Price
            };
        }
    }
}
