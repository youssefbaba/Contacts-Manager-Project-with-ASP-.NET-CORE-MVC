using Assignement_5.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace Assignement_5.Models
{
    public class Order
    {
        public int? OrderNo { get; set; }

        [Display(Name = "Order Date")]
        [Required(ErrorMessage = "{0} can't be blank.")]
        [MinimumOrderDateValidator(MinimunDate = "2005-02-14")]
        public DateTime? OrderDate { get; set; }

        [Display(Name = "Invoice Price")]
        [Required(ErrorMessage = "{0} can't be blank.")]
        [InvoicePriceMatchTotalCostValidator]
        [Range(1, double.MaxValue, ErrorMessage = "{0} should be between {1} and {2}")]
        public double? InvoicePrice { get; set; }

        [MinimumNumberOfProductValidator]
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
