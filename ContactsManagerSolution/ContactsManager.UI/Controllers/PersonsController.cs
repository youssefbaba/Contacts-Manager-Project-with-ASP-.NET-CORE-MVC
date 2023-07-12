using ContactsManager.Core.DTO;
using ContactsManager.Core.Enums;
using ContactsManager.Core.ServiceContracts;
using ContactsManager.UI.Filters;
using ContactsManager.UI.Filters.ActionFilters;
using ContactsManager.UI.Filters.AuthorizationFilter;
using ContactsManager.UI.Filters.ResourceFilters;
using ContactsManager.UI.Filters.ResultFilters;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;

namespace ContactsManager.UI.Controllers
{
    [Route("[controller]")]
    //[Route("persons")]
    //[TypeFilter(type: typeof(ResponseHeaderActionFilter), Arguments = new object[] { "X-Custom-Key-From-Controller", "X-Custom-Value-From-Controller" })]  // Class-Level filter
    //[TypeFilter(type: typeof(ResponseHeaderActionFilter), Arguments = new object[] { "X-Custom-Key-From-Controller", "X-Custom-Value-From-Controller", 3}, Order = 3)]  // Class-Level filter
    [ResponseHeaderFilterFactory("X-Custom-Key-From-Controller", "X-Custom-Value-From-Controller", 3)]  // Class-Level filter
    //[TypeFilter(typeof(HandleExceptionFilter))]
    [TypeFilter(typeof(PersonsAlwaysRunResultFilter))]
    public class PersonsController : Controller
    {
        // Fields
        private readonly IPersonsGetterService _personsGetterService;
        private readonly IPersonsAdderService _personsAdderService;
        private readonly IPersonsSorterService _personsSorterService;
        private readonly IPersonsUpdaterService _personsUpdaterService;
        private readonly IPersonsDeleterService _personsDeleterService;
        private readonly ICountriesGetterService _countriesGetterService;
        private readonly ILogger<PersonsController> _logger;

        // Constructors
        public PersonsController(IPersonsGetterService personsGetterService,
                IPersonsAdderService personsAdderService,
                IPersonsSorterService personsSorterService,
                IPersonsUpdaterService personsUpdaterService,
                IPersonsDeleterService personsDeleterService,
                ICountriesGetterService countriesGetterService,
            ILogger<PersonsController> logger
            )
        {
            _personsGetterService = personsGetterService;
            _personsAdderService = personsAdderService;
            _personsSorterService = personsSorterService;
            _personsUpdaterService = personsUpdaterService;
            _personsDeleterService = personsDeleterService;
            _countriesGetterService = countriesGetterService;
            _logger = logger;
        }

        // Methods
        [Route("/")]               // Attribute Routing : /      
        [Route("[action]")]        // Attribute Routing : /Persons/Index
        [HttpGet]
        [TypeFilter(typeof(PersonsListActionFilter), Arguments = new object[] { 4 }, Order = 4)]  // Method-Level filter
        //[TypeFilter(type: typeof(ResponseHeaderActionFilter), Arguments = new object[] { "X-Custom-Key-From-Action-Method", "X-Custom-Value-From-Action-Method" })]  // Method-Level filter
        //[TypeFilter(type: typeof(ResponseHeaderActionFilter), Arguments = new object[] { "X-Custom-Key-From-Action-Method", "X-Custom-Value-From-Action-Method", 1}, Order = 1)]  // Method-Level filter
        [ResponseHeaderFilterFactory("X-Custom-Key-From-Action-Method", "X-Custom-Value-From-Action-Method", 1)]

        //[TypeFilter(typeof(PersonsListResultFilter))]   // Method-Level Filter
        [ServiceFilter(typeof(PersonsListResultFilter))]  // Method-Level Filter
        [SkipFilter]
        public async Task<IActionResult> Index(string searchBy, string? searchString,
            string sortBy = nameof(PersonResponse.PersonName), SortOrderOptions sortOrder = SortOrderOptions.ASC)

        {
            //  For tracking the execution flow
            _logger.LogInformation("{ActionMethodName} action method of {ControllerName}", nameof(Index), nameof(PersonsController));

            // For debugging purposes
            _logger.LogDebug("{searchByArgument}: {searchBy}, {searchStringArgument}: {searchString}, {sortByArgument}: {sortBy}, {sortOrderArgument}: {sortOrder}", nameof(searchBy), searchBy, nameof(searchString), searchString, nameof(sortBy), sortBy, nameof(sortOrder), sortOrder);

            // To Display all records
            List<PersonResponse> persons = await _personsGetterService.GetAllPersons();

            // Searching
            persons = await _personsGetterService.GetFilteredPersons(searchBy, searchString);

            // Sorting
            persons = _personsSorterService.GetSortedPersons(persons, sortBy, sortOrder);

            return View(persons);  // Views/Persons/Index.cshtml
        }

