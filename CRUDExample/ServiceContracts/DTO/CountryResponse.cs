using Entities;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class that is used as return type for most of CountriesService methods
    /// </summary>
    public class CountryResponse
    {
        // Properties
        public Guid CountryID { get; set; }

        public string? CountryName { get; set; }

        // Methods
        public override bool Equals(object? obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || (obj.GetType() != typeof(CountryResponse)))
            {
                return false;
            }
            else
            {
                CountryResponse countryResponse = (CountryResponse)obj;
                return (CountryID == countryResponse.CountryID) && (CountryName == countryResponse.CountryName);
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class CountryExtensions
    {
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse()
            {
                CountryID = country.CountryID,
                CountryName = country.CountryName
            };
        }
    }
}
