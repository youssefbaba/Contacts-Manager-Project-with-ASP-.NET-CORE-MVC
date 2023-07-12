
namespace ServiceContracts.FinnhubService
{
    public interface IFinnhubCompanyProfileService
    {
        Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);
    }
}
