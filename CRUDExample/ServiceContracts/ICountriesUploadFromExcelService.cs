using Microsoft.AspNetCore.Http;

namespace ServiceContracts
{
    public interface ICountriesUploadFromExcelService
    {
        Task<int> UploadCountriesFromExcelFile(IFormFile formFile);
    }
}
