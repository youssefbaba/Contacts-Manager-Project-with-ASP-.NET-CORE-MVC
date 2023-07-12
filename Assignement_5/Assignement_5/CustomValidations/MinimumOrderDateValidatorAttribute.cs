using Assignement_5.Models;
using System.ComponentModel.DataAnnotations;

namespace Assignement_5.CustomValidations
{
    public class MinimumOrderDateValidatorAttribute : ValidationAttribute
    {
        // Properties
        public string DefaultErrorMessage { get; set; } = "Order date should be greater than or equal to {0}";
        public string MinimunDate { get; set; } = "2000-01-01";

        // Constructor
        public MinimumOrderDateValidatorAttribute()
        {
        }
        public MinimumOrderDateValidatorAttribute(string minimumDate)
        {
            MinimunDate = minimumDate;
        }

        // Methods
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime orderDate = Convert.ToDateTime(value);
                DateTime minimumDate = Convert.ToDateTime(MinimunDate);
                int result = DateTime.Compare(orderDate, minimumDate);

                if (result <= 0)
                {
                    string errorMessage = string.Format(ErrorMessage ?? DefaultErrorMessage, arg0: string.Format("{0:yyyy-MM-dd}", minimumDate));
                    return new ValidationResult(errorMessage, new string[] {nameof(Order.OrderDate)});
                }
                return ValidationResult.Success;
            }
            return null;
        }
    }
}
