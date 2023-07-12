using Microsoft.AspNetCore.Http;

namespace ContactsManager.Core.ServiceContracts
{
    public interface ICountriesUploadFromExcelService
    {
        Task<int> UploadCountriesFromExcelFile(IFormFile formFile);
    }
}
