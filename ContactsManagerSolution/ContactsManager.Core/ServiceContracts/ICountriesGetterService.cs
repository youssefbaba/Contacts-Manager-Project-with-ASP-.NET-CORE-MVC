using ContactsManager.Core.DTO;

namespace ContactsManager.Core.ServiceContracts
{
    public interface ICountriesGetterService
    {
        Task<List<CountryResponse>> GetAllCountries();

        Task<CountryResponse?> GetCountryByCountryID(Guid? countryID);
    }
}
