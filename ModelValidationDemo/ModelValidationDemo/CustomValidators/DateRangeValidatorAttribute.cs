using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ModelValidationDemo.CustomValidators
{
    public class DateRangeValidatorAttribute : ValidationAttribute
    {
        // Properties
        public string? OtherPropertyName { get; set; }
        public string DefaultErrorMessage { get; set; } = "'{0}' should be older than or equal to '{1}";


        // Constructors
        public DateRangeValidatorAttribute()
        {

        }
        public DateRangeValidatorAttribute(string otherPropertyName)
        {
            OtherPropertyName = otherPropertyName;
        }

        // Methods
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                // gets the value for toDate
                DateTime toDate = Convert.ToDateTime(value);

                // gets the value for fromDate
                if (OtherPropertyName != null)
                {
                    PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(OtherPropertyName);
                    DateTime fromDate = Convert.ToDateTime(otherProperty?.GetValue(validationContext.ObjectInstance));
                    int result = DateTime.Compare(fromDate, toDate);
                    if (result > 0)
                    {
                        string errorMessage = string.Format(format: ErrorMessage ?? DefaultErrorMessage, arg0: OtherPropertyName, arg1: validationContext.MemberName);
                        return new ValidationResult(errorMessage, new string[] { OtherPropertyName, validationContext.MemberName! });
                    }
                        return ValidationResult.Success;
                }
                return null;
            }
            return null;  // No Validation
        }
    }
}
