using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using Serilog;
using SerilogTimings;
using System.Drawing;
using System.Globalization;
using ContactsManager.Core.ServiceContracts;
using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.DTO;
using ContactsManager.Core.Domain.Entities;

namespace ContactsManager.Core.Services
{
    public class PersonsGetterService : IPersonsGetterService
    {
        // Fields
        private readonly IPersonsRepository _personsRepository;
        private readonly ILogger<PersonsGetterService> _logger;
        private readonly IDiagnosticContext _diagnosticContext;

        // Constructors
        public PersonsGetterService(IPersonsRepository personsRepository,
            ILogger<PersonsGetterService> logger, IDiagnosticContext diagnosticContext)
        {
            _personsRepository = personsRepository;
            _logger = logger;
            _diagnosticContext = diagnosticContext;
        }

        // Methods
        public virtual async Task<List<PersonResponse>> GetAllPersons()
        {
            // For tracking the execution flow 
            _logger.LogInformation("{MethodName} method of {ServiceName}", nameof(GetAllPersons), nameof(PersonsGetterService));

            /*
            // Stored Procedure
            List<Person> persons = _db.ProcedureGetAllPersons();
            */

            return (await _personsRepository.GetAllPersons()).Select(person => person.ToPersonResponse())
                .ToList();
        }

        public virtual async Task<PersonResponse?> GetPersonByPersonID(Guid? personID)
        {
            if (personID == null)
            {
                return null;
            }

            // Regular Linq Query
            Person? person = await _personsRepository.GetPersonByPersonID(personID.Value);

            /*
            // Stored Procedure
            Person? person = _db.ProcedureGetPersonByPersonID((Guid)personID);
            */

            if (person == null)
            {
                return null;
            }
            return person.ToPersonResponse();
        }

        public virtual async Task<List<PersonResponse>> GetFilteredPersons(string searchBy, string? searchString)
        {
            // For tracking the execution flow 
            _logger.LogInformation("{MethodName} method of {ServiceName}", nameof(GetFilteredPersons), nameof(PersonsGetterService));

            List<Person> persons = new List<Person>();

            if (string.IsNullOrEmpty(searchString) || string.IsNullOrEmpty(searchBy))
            {
                return (await _personsRepository.GetAllPersons()).Select(temp => temp.ToPersonResponse()).ToList();
            }

            using (Operation.Time("Time for Filtered Persons from Database"))
            {
                persons = searchBy switch
                {
                    nameof(PersonResponse.PersonName) =>
                        await _personsRepository.GetFilteredPersons(person => person.PersonName!.Contains(searchString!)),

                    nameof(PersonResponse.Email) =>
                        await _personsRepository.GetFilteredPersons(person => person.Email!.Contains(searchString!)),

                    nameof(PersonResponse.DateOfBirth) =>
                         (await _personsRepository.GetAllPersons())
                        .Where(temp => temp.DateOfBirth.HasValue && temp.DateOfBirth.Value.ToString("dd MMM yyyy").Contains(searchString!, StringComparison.OrdinalIgnoreCase)).ToList(),

                    nameof(PersonResponse.Gender) =>
                        await _personsRepository.GetFilteredPersons(person => person.Gender!.Contains(searchString!)),

                    nameof(PersonResponse.CountryID) =>
                        await _personsRepository.GetFilteredPersons(person => person.Country!.CountryName!.Contains(searchString!)),

                    nameof(PersonResponse.Address) =>
                        await _personsRepository.GetFilteredPersons(person => person.Address!.Contains(searchString!)),

                    _ =>
                        await _personsRepository.GetAllPersons()
                };
            }

            _diagnosticContext.Set("Persons", persons);

            return persons.Select(temp => temp.ToPersonResponse()).ToList();
        }

        public virtual async Task<MemoryStream> GetPersonsCSV()
        {
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream);

            CsvConfiguration csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);

