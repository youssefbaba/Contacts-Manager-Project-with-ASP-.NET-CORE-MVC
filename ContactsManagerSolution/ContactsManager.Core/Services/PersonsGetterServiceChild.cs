using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.DTO;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using Serilog;
using System.Drawing;

namespace ContactsManager.Core.Services
{
    public class PersonsGetterServiceChild : PersonsGetterService
    {
        // Constructors
        public PersonsGetterServiceChild(IPersonsRepository personsRepository, ILogger<PersonsGetterService> logger, IDiagnosticContext diagnosticContext) : base(personsRepository, logger, diagnosticContext)
        {
        }

        // Methods
        public override async Task<MemoryStream> GetPersonsExcel()
        {
            MemoryStream memoryStream = new MemoryStream();

            // Create a new excel file
            using (ExcelPackage excelPackage = new ExcelPackage(memoryStream))
            {
                // Create a new WorkSheet
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("PersonsSheet");

                // Create the content of the celles (Header Row)
                worksheet.Cells["A1"].Value = "Person Name";
                worksheet.Cells["B1"].Value = "Age";
                worksheet.Cells["C1"].Value = "Gender";

                // To apply some style format
                using (ExcelRange headerCells = worksheet.Cells["A1:C1"])
                {
                    headerCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    headerCells.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    headerCells.Style.Font.Bold = true;
                }

                // Create the content of the celles (Content Of WorkSheet)
                int row = 2;
                List<PersonResponse> persons = await GetAllPersons();

                if (persons.Count == 0)
                {
                    throw new InvalidOperationException("No persons data");
                }


                foreach (PersonResponse person in persons)
                {
                    worksheet.Cells[$"A{row}"].Value = person.PersonName;
                    worksheet.Cells[$"B{row}"].Value = person.Age;
                    worksheet.Cells[$"C{row}"].Value = person.Gender;
                    row++;
                }

                // To adjust the column width
                worksheet.Cells[$"A1:C{row}"].AutoFitColumns();

                await excelPackage.SaveAsync();

                memoryStream.Position = 0;
                return memoryStream;
            }
        }
    }
}
