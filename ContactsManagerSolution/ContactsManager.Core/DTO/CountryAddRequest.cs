using ContactsManager.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Core.DTO
{
    /// <summary>
    /// DTO class for adding a new country
    /// </summary>
    public class CountryAddRequest
    {
        // Properties
        [Required(ErrorMessage = "Country Name can't be blank")]
        public string? CountryName { get; set; }

        // Methods
        public Country ToCountry()
        {
            return new Country()
            {
                CountryName = CountryName
            };
        }
    }
}
