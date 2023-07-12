using Microsoft.AspNetCore.Http;
using ServiceContracts.DTO;

namespace ServiceContracts
{
    public interface ICountriesAdderService
    {
        Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest);
    }
}
