using System.ComponentModel.DataAnnotations;

namespace Assignement_18.Models
{
    public class StockTrade
    {
        public string? StockSymbol { get; set; }

        public string? StockName { get; set; }

        public double Price { get; set; }

        [Required(ErrorMessage = ("Quantity can't be blank"))]
        [Range(1, 100000, ErrorMessage = "{0} should be between {1} and {2}")]
        public uint Quantity { get; set; }
    }
}
