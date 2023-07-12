using ContactsManager.Core.Domain.Entities;
using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ContactsManager.Infrastructure.Repositories
{
    public class CountriesRepository : ICountriesRepository
    {
        // Fiels
        private readonly ApplicationDbContext _db;

        // Constructors
        public CountriesRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        // Methods
        public async Task<Country> AddCountry(Country country)
        {
            _db.Countries.Add(country);
            await _db.SaveChangesAsync();
            return country;
        }

        public async Task<List<Country>> GetAllCountries()
        {
            return await _db.Countries.ToListAsync();
        }

        public async Task<Country?> GetCountryByCountryID(Guid countryID)
        {
            return await _db.Countries.FirstOrDefaultAsync(temp => temp.CountryID == countryID);
        }

        public async Task<Country?> GetCountryByCountryName(string countryName)
        {
            return await _db.Countries.FirstOrDefaultAsync(temp => temp.CountryName == countryName);
        }
    }
}