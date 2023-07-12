using Assignement_5.Models;
using System.ComponentModel.DataAnnotations;

namespace Assignement_5.CustomValidations
{
    public class MinimumNumberOfProductValidatorAttribute : ValidationAttribute
    {
        // Properties
        public string DefaultErrorMessage { get; set; } = "Order must contain at least one product.";

        // Constructors
        public MinimumNumberOfProductValidatorAttribute()
        {
        }

        // Methods
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                List<Product> products = (List<Product>)value;
                if (products.Count < 1)
                {
                    string errorMessage = ErrorMessage ?? DefaultErrorMessage;
                    return new ValidationResult(errorMessage, new string[] { nameof(validationContext.MemberName) });
                }
            }
            return null;
        }
    }
}
