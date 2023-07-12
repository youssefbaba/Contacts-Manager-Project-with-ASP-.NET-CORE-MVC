using Assignement_5.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Assignement_5.CustomValidations
{
    public class InvoicePriceMatchTotalCostValidatorAttribute : ValidationAttribute
    {
        // Properties
        public string DefaultErrorMessage { get; set; } = "InvoicePrice doesn't match with the total cost of the specified products in the order.";

        // Constructors
        public InvoicePriceMatchTotalCostValidatorAttribute()
        {
        }

        // Methods
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                double invoicePrice = Convert.ToDouble(value);
                PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(nameof(Order.Products));
                if (otherProperty != null)
                {
                    List<Product> products = (List<Product>)otherProperty.GetValue(validationContext.ObjectInstance)!;
                    if (products.Count < 1)
                    {
                        return new ValidationResult("No products found to validate invoice price.", new string[] {nameof(validationContext.MemberName)});
                    }
                    double? totalCost = 0;
                    foreach (var product in products)
                    {
                        totalCost += (product.Price * product.Quantity);
                    }
                    if (totalCost != invoicePrice)
                    {
                        string errorMessage = ErrorMessage ?? DefaultErrorMessage;
                        return new ValidationResult(errorMessage, new string[] { nameof(validationContext.MemberName) });
                    }
                    return ValidationResult.Success;
                }
                return null;
            }
            return null;
        }
    }
}
