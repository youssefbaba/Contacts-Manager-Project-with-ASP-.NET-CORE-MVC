using Xunit.Abstractions;
using AutoFixture;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Serilog;
using ContactsManager.Core.ServiceContracts;
using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.Services;
using ContactsManager.Core.DTO;
using ContactsManager.Core.Domain.Entities;
using ContactsManager.Core.Enums;

namespace ContactsManager.ServiceTests
{
    public class PersonsServiceTest
    {
        // Fields
        private readonly IPersonsGetterService _personsGetterService;
        private readonly IPersonsAdderService _personsAdderService;
        private readonly IPersonsSorterService _personsSorterService;
        private readonly IPersonsUpdaterService _personsUpdaterService;
        private readonly IPersonsDeleterService _personsDeleterService;

        private readonly Mock<IPersonsRepository> _personsRepositoryMock;
        private readonly IPersonsRepository _personsRepository;

        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IFixture _fixture;

        private readonly Mock<ILogger<PersonsGetterService>> _loggerGetterMock;
        private readonly ILogger<PersonsGetterService> _loggerGetter;

        private readonly Mock<ILogger<PersonsSorterService>> _loggerSorterMock;
        private readonly ILogger<PersonsSorterService> _loggerSorter;


        private readonly Mock<IDiagnosticContext> _diagnosticContextMock;
        private readonly IDiagnosticContext _diagnosticContext;

        // Constructors
        public PersonsServiceTest(ITestOutputHelper testOutputHelper)
        {
            // Create instance of AutoFixture
            _fixture = new Fixture();

            _loggerGetterMock = new Mock<ILogger<PersonsGetterService>>();
            _loggerGetter = _loggerGetterMock.Object;

            _loggerSorterMock = new Mock<ILogger<PersonsSorterService>>();
            _loggerSorter = _loggerSorterMock.Object;

            _diagnosticContextMock = new Mock<IDiagnosticContext>();
            _diagnosticContext = _diagnosticContextMock.Object;

            // Mock the Repository
            _personsRepositoryMock = new Mock<IPersonsRepository>();
            _personsRepository = _personsRepositoryMock.Object;

            _personsGetterService = new PersonsGetterService(_personsRepository, _loggerGetter, _diagnosticContext);
            _personsAdderService = new PersonsAdderService(_personsRepository);
            _personsSorterService = new PersonsSorterService(_personsRepository, _loggerSorter, _diagnosticContext);
            _personsUpdaterService = new PersonsUpdaterService(_personsRepository);
            _personsDeleterService = new PersonsDeleterService(_personsRepository);

            _testOutputHelper = testOutputHelper;
        }

        // Methods

