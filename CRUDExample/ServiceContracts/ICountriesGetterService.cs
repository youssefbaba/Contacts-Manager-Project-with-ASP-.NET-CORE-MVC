using ServiceContracts.DTO;

namespace ServiceContracts
{
    public interface ICountriesGetterService
    {
        Task<List<CountryResponse>> GetAllCountries();

        Task<CountryResponse?> GetCountryByCountryID(Guid? countryID);
    }
}