            CsvWriter csvWriter = new CsvWriter(writer: streamWriter, configuration: csvConfiguration);

            // PersonName, Email, DateOfBirth, Age, Country, Address, ReceiveNewsLetters
            csvWriter.WriteField(nameof(PersonResponse.PersonName));
            csvWriter.WriteField(nameof(PersonResponse.Email));
            csvWriter.WriteField(nameof(PersonResponse.DateOfBirth));
            csvWriter.WriteField(nameof(PersonResponse.Age));
            csvWriter.WriteField(nameof(PersonResponse.Country));
            csvWriter.WriteField(nameof(PersonResponse.Address));
            csvWriter.WriteField(nameof(PersonResponse.ReceiveNewsLetters));

            csvWriter.NextRecord(); // Newline

            //List<PersonResponse> persons = await _db.Persons.Include(nameof(Person.Country)).Select(person => person.ToPersonResponse()).ToListAsync();
            List<PersonResponse> persons = await GetAllPersons();

            foreach (PersonResponse person in persons)
            {
                csvWriter.WriteField(person.PersonName);
                csvWriter.WriteField(person.Email);
                if (person.DateOfBirth.HasValue)
                {
                    csvWriter.WriteField(person.DateOfBirth.Value.ToString("yyyy-MM-dd"));
                }
                else
                {
                    csvWriter.WriteField("");
                }
                csvWriter.WriteField(person.Age);
                csvWriter.WriteField(person.Country);
                csvWriter.WriteField(person.Address);
                csvWriter.WriteField(person.ReceiveNewsLetters);
                csvWriter.NextRecord(); // Newline
                csvWriter.Flush(); // To add every record into memorystream
            }

            memoryStream.Position = 0;
            return memoryStream;
        }

        public virtual async Task<MemoryStream> GetPersonsExcel()
        {
            MemoryStream memoryStream = new MemoryStream();

            // Create a new excel file
            using (ExcelPackage excelPackage = new ExcelPackage(memoryStream))
            {
                // Create a new WorkSheet
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("PersonsSheet");

                // Create the content of the celles (Header Row)
                worksheet.Cells["A1"].Value = "Person Name";
                worksheet.Cells["B1"].Value = "Email";
                worksheet.Cells["C1"].Value = "Date Of Birth";
                worksheet.Cells["D1"].Value = "Age";
                worksheet.Cells["E1"].Value = "Gender";
                worksheet.Cells["F1"].Value = "Country";
                worksheet.Cells["G1"].Value = "Address";
                worksheet.Cells["H1"].Value = "Receive News Letters";

                // To apply some style format
                using (ExcelRange headerCells = worksheet.Cells["A1:H1"])
                {
                    headerCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    headerCells.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    headerCells.Style.Font.Bold = true;
                }

                // Create the content of the celles (Content Of WorkSheet)
                int row = 2;
                List<PersonResponse> persons = await GetAllPersons();
                foreach (PersonResponse person in persons)
                {
                    worksheet.Cells[$"A{row}"].Value = person.PersonName;
                    worksheet.Cells[$"B{row}"].Value = person.Email;
                    if (person.DateOfBirth.HasValue)
                    {
                        worksheet.Cells[$"C{row}"].Value = person.DateOfBirth.Value.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        worksheet.Cells[$"C{row}"].Value = "";
                    }
                    worksheet.Cells[$"D{row}"].Value = person.Age;
                    worksheet.Cells[$"E{row}"].Value = person.Gender;
                    worksheet.Cells[$"F{row}"].Value = person.Country;
                    worksheet.Cells[$"G{row}"].Value = person.Address;
                    worksheet.Cells[$"H{row}"].Value = person.ReceiveNewsLetters;
                    row++;
                }

                // To adjust the column width
                worksheet.Cells[$"A1:H{row}"].AutoFitColumns();

                await excelPackage.SaveAsync();

                memoryStream.Position = 0;
                return memoryStream;
            }
        }
    }
}
