using Entities;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;

namespace ServiceLayer
{
    public class CountriesGetterService : ICountriesGetterService
    {
        // Fields
        private readonly ICountriesRepository _countriesRepository;

        // Constructors
        public CountriesGetterService(ICountriesRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;
        }

        // Methods
        public async Task<List<CountryResponse>> GetAllCountries()
        {
            return (await _countriesRepository.GetAllCountries()).Select(country => country.ToCountryResponse()).ToList();  // because we cannot call your own methods or functions as part of linq to entities expression
        }

        public async Task<CountryResponse?> GetCountryByCountryID(Guid? countryID)
        {
            if (countryID == null)
            {
                return null;
            }
            Country? country = await _countriesRepository.GetCountryByCountryID(countryID.Value);
            if (country == null)
            {
                return null;
            }
            return country.ToCountryResponse();
        }
    }
}

