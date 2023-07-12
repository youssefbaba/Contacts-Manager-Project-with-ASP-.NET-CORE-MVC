using ContactsManager.Core.Domain.Entities;
using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace ContactsManager.Infrastructure.Repositories
{
    public class PersonsRepository : IPersonsRepository
    {

        // Fields
        private readonly ApplicationDbContext _db;
        private readonly ILogger<PersonsRepository> _logger;


        // Constructors
        public PersonsRepository(ApplicationDbContext db,
            ILogger<PersonsRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        // Methods

        public async Task<Person> AddPerson(Person person)
        {
            _db.Persons.Add(person);
            await _db.SaveChangesAsync();
            return person;
        }

        public async Task<bool> DeletePerson(Guid personID)
        {
            Person? person = await _db.Persons.FirstOrDefaultAsync(temp => temp.PersonID == personID);
            if (person != null)
            {
                _db.Persons.Remove(person);
            }
            int rowsDeleted = await _db.SaveChangesAsync();
            return (rowsDeleted > 0) ? true : false;
        }

        public async Task<List<Person>> GetAllPersons()
        {
            // For tracking the execution flow 
            _logger.LogInformation("{MethodName} method of {RepositoryName}", nameof(GetAllPersons), nameof(PersonsRepository));

            return await _db.Persons.Include(nameof(Person.Country)).ToListAsync();
        }

        public async Task<List<Person>> GetFilteredPersons(Expression<Func<Person, bool>> predicate)
        {
            // For tracking the execution flow 
            _logger.LogInformation("{MethodName} method of {RepositoryName}", nameof(GetFilteredPersons), nameof(PersonsRepository));

            return  await _db.Persons.Include(nameof(Person.Country))
                .Where(predicate).ToListAsync();
        }

        public async Task<Person?> GetPersonByPersonID(Guid personID)
        {
            return await _db.Persons.Include(nameof(Person.Country))
                .FirstOrDefaultAsync(temp => temp.PersonID == personID);
        }

        public async Task<Person?> GetPersonByPersonName(string personName)
        {
            return await _db.Persons.Include(nameof(Person.Country)).FirstOrDefaultAsync(temp => temp.PersonName == personName);
        }

        public async Task<Person> UpdatePerson(Person person)
        {
            Person? matchingPerson = await _db.Persons.Include(nameof(Person.Country)).FirstOrDefaultAsync(temp => temp.PersonID == person.PersonID);
            if (matchingPerson == null)
            {
                return person;
            }
            matchingPerson.PersonName = person.PersonName;
            matchingPerson.Email = person.Email;
            matchingPerson.DateOfBirth = person.DateOfBirth;
            matchingPerson.Gender = person.Gender;
            matchingPerson.CountryID = person.CountryID;
            matchingPerson.Address = person.Address;
            matchingPerson.ReceiveNewsLetters = person.ReceiveNewsLetters;
            await _db.SaveChangesAsync();
            return matchingPerson;
        }
    }
}
