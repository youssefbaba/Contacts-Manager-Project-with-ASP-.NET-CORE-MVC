using ContactsManager.Core.Domain.Entities;
using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.DTO;
using ContactsManager.Core.Exceptions;
using ContactsManager.Core.Helpers;
using ContactsManager.Core.ServiceContracts;

namespace ContactsManager.Core.Services
{
    public class CountriesAdderService : ICountriesAdderService
    {
        // Fields
        private readonly ICountriesRepository _countriesRepository;

        // Constructors
        public CountriesAdderService(ICountriesRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;
        }

        // Method
        public async Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest)
        {
            // countryAddRequest parameter can't be null
            if (countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }

            // Model Validations
            ValidationHelper.ModelValidation(countryAddRequest);

            // Validation: CountryName value can't be duplicate
            if (await _countriesRepository.GetCountryByCountryName(countryAddRequest.CountryName!) != null)
            {
                throw new DuplicateCountryNameException("CountryName already exists");
                //throw new ArgumentException("CountryName already exists");
            }

            // Convert object from CountryAddRequest to Country type
            Country country = countryAddRequest.ToCountry();

            // Generate new CountryID
            country.CountryID = Guid.NewGuid();

            // Add country object into _countries
            await _countriesRepository.AddCountry(country);

            return country.ToCountryResponse();
        }
    }
}