        #region AddPerson
        // When we supply null value as PersonAddRequest, it should throw ArgumentNullException
        [Fact]
        public async Task AddPerson_PersonIsNull_ToBeArgumentNullException()
        {
            // Arrange 
            PersonAddRequest? personAddRequest = null;

            // Act
            Func<Task> action = async () =>
            {
                await _personsAdderService.AddPerson(personAddRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        // When we supply null value as PersonName, it should throw ArgumentException
        [Fact]
        public async Task AddPerson_PersonNameIsNull_ToBeArgumentException()
        {
            // Arrange
            PersonAddRequest personAddRequest =
                _fixture.Build<PersonAddRequest>()
                .With(temp => temp.PersonName, null as string)
                .With(temp => temp.Email, "Jim@gmail.com")
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _personsAdderService.AddPerson(personAddRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When we supply null value as Email, it should throw ArgumentException
        [Fact]
        public async Task AddPerson_EmailIsNull_ToBeArgumentException()
        {
            // Arrange
            PersonAddRequest personAddRequest =
                _fixture.Build<PersonAddRequest>()
                .With(temp => temp.Email, null as string)
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _personsAdderService.AddPerson(personAddRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When we supply invalid email, it should throw ArgumentException
        [Fact]
        public async Task AddPerson_EmailIsInvalid_ToBeArgumentException()
        {
            // Arrange
            PersonAddRequest personAddRequest =
                _fixture.Build<PersonAddRequest>()
                .With(temp => temp.Email, "Johngmail.com")
                .Create();

            // Act
            Func<Task> action = async () =>
            {
                await _personsAdderService.AddPerson(personAddRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();

        }

        // When we supply proper PersonAddRequest, it should add this object into persons list
        // which includes with the newly generated PersonID
        [Fact]
        public async Task AddPerson_FullPersonDetails_ToBeSuccessful()
        {
            // Arrange
            PersonAddRequest personAddRequest =
                _fixture.Build<PersonAddRequest>()
                .With(temp => temp.Email, "David@gmail.com")
                .Create();

            Person person = personAddRequest.ToPerson();

            PersonResponse expectedPersonResponse = person.ToPersonResponse();

            // If we supply any argument value to the AddPerson method
            // it should return the same return value
            _personsRepositoryMock.Setup(temp => temp.AddPerson(It.IsAny<Person>()))
              .ReturnsAsync(person);

            // Act
            PersonResponse? personResponse = await _personsAdderService.AddPerson(personAddRequest);
            expectedPersonResponse.PersonID = personResponse.PersonID;

            // Assert
            personResponse.PersonID.Should().NotBe(Guid.Empty);
            personResponse.Should().Be(expectedPersonResponse);
        }

        #endregion

        #region GetPersonByPersonID

        // When we supply personID as null value, it should return null as PersonResponse
        [Fact]
        public async Task GetPersonByPersonID_PersonIDIsNull_ToBeNull()
        {
            // Arrange
            Guid? personID = null;

            // Act
            PersonResponse? actualPersonResponse = await _personsGetterService.GetPersonByPersonID(personID);

            // Assert
            actualPersonResponse.Should().BeNull();
        }

        // When we supply invalid PersonID, it should return null
        [Fact]
        public async Task GetPersonByPersonID_PersonIDIsInvalid_ToBeNull()
        {
            // Arrange
            _personsRepositoryMock.Setup(temp => temp.GetPersonByPersonID(It.IsAny<Guid>()))
                .ReturnsAsync(null as Person);

            // Act
            PersonResponse? actualPersonResponse = await _personsGetterService.GetPersonByPersonID(Guid.NewGuid());

            // Assert
            actualPersonResponse.Should().BeNull();
        }

        // When we supply a valid PersonID, it should return the valid person details as PersonResponse object
        [Fact]
        public async Task GetPersonByPersonID_PersonIDIsValid_ToBeSuccessful()
        {
            // Arrange
            Person person = _fixture.Build<Person>()
                .With(temp => temp.Country, null as Country)
                .Create();

            PersonResponse expectedPersonResponse = person.ToPersonResponse();

            _personsRepositoryMock.Setup(temp => temp.GetPersonByPersonID(It.IsAny<Guid>()))
                .ReturnsAsync(person);

            // Act
            PersonResponse? actualPersonResponse = await _personsGetterService.GetPersonByPersonID(person.PersonID);

            // Assert
            actualPersonResponse.Should().Be(expectedPersonResponse);

        }
        #endregion

        #region GetAllPersons
        // when persons list is empty, it should return empty list also
        [Fact]
        public async Task GetAllPersons_EmptyList_ToBeEmptyList()
        {
            // Arrange
            _personsRepositoryMock.Setup(temp => temp.GetAllPersons())
                .ReturnsAsync(new List<Person>());

            // Act 
            List<PersonResponse> actualPersonResponses = await _personsGetterService.GetAllPersons();

            // Assert
            actualPersonResponses.Should().BeEmpty();

        }

        // if persons list contains some elements, it should return PersonResponse list with the same elements
        [Fact]
        public async Task GetAllPersons_WithFewPersons_ToBeSeccessfu()
        {
            // Arrange
            Person personOne = _fixture.Build<Person>()
                .With(temp => temp.Country, null as Country)
                .Create();
            Person personTwo = _fixture.Build<Person>()
                .With(temp => temp.Country, null as Country)
                .Create();

            List<Person> persons = new List<Person>()
            {
                personOne, personTwo
            };

            List<PersonResponse> expectedPersonResponseList = persons.Select(temp => temp.ToPersonResponse()).ToList();

            _personsRepositoryMock.Setup(temp => temp.GetAllPersons())
                .ReturnsAsync(persons);

            // To print expected value in Test Detail Summary
            _testOutputHelper.WriteLine("Expected:");
            foreach (var personResponse in expectedPersonResponseList)
            {
                _testOutputHelper.WriteLine($"{personResponse}");
            }

            // Act 
            List<PersonResponse> actualPersonResponseList = await _personsGetterService.GetAllPersons();

            // To print actual value in Test Detail Summary
            _testOutputHelper.WriteLine("Actual:");
            foreach (var personResponse in actualPersonResponseList)
            {
                _testOutputHelper.WriteLine($"{personResponse}");
            }

            // Assert
            actualPersonResponseList.Should().BeEquivalentTo(expectedPersonResponseList);
        }

        #endregion

        #region GetFilteredPersons
        // if the searchString parameter is empty and searchBy is "PersonName", it should return all persons
        [Fact]
        public async Task GetFilteredPersons_SearchStringIsEmpty_ToBeSeccessful()
        {
            // Arrange
            Person personOne = _fixture.Build<Person>()
                .With(temp => temp.Country, null as Country)
                .Create();
            Person personTwo = _fixture.Build<Person>()
                .With(temp => temp.Country, null as Country)
                .Create();

            List<Person> persons = new List<Person>()
            {
                personOne, personTwo
            };

            List<PersonResponse> expectedPersonResponseList = persons.Select(temp => temp.ToPersonResponse()).ToList();

            _personsRepositoryMock.Setup(temp => temp.GetAllPersons())
                .ReturnsAsync(persons);

            // To print expected value in Test Detail Summary
            _testOutputHelper.WriteLine("Expected:");
            foreach (var personResponse in expectedPersonResponseList)
            {
                _testOutputHelper.WriteLine($"{personResponse}");
            }

            // Act 
            List<PersonResponse> actualPersonResponseList = await _personsGetterService.GetFilteredPersons(nameof(Person.PersonName), string.Empty);

            // To print actual value in Test Detail Summary
            _testOutputHelper.WriteLine("Actual:");
            foreach (var personResponse in actualPersonResponseList)
            {
                _testOutputHelper.WriteLine($"{personResponse}");
            }

            // Assert
            actualPersonResponseList.Should().BeEquivalentTo(expectedPersonResponseList);
        }

        // if the searchString is not empty and it is valid and searchBy is "PersonName", it should return the corresponsing value based on searchString and searchBy
        [Fact]
        public async Task GetFilteredPersons_SearchStringNotEmptyAndIsValidAndSearchByPersonName_ToBeSuccessful()
        {
            // Arrange
            string searchString = "da";
            Person personOne = _fixture.Build<Person>()
                .With(temp => temp.PersonName, "John Smith")
                .With(temp => temp.Country, null as Country)
                .Create();
            Person personTwo = _fixture.Build<Person>()
                .With(temp => temp.PersonName, "Adam Brown")
                .With(temp => temp.Country, null as Country)
                .Create();
            Person personThree = _fixture.Build<Person>()
                .With(temp => temp.PersonName, "David Jims")
                .With(temp => temp.Country, null as Country)
                .Create();

            List<Person> persons = new List<Person>() { personOne, personTwo, personThree }
            .Where(temp => temp.PersonName != null && temp.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();

            _personsRepositoryMock.Setup(temp => temp.GetFilteredPersons(It.IsAny<Expression<Func<Person, bool>>>()))
                .ReturnsAsync(persons);

            List<PersonResponse> expectedPersonResponseList = persons.Select(temp => temp.ToPersonResponse()).ToList();

            // To print expected value in Test Detail Summary
            _testOutputHelper.WriteLine("Expected:");
            foreach (var expectedPersonResponse in expectedPersonResponseList)
            {
                _testOutputHelper.WriteLine($"{expectedPersonResponse}");
            }

            // Act 
            List<PersonResponse> actualPersonResponseList = await _personsGetterService.GetFilteredPersons(nameof(Person.PersonName), searchString);

            // To print actual value in Test Detail Summary
            _testOutputHelper.WriteLine("Actual:");
            foreach (var personResponse in actualPersonResponseList)
            {
                _testOutputHelper.WriteLine($"{personResponse}");
            }

            // Assert
            actualPersonResponseList.Should().BeEquivalentTo(expectedPersonResponseList);
        }


        // if the searchString is not empty and it is not valid and searchBy is "PersonName", it should return an empty list
        [Fact]
        public async Task GetFilteredPersons_SearchStringNotEmptyAndIsNotValidAndSearchByPersonName_ToBeEmpty()
        {
            // Arrange
            string searchString = "da";
            Person personOne = _fixture.Build<Person>()
                .With(temp => temp.PersonName, "John Smith")
                .With(temp => temp.Country, null as Country)
                .Create();
            Person personTwo = _fixture.Build<Person>()
                .With(temp => temp.PersonName, "Jim Brown")
                .With(temp => temp.Country, null as Country)
                .Create();

            List<Person> persons = new List<Person>() { personOne, personTwo }
            .Where(temp => temp.PersonName != null && temp.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();

            _personsRepositoryMock.Setup(temp => temp.GetFilteredPersons(It.IsAny<Expression<Func<Person, bool>>>()))
                .ReturnsAsync(persons);

            // To print expected value in Test Detail Summary
            _testOutputHelper.WriteLine("Expected:");
            _testOutputHelper.WriteLine($"[]");


            // Act 
            List<PersonResponse> actualPersonResponseList = await _personsGetterService.GetFilteredPersons(nameof(Person.PersonName), searchString);

            // To print actual value in Test Detail Summary
            _testOutputHelper.WriteLine("Actual:");
            if (actualPersonResponseList.Count == 0)
            {
                _testOutputHelper.WriteLine($"[]");
            }
            else
            {
                foreach (var personResponse in actualPersonResponseList)
                {
                    _testOutputHelper.WriteLine($"{personResponse}");
                }
            }

            // Assert
            actualPersonResponseList.Should().BeEmpty();
        }

        #endregion

        #region GetSortedPersons
        // When we sort based on PersonName in DESC, it should return persons list in descending on PersonName
        [Fact]
        public async Task GetSortedPersons_ToBeSuccessful()
        {
            // Arrange
            Person personOne =
                _fixture.Build<Person>()
                .With(temp => temp.PersonName, "Adam Lambert")
                .With(temp => temp.Country, null as Country)
                .Create();
            Person personTwo =
                _fixture.Build<Person>()
                .With(temp => temp.PersonName, "Mark Smith")
                .With(temp => temp.Country, null as Country)
                .Create();
            Person personThree =
                _fixture.Build<Person>()
                .With(temp => temp.PersonName, "Bernard Huge")
                .With(temp => temp.Country, null as Country)
                .Create();

            List<Person> persons = new List<Person>()
            {
                personOne, personTwo, personThree
            };

            List<PersonResponse> personResponseList = persons.Select(temp => temp.ToPersonResponse()).ToList();

            List<PersonResponse> expectedPersonResponseList = personResponseList.OrderByDescending(temp => temp.PersonName).ToList();

            _testOutputHelper.WriteLine("Expected: ");
            foreach (var expectedPersonResponse in expectedPersonResponseList)
            {
                _testOutputHelper.WriteLine($"{expectedPersonResponse}");
            }

            // Act 
            List<PersonResponse> actualPersonResponseList = _personsSorterService.GetSortedPersons(personResponseList, nameof(Person.PersonName), SortOrderOptions.DESC);

            _testOutputHelper.WriteLine("Actual: ");
            foreach (var actualPersonResponse in actualPersonResponseList)
            {
                _testOutputHelper.WriteLine($"{actualPersonResponse}");
            }

            // Assert
            await Task.Delay(0);
            actualPersonResponseList.Should().BeInDescendingOrder(temp => temp.PersonName);

        }
        #endregion

        #region UpdatePerson

        // When we supply null as PersonUpdateRequest, it should throw ArgumentNullException
        [Fact]
        public async Task UpdatePerson_PersonIsNull_ToBeArgumentNullException()
        {
            // Arrange
            PersonUpdateRequest? personUpdateRequest = null;

            // Act 
            Func<Task> action = async () =>
            {
                await _personsUpdaterService.UpdatePerson(personUpdateRequest);
            };

            // Assert 
            await action.Should().ThrowAsync<ArgumentNullException>();

        }

        // When we supply null value as PersonName, it should throw ArgumentException
        [Fact]
        public async Task UpdatePerson_PersonNameIsNull_ToBeArgumentException()
        {
            // Arrange
            PersonUpdateRequest personUpdateRequest =
                _fixture.Build<PersonUpdateRequest>()
                .With(temp => temp.PersonName, null as string)
                .With(temp => temp.Email, "David@gmail.com")
                .Create();

            // Act 
            Func<Task> action = async () =>
            {
                await _personsUpdaterService.UpdatePerson(personUpdateRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();

        }

        // When we supply null value as Email, it should throw ArgumentException
        [Fact]
        public async Task UpdatePerson_EmailIsNull_ToBeArgumentException()
        {
            // Arrange
            PersonUpdateRequest personUpdateRequest =
                _fixture.Build<PersonUpdateRequest>()
                .With(temp => temp.Email, null as string)
                .Create();

            // Act 
            Func<Task> action = async () =>
            {
                await _personsUpdaterService.UpdatePerson(personUpdateRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When we supply invalid Email, it should throw ArgumentException
        [Fact]
        public async Task UpdatePerson_EmailIsInvalid_ToBeArgumentException()
        {
            // Arrange
            PersonUpdateRequest personUpdateRequest =
                _fixture.Build<PersonUpdateRequest>()
                .With(temp => temp.Email, "Davidgmail.com")
                .Create();

            // Act 
            Func<Task> action = async () =>
            {
                await _personsUpdaterService.UpdatePerson(personUpdateRequest);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // When we supply invalid PersonID, it should throw ArgumentException
        [Fact]
        public async Task UpdatePerson_PersonIDIsInvalid_ToBeArgumentException()
        {
            // Arrange
            PersonUpdateRequest personUpdateRequest =
                _fixture.Build<PersonUpdateRequest>()
                .With(temp => temp.Email, "Jim@gmail.com")
                .Create();

            _personsRepositoryMock.Setup(temp => temp.GetPersonByPersonID(It.IsAny<Guid>()))
                .ReturnsAsync(null as Person);

            // Act 
            Func<Task> action = async () =>
            {
                await _personsUpdaterService.UpdatePerson(personUpdateRequest);
            };

            // Assert 
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // First, add a new person and try to update the person name and email
        [Fact]
        public async Task UpdatePerson_PersonFullDetails_ToBeSuccessful()
        {
            // Arrange
            Person person = _fixture.Build<Person>()
                .With(temp => temp.PersonName, "David Smith")
                .With(temp => temp.Email, "David@gmail.com")
                .With(temp => temp.Country, null as Country)
                .Create();

            PersonResponse personResponse = person.ToPersonResponse();
            PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();
            personUpdateRequest.PersonName = "UpdatedName";
            personUpdateRequest.Email = "UpdatedEmail@gmail.com";

            PersonResponse expectedPersonResponse = personUpdateRequest.ToPerson().ToPersonResponse();

            _personsRepositoryMock.Setup(temp => temp.GetPersonByPersonID(It.IsAny<Guid>()))
                .ReturnsAsync(person);

            _personsRepositoryMock.Setup(temp => temp.UpdatePerson(It.IsAny<Person>()))
                .ReturnsAsync(personUpdateRequest.ToPerson());

            // Act
            PersonResponse actualPersonResponse = await _personsUpdaterService.UpdatePerson(personUpdateRequest);

            // Assert
            actualPersonResponse.Should().Be(expectedPersonResponse);
        }

        #endregion

        #region DeletePerson

        // if PersonID is null, it should throw ArgumentNullException
        [Fact]
        public async Task DeletePerson_PersonIDIsNull_ToBeArgumentNullException()
        {
            // Arrange
            Guid? personID = null;

            // Act 
            Func<Task> action = async () =>
            {
                await _personsDeleterService.DeletePerson(personID);
            };

            // Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        // if PersonID is invalid, it should return false
        [Fact]
        public async Task DeletePerson_PersonIDIsInvalid_ToBeFailure()
        {
            // Arrange
            Guid personID = Guid.NewGuid();

            _personsRepositoryMock.Setup(temp => temp.GetPersonByPersonID(It.IsAny<Guid>()))
                .ReturnsAsync(null as Person);

            // Act 
            bool isDeleted = await _personsDeleterService.DeletePerson(personID);

            // Assert
            isDeleted.Should().BeFalse();
        }

        // if PersonID is valid, it should delete the corresponding person and return true
        [Fact]
        public async Task DeletePerson_FullPersonDetails_ToBeSuccessful()
        {
            // Arrange
            Person person = _fixture.Build<Person>()
                .With(temp => temp.Country, null as Country)
                .Create();

            _personsRepositoryMock.Setup(temp => temp.GetPersonByPersonID(It.IsAny<Guid>()))
                .ReturnsAsync(person);

            _personsRepositoryMock.Setup(temp => temp.DeletePerson(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            // Act
            bool isDeleted = await _personsDeleterService.DeletePerson(person.PersonID);

            // Assert
            isDeleted.Should().BeTrue();
        }
        #endregion

    }
}
