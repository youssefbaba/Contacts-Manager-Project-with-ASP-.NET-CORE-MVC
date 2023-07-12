using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.Helpers
{
    public class ValidationHelper
    {
        public static void ModelValidation(object obj)
        {
            // Model Validations
            ValidationContext validationContext = new ValidationContext(obj);

            List<ValidationResult> validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            if (!isValid)
            {
                throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
            }
        }
    }
}
