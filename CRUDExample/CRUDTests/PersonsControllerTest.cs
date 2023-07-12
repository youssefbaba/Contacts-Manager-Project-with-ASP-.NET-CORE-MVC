using AutoFixture;
using Moq;
using ServiceContracts;
using FluentAssertions;
using CRUDExample.Controllers;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CRUDTests
{
    public class PersonsControllerTest
    {
        // Fields

        private readonly IFixture _fixture;

        private readonly Mock<IPersonsGetterService> _personsGetterServiceMock;
        private readonly Mock<IPersonsAdderService> _personsAdderServiceMock;
        private readonly Mock<IPersonsSorterService> _personsSorterServiceMock;
        private readonly Mock<IPersonsUpdaterService> _personsUpdaterServiceMock;
        private readonly Mock<IPersonsDeleterService> _personsDeleterServiceMock;
        private readonly Mock<ICountriesGetterService> _countriesGetterServiceMock;

        private readonly IPersonsGetterService _personsGetterService;
        private readonly IPersonsAdderService _personsAdderService;
        private readonly IPersonsSorterService _personsSorterService;
        private readonly IPersonsUpdaterService _personsUpdaterService;
        private readonly IPersonsDeleterService _personsDeleterService;

        private readonly ICountriesGetterService _countriesGetterService;

        private readonly PersonsController _personsController;

        private readonly Mock<ILogger<PersonsController>> _loggerMock;
        private readonly ILogger<PersonsController> _logger;

        // Constructors
        public PersonsControllerTest()
        {
            _fixture = new Fixture();

            _loggerMock = new Mock<ILogger<PersonsController>>();
            _logger = _loggerMock.Object;

            _personsGetterServiceMock = new Mock<IPersonsGetterService>();
            _personsAdderServiceMock = new Mock<IPersonsAdderService>();
            _personsSorterServiceMock = new Mock<IPersonsSorterService>();
            _personsUpdaterServiceMock = new Mock<IPersonsUpdaterService>();
            _personsDeleterServiceMock = new Mock<IPersonsDeleterService>();
            _countriesGetterServiceMock = new Mock<ICountriesGetterService>();

            _personsGetterService = _personsGetterServiceMock.Object;
            _personsAdderService = _personsAdderServiceMock.Object;
            _personsSorterService = _personsSorterServiceMock.Object;
            _personsUpdaterService = _personsUpdaterServiceMock.Object;
            _personsDeleterService = _personsDeleterServiceMock.Object;
            _countriesGetterService = _countriesGetterServiceMock.Object;

            _personsController = new PersonsController(
                _personsGetterService, _personsAdderService,
                _personsSorterService, _personsUpdaterService,
                _personsDeleterService,_countriesGetterService, _logger);
        }

        // Methods
        #region Index

        [Fact]
        public async Task Index_ShouldReturnIndexViewWithPersonsList()
        {
            // Arrange 
            List<PersonResponse> personResponseList =
                _fixture.Create<List<PersonResponse>>();

            _personsGetterServiceMock.Setup(temp => temp.GetAllPersons())
                .ReturnsAsync(personResponseList);

            _personsGetterServiceMock.Setup(temp => temp.GetFilteredPersons(It.IsAny<string>(), It.IsAny<string?>()))
                .ReturnsAsync(personResponseList);

            _personsSorterServiceMock.Setup(temp => temp.GetSortedPersons(It.IsAny<List<PersonResponse>>(), It.IsAny<string>(), It.IsAny<SortOrderOptions>()))
                .Returns(personResponseList);

            // Act
            IActionResult result = await _personsController.Index(searchBy: nameof(PersonResponse.PersonName), searchString: string.Empty, sortBy: nameof(PersonResponse.PersonName), sortOrder: SortOrderOptions.DESC);

            // Assert 
            ViewResult viewResult = Assert.IsType<ViewResult>(result);

            viewResult.ViewData.Model.Should().BeAssignableTo<IEnumerable<PersonResponse>>();

            viewResult.ViewData.Model.Should().Be(personResponseList);
        }

        #endregion

        #region GetCreate

        [Fact]
        public async Task Create_ShouldReturnCreateView()
        {
            // Arrange 
            List<CountryResponse> countryResponseList = _fixture.Create<List<CountryResponse>>();

            // To mock GetAllCountries method
            _countriesGetterServiceMock.Setup(temp => temp.GetAllCountries())
                .ReturnsAsync(countryResponseList);

            // Act
            IActionResult result = await _personsController.Create();

            // Assert
            result.Should().BeOfType<ViewResult>();
        }

        #endregion

        #region PostCreate
        [Fact]
        public async Task Create_IfNoModelErrors_ToRedirectToIndexView()
        {
            // Arrange
            PersonAddRequest personAddRequest = _fixture.Build<PersonAddRequest>()
                .With(temp => temp.Email, "Jim@gmail.com")
                .Create();

            PersonResponse personResponse = (personAddRequest.ToPerson()).ToPersonResponse();

            // To mock AddPerson method
            _personsAdderServiceMock.Setup(temp => temp.AddPerson(It.IsAny<PersonAddRequest?>()))
                .ReturnsAsync(personResponse);

            // Act
            IActionResult result = await _personsController.Create(personAddRequest);

            // Assert 
            RedirectToActionResult redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            redirectToActionResult.ActionName.Should().Be("Index");

            redirectToActionResult.ControllerName.Should().Be("Persons");
        }

        #endregion
    }
}
