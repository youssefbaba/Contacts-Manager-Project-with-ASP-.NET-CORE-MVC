using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelValidationDemo.Models;

namespace ModelValidationDemo.CustomModelBinders
{
    public class PersonModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Person person = new Person();

            // FirstName and LastName
            if (bindingContext.ValueProvider.GetValue("FirstName").Length > 0)
            {
                person.PersonName = bindingContext.ValueProvider.GetValue("FirstName").FirstValue;

                if (bindingContext.ValueProvider.GetValue("LastName").Length > 0)
                {
                    person.PersonName += " " + bindingContext.ValueProvider.GetValue("LastName").FirstValue;
                }
            }

            // Email
            if (bindingContext.ValueProvider.GetValue(nameof(Person.Email)).Length > 0)
            {
                person.Email = bindingContext.ValueProvider.GetValue(nameof(Person.Email)).FirstValue;
            }

            // Phone
            if (bindingContext.ValueProvider.GetValue(nameof(Person.Phone)).Length > 0)
            {
                person.Phone = bindingContext.ValueProvider.GetValue(nameof(Person.Phone)).FirstValue;
            }

            // Password
            if (bindingContext.ValueProvider.GetValue(nameof(Person.Password)).Length > 0)
            {
                person.Password = bindingContext.ValueProvider.GetValue(nameof(Person.Password)).FirstValue;
            }

            // ConfirmPassword
            if (bindingContext.ValueProvider.GetValue(nameof(Person.ConfirmPassword)).Length > 0)
            {
                person.ConfirmPassword = bindingContext.ValueProvider.GetValue(nameof(Person.ConfirmPassword)).FirstValue;
            }

            // Price
            if (bindingContext.ValueProvider.GetValue(nameof(Person.Price)).Length > 0)
            {
                person.Price = Convert.ToDouble(bindingContext.ValueProvider.GetValue(nameof(Person.Price)).FirstValue);
            }

            // DateOfBirth
            if (bindingContext.ValueProvider.GetValue(nameof(Person.DateOfBirth)).Length > 0)
            {
                person.DateOfBirth = Convert.ToDateTime(bindingContext.ValueProvider.GetValue(nameof(Person.DateOfBirth)).FirstValue);
            }

            // Age
            if (bindingContext.ValueProvider.GetValue(nameof(Person.Age)).Length > 0)
            {
                person.Age = Convert.ToInt32(bindingContext.ValueProvider.GetValue(nameof(Person.Age)).FirstValue);
            }

            bindingContext.Result = ModelBindingResult.Success(person);
            return Task.CompletedTask;
        }
    }
}
