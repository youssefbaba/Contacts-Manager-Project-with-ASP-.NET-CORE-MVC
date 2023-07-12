using AgeCalculator;
using ContactsManager.Core.Domain.Entities;
using ContactsManager.Core.Enums;

namespace ContactsManager.Core.DTO
{
    public class PersonResponse
    {
        // Properties
        public Guid PersonID { get; set; }

        public string? PersonName { get; set; }

        public string? Email { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public Guid? CountryID { get; set; }

        public string? Country { get; set; }

        public string? Address { get; set; }

        public bool ReceiveNewsLetters { get; set; }

        public int? Age { get; set; }

        // Methods
        public override bool Equals(object? obj)
        {
            if ((obj == null) || obj.GetType() != typeof(PersonResponse))
            {
                return false;
            }
            PersonResponse personResponse = (PersonResponse)obj;
            return (PersonID == personResponse.PersonID) &&
                (PersonName == personResponse.PersonName) &&
                (Email == personResponse.Email) &&
                (DateOfBirth == personResponse.DateOfBirth) &&
                (Gender == personResponse.Gender) &&
                (CountryID == personResponse.CountryID) &&
                (Country == personResponse.Country) &&
                (Address == personResponse.Address) &&
                (ReceiveNewsLetters == personResponse.ReceiveNewsLetters) &&
                (Age == personResponse.Age);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"Person ID: {PersonID}, Person Name: {PersonName}, Email: {Email}, Date Of Birth: {DateOfBirth?.ToString("yyyy/MM/dd")}," +
                $" Gender: {Gender}, Country ID: {CountryID}, Country: {Country}, Address: {Address}, Receive NewsLetters: {ReceiveNewsLetters}, Age: {Age}";
        }

        public PersonUpdateRequest ToPersonUpdateRequest()
        {
            return new PersonUpdateRequest()
            {
                PersonID = PersonID,
                PersonName = PersonName,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Gender = Enum.TryParse(value: Gender, ignoreCase: true, result: out GenderOptions gender) ? gender : null,
                CountryID = CountryID,
                Address = Address,
                ReceiveNewsLetters = ReceiveNewsLetters
            };
        }
    }

    public static class PersonExtensions
    {
        public static PersonResponse ToPersonResponse(this Person person)
        {
            return new PersonResponse()
            {
                PersonID = person.PersonID,
                PersonName = person.PersonName,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                Gender = person.Gender,
                CountryID = person.CountryID,
                Country = person.Country?.CountryName,
                Address = person.Address,
                ReceiveNewsLetters = person.ReceiveNewsLetters,
                Age = (person.DateOfBirth == null || (person.DateOfBirth.Value > DateTime.Today) ? null : new Age(person.DateOfBirth.Value, DateTime.Today).Years)
            };
        }
    }
}