        [Route("[action]")]       // Attribute Routing : /Persons/Create
        //[Route("create")]       // Attribute Routing : /Persons/Create
        [HttpGet]
        //[TypeFilter(type: typeof(ResponseHeaderActionFilter), Arguments = new object[] { "X-Custom-Key-Updated", "Custom-Value-Updated" })]
        public async Task<IActionResult> Create()
        {
            List<CountryResponse> countries = await _countriesGetterService.GetAllCountries();
            ViewBag.Countries = countries;
            return View();
        }

        [Route("[action]")]       // Attribute Routing : /Persons/Create
        //[Route("create")]       // Attribute Routing : /Persons/Create
        [HttpPost]
        [TypeFilter(typeof(PersonCreateAndEditPostActionFilter))]
        //[TypeFilter(typeof(FeatureDisabledResourceFilter))]  // Disables Create action method
        [TypeFilter(typeof(FeatureDisabledResourceFilter), Arguments = new object[] { false })]  // Enables Create action method
        public async Task<IActionResult> Create(PersonAddRequest personRequest)
        {
            await _personsAdderService.AddPerson(personRequest);

            return RedirectToAction("Index", "Persons");
        }

        [HttpGet]
        [Route("[action]/{personID}")]  // Attribute Routing : /Persons/Edit/12EC350F-7E92-4DC0-8A1F-C8CB12E925E8
        [TypeFilter(typeof(TokenResultFilter))]
        public async Task<IActionResult> Edit(Guid personID)
        {
            PersonResponse? personResponse = await _personsGetterService.GetPersonByPersonID(personID);
            if (personResponse == null)
            {
                return RedirectToAction(nameof(Index));
            }

            PersonUpdateRequest personUpdateRequest = personResponse.ToPersonUpdateRequest();

            List<CountryResponse> countries = await _countriesGetterService.GetAllCountries();
            ViewBag.Countries = countries;

            return View(personUpdateRequest);
        }

        [HttpPost]
        [Route("[action]/{personID}")]  // Attribute Routing : /Persons/Edit/12EC350F-7E92-4DC0-8A1F-C8CB12E925E8
        [TypeFilter(typeof(PersonCreateAndEditPostActionFilter))]
        [TypeFilter(typeof(TokenAuthorizationFilter))]
        public async Task<IActionResult> Edit(PersonUpdateRequest personRequest)
        {
            PersonResponse? personResponse = await _personsGetterService.GetPersonByPersonID(personRequest.PersonID);
            if (personResponse == null)
            {
                return RedirectToAction(nameof(Index));
            }

            //personRequest.PersonID = Guid.NewGuid();

            await _personsUpdaterService.UpdatePerson(personRequest);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("[action]/{personID}")]  // Attribute Routing : /Persons/Delete/12EC350F-7E92-4DC0-8A1F-C8CB12E925E8
        public async Task<IActionResult> Delete(Guid personID)
        {
            PersonResponse? personResponse = await _personsGetterService.GetPersonByPersonID(personID);
            if (personResponse == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(personResponse);
        }

        [HttpPost]
        [Route("[action]/{personID}")]  // Attribute Routing : /Persons/Delete/12EC350F-7E92-4DC0-8A1F-C8CB12E925E8
        [TypeFilter(typeof(ImpactShortCircuitingResourceFilter))]
        [TypeFilter(typeof(ImpactShortCircuitingActionFilter))]
        [TypeFilter(typeof(ImpactShortCircuitingResultFilter))]
        public async Task<IActionResult> Delete(PersonUpdateRequest personUpdateRequest)
        {
            PersonResponse? personResponse = await _personsGetterService.GetPersonByPersonID(personUpdateRequest.PersonID);
            if (personResponse == null)
            {
                return RedirectToAction(nameof(Index));
            }
            await _personsDeleterService.DeletePerson(personUpdateRequest.PersonID);
            return RedirectToAction(nameof(Index));
        }

        [Route("[action]")]     // Attribute Routing : /Persons/PersonsPDF
        [HttpGet]
        public async Task<IActionResult> PersonsPDF()
        {
            // Get list of persons
            List<PersonResponse> persons = await _personsGetterService.GetAllPersons();

            // Return the view as PDF
            return new ViewAsPdf(viewName: "PersonsPDF", model: persons, viewData: null)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins()
                {
                    Top = 20,
                    Right = 20,
                    Bottom = 20,
                    Left = 20
                },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
            };
        }

        [Route("[action]")]     // Attribute Routing : /Persons/PersonsCSV
        [HttpGet]
        public async Task<IActionResult> PersonsCSV()
        {
            MemoryStream memoryStream = await _personsGetterService.GetPersonsCSV();
            return File(fileStream: memoryStream, contentType: "text/csv", fileDownloadName: "persons.csv");
        }

        [Route("[action]")]     // Attribute Routing : /Persons/PersonsExcel
        [HttpGet]
        public async Task<IActionResult> PersonsExcel()
        {
            MemoryStream memoryStream = await _personsGetterService.GetPersonsExcel();
            return File(
                fileStream: memoryStream,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "persons.xlsx");
        }
    }
}
