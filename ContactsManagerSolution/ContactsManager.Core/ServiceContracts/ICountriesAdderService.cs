using ContactsManager.Core.DTO;

namespace ContactsManager.Core.ServiceContracts
{
    public interface ICountriesAdderService
    {
        Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest);
    }
}
