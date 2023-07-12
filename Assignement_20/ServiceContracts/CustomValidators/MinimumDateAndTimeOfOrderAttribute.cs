using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.CustomValidators
{
    public class MinimumDateAndTimeOfOrderAttribute : ValidationAttribute
    {
        // Property
        public string MinimumDateAndTimeOfOrder { get; set; } = "2000-01-01";
        public string DefaultErrorMessage { get; set; } = "Date And Time Of Order should not be older than Jan 01, 2000";

        // Constructor  
        public MinimumDateAndTimeOfOrderAttribute()
        {
        }
        public MinimumDateAndTimeOfOrderAttribute(string minimumDateAndTimeOfOrder)
        {
            if (DateTime.TryParse(minimumDateAndTimeOfOrder, out DateTime result))
            {
                MinimumDateAndTimeOfOrder = minimumDateAndTimeOfOrder;
            }

        }

        // Methods
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime date = (DateTime)value;
                DateTime dateToCompare = Convert.ToDateTime(MinimumDateAndTimeOfOrder);
                if (date < dateToCompare)
                {
                    string errorMessage = (ErrorMessage == null) ? DefaultErrorMessage : ErrorMessage;
                    return new ValidationResult(errorMessage);
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            return null;
        }
    }
}
