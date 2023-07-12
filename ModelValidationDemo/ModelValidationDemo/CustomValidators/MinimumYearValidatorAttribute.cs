using System.ComponentModel.DataAnnotations;

namespace ModelValidationDemo.CustomValidators
{
    public class MinimumYearValidatorAttribute : ValidationAttribute
    {
        // Properties
        public int MinimumYear { get; set; } = 2000;

        public string DefaultErrorMessage { get; set; } = "Year should not be less than {0}";

        // Constructors
        public MinimumYearValidatorAttribute()
        {
        }
        public MinimumYearValidatorAttribute(int minimumYear)
        {
            MinimumYear = minimumYear;
        }

        // Methods
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime date = Convert.ToDateTime(value);
                if (date.Year > MinimumYear)
                {
                    string errorMessage = string.Empty;
                    if (ErrorMessage != null)
                    {
                        errorMessage = string.Format(format: ErrorMessage, arg0: validationContext.DisplayName, arg1: MinimumYear);
                    }
                    else
                    {
                        errorMessage = string.Format(format: DefaultErrorMessage, arg0: MinimumYear);
                    }
                    return new ValidationResult(errorMessage);
                }
                return ValidationResult.Success;
            }
            return null;
        }
    }
}
