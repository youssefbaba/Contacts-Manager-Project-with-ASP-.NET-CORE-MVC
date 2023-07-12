using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelValidationDemo.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace ModelValidationDemo.Models
{
    public class Person : IValidatableObject
    {
        // Properties

        //[Required()]
        [Display(Name = "Person Name")]
        [Required(ErrorMessage = "{0} can't be empty or null")]
        [StringLength(maximumLength: 40, MinimumLength = 3, ErrorMessage = "{0} should be between {2} and {1} characters long")]
        [RegularExpression("^[A-Za-z ]*$", ErrorMessage = "{0} should contain only alphabets and space")]
        public string? PersonName { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
        [EmailAddress(ErrorMessage = "{0} should be a proper email address")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "{0} should be a valid phone number")]
        //[ValidateNever]  // Excludes Phone property from model validation
        public string? Phone { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
        public string? Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "{0} can't be blank")]
        [Compare("Password", ErrorMessage = "{0} and {1} should be the same")]
        public string? ConfirmPassword { get; set; }

        [Range(minimum: 0, maximum: 9999.99, ErrorMessage = "{0} should be between {1}$ and {2}$")]
        public double? Price { get; set; }

        [Display(Name = "Date Of Birth")]
        //[MinimumYearValidator(MinimumYear = 2005, ErrorMessage = "Date Of Birth should not be newer than Jan 01, 2005")]
        //[MinimumYearValidator(MinimumYear = 2005, ErrorMessage = "{0} should not be newer than Jan 01, {1}")]
        [MinimumYearValidator(MinimumYear = 2005)]
        //[BindNever]  // To escape model binding for this property
        public DateTime? DateOfBirth { get; set; }

        public DateTime? FromDate { get; set; }

        [DateRangeValidator(OtherPropertyName = "FromDate", ErrorMessage = "'{0}' should be older than or equal to '{1}'")]
        public DateTime? ToDate { get; set; }

        public int? Age { get; set; }

        public List<string?> Tags { get; set; } = new List<string?>();

        // Methods
        public override string ToString()
        {
            return $"Person Name: {PersonName}\n" +
                $"Email: {Email}\n" +
                $"Phone: {Phone}\n" +
                $"Password: {Password}\n" +
                $"Confirm Password: {ConfirmPassword}\n" +
                $"Price: {Price}\n";
        }

        // Validate method executes automatically only when all models validation should be clear (No error message)
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!DateOfBirth.HasValue && !Age.HasValue)
            {
              yield  return new ValidationResult("Either of Date of Birth or Age must be supplied", new[] {nameof(DateOfBirth), nameof(Age)});
            }
            /*
            if (true)
            {
               yield return new ValidationResult("Some Error Message");
            }
            */
        }
    }
}
