using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Assignement_5.Models
{
    public class Product
    {
        [Display(Name = "Product Code")]
        [Required(ErrorMessage = "{0} can't be blank.")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} should be between {1} and {2}")]
        public int? ProductCode { get; set; }

        [Required(ErrorMessage = "{0} can't be blank.")]
        [Range(1, double.MaxValue, ErrorMessage = "{0} should be between {1} and {2}")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "{0} can't be blank.")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} should be between {1} and {2}")]
        public int? Quantity { get; set; }
    }
}
