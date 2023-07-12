using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceLayer;
using AutoFixture;
using FluentAssertions;
using Moq;
using RepositoryContracts;

namespace CRUDTests
{
    public class CountriesServiceTest
    {
        // Fields
        private readonly ICountriesGetterService _countriesGetterService;
        private readonly ICountriesAdderService _countriesAdderService;

        private readonly Mock<ICountriesRepository> _countriesRepositoryMock;
        private readonly ICountriesRepository _countriesRepository;

        private readonly IFixture _fixture;

        // Constructors
        public CountriesServiceTest()
        {
            // Create an instance of AutoFixture
            _fixture = new Fixture();

            // Mock the Repository
            _countriesRepositoryMock = new Mock<ICountriesRepository>();
            _countriesRepository = _countriesRepositoryMock.Object;

            // Create service instance with mocked DbContext
            _countriesGetterService = new CountriesGetterService(_countriesRepository);
            _countriesAdderService = new CountriesAdderService(_countriesRepository);
        }

        // Methods

        #region AddCountry
        // When CountryAddRequest is null, it should throw ArgumentNullException
        [Fact]
        public async Task AddCountry_CountryIsNull_ToBeArgumentNullException()
        {
            // Arrange
            CountryAddRequest? countryAddRequest = null;

            // Act
            Func<Task> action = async () =>
            {
                await _countriesAdderService.AddCountry(countryAddRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        // When the CountryName is null, it should throw ArgumentException
        [Fact]
        public async Task AddCountry_CountryNameIsNull_ToBeArgumentException()
        {
            // Arrange
            CountryAddRequest? countryAddRequest =
                _fixture.Build<CountryAddRequest>()
                .With(temp => temp.CountryName, null as string)
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _countriesAdderService.AddCountry(countryAddRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When the CoutryName is duplicate, it should throw ArgumentException
        [Fact]
        public async Task AddCountry_CountryNameIsDuplicate_ToBeArgumentException()
        {
            // Arrange
            CountryAddRequest? countryAddRequest = _fixture.Create<CountryAddRequest>();

            Country country = countryAddRequest.ToCountry();

            _countriesRepositoryMock.Setup(temp => temp.GetCountryByCountryName(It.IsAny<string>()))
            .ReturnsAsync(country);

            // Act
            Func<Task> action = async () =>
            {
                await _countriesAdderService.AddCountry(countryAddRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When you supply proper CountryName, it should insert the same into the existing list of countries
        [Fact]
        public async Task AddCountry_ProperCountry_ToBeSuccessful()
        {
            // Arrange
            CountryAddRequest? countryAddRequest = _fixture.Create<CountryAddRequest>();

            Country country = countryAddRequest.ToCountry();

            CountryResponse expectedCountryResponse = country.ToCountryResponse();

            _countriesRepositoryMock.Setup(temp => temp.GetCountryByCountryName(It.IsAny<string>()))
                .ReturnsAsync(null as Country);

            _countriesRepositoryMock.Setup(temp => temp.AddCountry(It.IsAny<Country>()))
                .ReturnsAsync(country);

            // Act
            CountryResponse actualCountryResponse = await _countriesAdderService.AddCountry(countryAddRequest);
            expectedCountryResponse.CountryID = actualCountryResponse.CountryID;

            // Assert

            actualCountryResponse.CountryID.Should().NotBe(Guid.Empty);
            actualCountryResponse.Should().Be(expectedCountryResponse);
        }
        #endregion

        #region GetAllCountries
        // The list of countries should be empty by default (before adding any countries)
        [Fact]
        public async Task GetAllCountries_ListIsEmpty_ToBeEmptyList()
        {
            // Arrange
            _countriesRepositoryMock.Setup(temp => temp.GetAllCountries())
                .ReturnsAsync(new List<Country>());

            // Act 
            List<CountryResponse> actualCountryResponseList = await _countriesGetterService.GetAllCountries();

            // Assert
            actualCountryResponseList.Should().BeEmpty();
        }

        // The list of countries countains some countries and it will be return the list of the same countries
        [Fact]
        public async Task GetAllCountries_WithFewCountries_ToBeSuccessful()
        {
            // Arrange
            Country countryOne = _fixture.Build<Country>()
                .With(temp => temp.Persons, null as ICollection<Person>)
                .Create();

            Country countryTwo = _fixture.Build<Country>()
                .With(temp => temp.Persons, null as ICollection<Person>)
                .Create();

            List<Country> countries = new List<Country>() { countryOne, countryTwo };

            List<CountryResponse> expectedCountryResponseList = countries.Select(temp => temp.ToCountryResponse()).ToList();

            _countriesRepositoryMock.Setup(temp => temp.GetAllCountries())
                .ReturnsAsync(countries);

            // Act 
            List<CountryResponse> actualCountryResponseList = await _countriesGetterService.GetAllCountries();

            // Assert
            actualCountryResponseList.Should().BeEquivalentTo(expectedCountryResponseList);
        }

        #endregion

        #region GetCountryByCountryID
        // When CountryID parameter is null, it should return null
        [Fact]
        public async Task GetCountryByCountryID_CountryIDIsNull_ToBeNull()
        {
            // Arrange 
            Guid? countryID = null;

            // Act 
            CountryResponse? actualCountryResponse = await _countriesGetterService.GetCountryByCountryID(countryID);

            // Assert
            actualCountryResponse.Should().BeNull();
        }

        // When CountryID is not valid, it should return null
        [Fact]
        public async Task GetCountryByCountryID_CountryIDIsNotValid_ToBeNull()
        {
            // Arrange
            CountryAddRequest countryAddRequest = _fixture.Create<CountryAddRequest>();

            _countriesRepositoryMock.Setup(temp => temp.GetCountryByCountryID(It.IsAny<Guid>()))
                .ReturnsAsync(null as Country); 

            // Act
            CountryResponse? actualCountryResponse = await _countriesGetterService.GetCountryByCountryID(Guid.NewGuid());

            // Assert
            actualCountryResponse.Should().BeNull();
        }

        // When CountryID is valid, it should return the matching CountryResponse object
        [Fact]
        public async Task GetCountryByCountryID_CountryIDIsValid_ToBeSuccessful()
        {
            // Arrange
            Country country = _fixture.Build<Country>()
                .With(temp => temp.Persons, null as ICollection<Person>)
                .Create();

            _countriesRepositoryMock.Setup(temp => temp.GetCountryByCountryID(It.IsAny<Guid>()))
                .ReturnsAsync(country);

            CountryResponse expectedCountryResponse = country.ToCountryResponse();

            // Act
            CountryResponse? actualCountryResponse = await _countriesGetterService.GetCountryByCountryID(country.CountryID);

            // Assert
            actualCountryResponse.Should().Be(expectedCountryResponse);
        }

        #endregion
    }
}
